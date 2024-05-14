

namespace MoviesGallery.Shared.Models.Response
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public T? ResponseObject { get; set; }
    }
}
