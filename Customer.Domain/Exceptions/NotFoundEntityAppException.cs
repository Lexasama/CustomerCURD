namespace Customer.Domain.Exceptions
{
    public class NotFoundEntityAppException : AppException
    {
        public NotFoundEntityAppException(string name, int id) : base(
            $"Entity {name} not found. Current id: ${id}")
        {
        }

        public NotFoundEntityAppException(string message) : base(message)
        {
        }
    }
}