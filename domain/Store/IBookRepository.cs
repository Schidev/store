namespace Store
{
    public interface IBookRepository
    {
        public Book[] GetAllByTitle(string titlePart);
    }
}