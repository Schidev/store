using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors
{
    public class PostamateDeliveryService : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>()
        {
            { "1","Киев"},
            { "2","Кривой Рог"}
        };

        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postamates = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            {
                "1", 
                new Dictionary<string, string>
                    {
                        {"1","Киевский вокзал"},
                        {"2","Охотный ряд"},
                        {"3","Савёловский рынок"},
                    }
            },
            {
                "2",
                new Dictionary<string, string>
                    {
                        {"4","Криворожское СТО"},
                        {"5","Гостиница Киев"},
                        {"6","Петропавловская крепость"},
                    }
            },
        };

        public string UniqueCode => "Postamate";

        public string Title => "Доставка через постаматы в Киеве и Кривом Роге.";

        public OrderDelivery GetDelivery(Form form)
        {
            if (form.UniqueCode != UniqueCode || !form.IsFinal)
            {
                throw new InvalidOperationException("Invalid form.");
            }

            var cityId = form.Fields
                             .Single(field => field.Name == "city")
                             .Value;
            var cityName = cities[cityId];
            var postamateId = form.Fields
                                  .Single(field => field.Name == "postamate").Value;
            var postamateName = postamates[cityId][postamateId];

            var parameters = new Dictionary<string, string>
            {
                { nameof(cityId), cityId},
                { nameof(cityName), cityName},
                { nameof(postamateId), postamateId},
                { nameof(postamateName), postamateName}
            };

            var description = $"Город: {cityName}.\nПостамат: {postamateName}.";

            return new OrderDelivery(UniqueCode, description, 150m, parameters);
        }

        public Form CreateForm(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return new Form(UniqueCode, order.Id, 1, false, new[]
            {
                new SelectionField("Город", "city", "1", cities)
            });

        }

        public Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            if (step == 1)
            {
                if (values["city"] == "1")
                {
                    return new Form(UniqueCode, orderId, 2, false, new Field[]
                    {
                        new HiddenField("Город", "city","1"),
                        new SelectionField("Постамат", "postamate","1", postamates["1"]),
                    });
                }
                else if (values["city"] == "2")
                {
                    return new Form(UniqueCode, orderId, 2, false, new Field[]
                    {
                        new HiddenField("Город","city","2"),
                        new SelectionField("Постамат","postamate","4",postamates["2"])
                    });
                }
                else
                    throw new InvalidOperationException("Invalid postamate id.");
            }
            else if (step == 2)
            {
                return new Form(UniqueCode, orderId, 3, true, new Field[]
                {
                    new HiddenField("Город", "city", values["city"]),
                    new HiddenField("Постамат", "postamate", values["postamate"])
                });
            }
            else
                throw new InvalidOperationException("Invalid postamate step.");
        }
    }
}
