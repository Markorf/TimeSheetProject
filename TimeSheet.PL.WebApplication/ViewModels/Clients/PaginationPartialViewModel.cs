using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.PL.WebApplication.ViewModels.Clients
{
    public class PaginationPartialViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 1;
        public readonly int RowsPerPage = 5;
        public int RowsOffset { get; set; } = 0;
    }
}