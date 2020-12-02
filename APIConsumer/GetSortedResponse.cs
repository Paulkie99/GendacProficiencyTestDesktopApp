using System.Collections.Generic;

namespace APIConsumer
{
    // Class to represent the json response after a sorted, filtered, ordered request
    class GetSortedResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending{ get; set; }
        public string Filter { get; set; }
        public List<Product> Results { get; set; }
    }
}
