namespace Domain.DTO.Warehous
{
    public class WarehouseFilter
    {
        public string Keyword { get; set; }
        public string OrderBy { get; set; }
        public string Direction { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool Active { get; set; }
    }
}

