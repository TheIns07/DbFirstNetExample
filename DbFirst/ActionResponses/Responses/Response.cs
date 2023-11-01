using DbFirst.ActionResponses.Status;

namespace DbFirst.ActionResponses.TeamResponse
{
    public class Response
    {
        public StatusAPI Status { get; set; }
        public string? Message { get; set; }
        public Object? Created { get; set;  }
        public Object? Updated { get; set; }
        public Object? Obtained { get; set; }

        public Response(Object returnetObject)
        {
        }
    }
}
