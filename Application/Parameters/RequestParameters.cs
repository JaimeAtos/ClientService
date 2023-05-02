namespace Application.Parameters
{
    public class RequestParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public RequestParameters()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public RequestParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 5 ? 5 : pageSize;

        }
    }
}
