using System;

namespace DTO
{
    public class UserFilter
    {
        public string Keyword { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
