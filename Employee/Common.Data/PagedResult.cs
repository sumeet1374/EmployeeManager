using System;
using System.Collections.Generic;

namespace Common.Data
{
    public class PagedResult<T>
    {
        public int RowCount { get; set; }
        public int NumberOfPages { get; set; }
        public List<T> Result { get; set; }
    }
}
