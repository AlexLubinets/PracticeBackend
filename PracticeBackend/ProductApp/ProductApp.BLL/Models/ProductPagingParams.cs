using ProductApp.BLL.Constants;

namespace ProductApp.BLL.Models
{
    public class ProductPagingParams
    {
        const int maxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public SortProductState SortOrder { get; set; } = SortProductState.NameAsc;

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
