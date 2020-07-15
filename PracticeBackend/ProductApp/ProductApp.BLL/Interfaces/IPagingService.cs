using ProductApp.BLL.Models;
using System.Collections.Generic;

namespace ProductApp.BLL.Interfaces
{
    public interface IPagingService<T>
    {
        PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize);
    }
}
