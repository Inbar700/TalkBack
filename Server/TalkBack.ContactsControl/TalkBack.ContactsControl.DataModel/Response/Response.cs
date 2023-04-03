namespace DM.Response
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }

        public object Entity { get; set; }

        //public ErrorMessage Error { get; set; }

        public Response()
        {
            IsSuccess = true;
        }
    }
}
