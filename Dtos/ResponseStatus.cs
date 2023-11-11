using Newtonsoft.Json;

namespace Booking.API.Dtos
{
    public class ResponseStatus
    {
        [JsonIgnore]
        public int StatusCode { get; set; }

        public string Message { get; set; }
        public string Ref { get; set; }
    }
}
