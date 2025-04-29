using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pricat.Utilities.Helpers
{
    public static class PaginationHelper
    {
        public static IQueryable<T> Paginate<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public static IEnumerable<T> Paginate<T>(
            this IEnumerable<T> enumerable,
            int pageNumber,
            int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return enumerable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public static PagedResponse<T> ToPagedResponse<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            var totalItems = query.Count();
            var items = query.Paginate(pageNumber, pageSize).ToList();

            return new PagedResponse<T>(
                items,
                pageNumber,
                pageSize,
                totalItems
            );
        }

        public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            var totalItems = await query.CountAsync();
            var items = await query
                .Paginate(pageNumber, pageSize)
                .ToListAsync();

            return new PagedResponse<T>(
                items,
                pageNumber,
                pageSize,
                totalItems
            );
        }
    }

    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PagedResponse(IEnumerable<T> items, int pageNumber, int pageSize, int totalItems)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }

        public PagedResponse() { }
    }

}
