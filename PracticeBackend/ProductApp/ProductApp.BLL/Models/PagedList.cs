using System.Collections.Generic;

namespace ProductApp.BLL.Models
{
    public class PagedList<T> : List<T>
	{
		public int PageNumber { get; private set; }
		public int TotalPages { get; private set; }
		public int PageSize { get; private set; }
		public int TotalCount { get; private set; }

		public bool HasPrevious => PageNumber > 1;
		public bool HasNext => PageNumber < TotalPages;

		public PagedList(List<T> items, int count, int pageNumber, int pageSize, int totalPages)
		{
			TotalCount = count;
			PageSize = pageSize;
			PageNumber = pageNumber;
			TotalPages = totalPages;

			AddRange(items);
		}
	}
}
