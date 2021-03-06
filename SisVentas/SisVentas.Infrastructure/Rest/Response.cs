namespace SisVentas.Infrastructure.Rest
{
    public class Response<T>
    {
        public int IsSuccess { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
