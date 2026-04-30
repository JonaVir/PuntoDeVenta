using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Application.Core
{
    public class PagedList<T>
    {
        #region Fields and properties
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
        #endregion

        #region Builders
            public PagedList(List<T> items, int count, int pageNumber, int pageSize)
            {
                Items = items;
                TotalCount = count;
                PageSize = pageSize;
                CurrentPage = pageNumber;
                TotalPages = (int)Math.Ceiling(count/(double)pageSize);
        }
        #endregion

        #region Methods
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
        #endregion
    }
}
