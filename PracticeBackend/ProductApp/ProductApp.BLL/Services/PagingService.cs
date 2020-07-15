using System;
using System.Collections.Generic;
using System.Linq;
using ProductApp.BLL.Interfaces;
using ProductApp.BLL.Models;

namespace ProductApp.BLL.Services
{
    public class PagingService<T> : IPagingService<T>
    {
		public PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
		{
			var count = source.Count();
			var totalPages = (int)Math.Ceiling(count / (double)pageSize);
			if (pageNumber > totalPages)
				pageNumber = totalPages;

			var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			return new PagedList<T>(items, count, pageNumber, pageSize, totalPages);
		}
	}
}
