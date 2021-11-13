using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionPortal.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);

        }

        public bool PreviousPage
        {
            get { return (PageIndex > 1); }
        }

        public bool NextPage
        {
            get { return (PageIndex < TotalPages); }
        }

        //public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        public static async Task<PaginatedList<T>> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);

            //var count = await source.CountAsync();
            //var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();            
            //return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
        internal static Task<object> CreateAsync(object p, int v, int pageSize)
        {
            throw new NotImplementedException();
        }
        internal static Task<IEnumerable<Emp>> CreateAsync<TEntity>(IQueryable<TEntity> entities, object p, int pageSize) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
