using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest.Services.Models
{
    public class Pagination
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public int totalPages { get; set; }
    }

    public class AccountTransactions
    {
        public string accountId { get; set; }
        public List<object> transactions { get; set; }
        public Pagination pagination { get; set; }
    }
}
