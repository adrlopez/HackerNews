using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Application.Models
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(List<T> items, int pageNumber, int pageSize, long totalItems)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public long TotalItems { get; set; }

        public int TotalPages { get; set; }

        public bool HasPrevious => PageNumber > 1;

        public bool HasNext => PageNumber < TotalPages;

        public List<T> Items { get; set; }

        public static PaginatedResponse<T> Create(IQueryable<T> items, int pageNumber, int pageSize)
        {
            var totalItems = items != null && items.Any() ? items.Count() : 0;
            var itemsInPage = items != null && items.Any() ? items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList() : new List<T>();

            return new PaginatedResponse<T>(itemsInPage, pageNumber, pageSize, totalItems);
        }
    }
}
