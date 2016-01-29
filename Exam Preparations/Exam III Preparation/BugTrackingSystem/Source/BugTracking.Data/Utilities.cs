using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Data
{
    public static class Utilities
    {
        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj, int page, int pageSize, Expression<Func<T, TResult>> orderExpression, bool asc, out int itemsCount)
        {
            itemsCount = obj.Count();
            if (asc)
            {

                return obj.OrderBy(orderExpression).Skip(page * pageSize).Take(pageSize);
            }
            else
            {
                return obj.OrderByDescending(orderExpression).Skip(page * pageSize).Take(pageSize);
            }
        }

        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj, int page, int pageSize, Expression<Func<T, TResult>> orderExpression, Expression<Func<T, bool>> whereExpression, bool asc, out int itemsCount)
        {
            if (asc)
            {

                var result = obj.OrderBy(orderExpression).Where(whereExpression).Skip(page * pageSize).Take(pageSize);
                itemsCount = obj.Where(whereExpression).Count();
                return result;
            }
            else
            {
                var result = obj.OrderByDescending(orderExpression).Where(whereExpression).Skip(page * pageSize).Take(pageSize);
                itemsCount = obj.Where(whereExpression).Count();
                return result;
            }
        }

        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj, int page, int pageSize, Expression<Func<T, TResult>> orderExpression, Expression<Func<T, bool>> whereExpression1, Expression<Func<T, bool>> whereExpression2, bool asc, out int itemsCount)
        {
            if (asc)
            {
                var result = obj.OrderBy(orderExpression).Where(whereExpression1).Where(whereExpression2).Skip(page * pageSize).Take(pageSize);
                itemsCount = obj.Where(whereExpression1).Where(whereExpression2).Count();
                return result;
            }
            else
            {
                var result = obj.OrderByDescending(orderExpression).Where(whereExpression1).Where(whereExpression2).Skip(page * pageSize).Take(pageSize);
                itemsCount = obj.Where(whereExpression1).Where(whereExpression2).Count();
                return result;
            }
        }

        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj, int page, int pageSize, Expression<Func<T, TResult>> orderExpression, Expression<Func<T, bool>> whereExpression1, Expression<Func<T, bool>> whereExpression2, Expression<Func<T, bool>> whereExpression3, bool asc, out int itemsCount)
        {
            if (asc)
            {

                var result = obj.OrderBy(orderExpression).Where(whereExpression1).Where(whereExpression2).Where(whereExpression3).Skip(page * pageSize).Take(pageSize);
                itemsCount = obj.Where(whereExpression1).Where(whereExpression2).Where(whereExpression3).Count();
                return result;
            }
            else
            {
                var result = obj.OrderByDescending(orderExpression).Where(whereExpression1).Where(whereExpression2).Where(whereExpression3).Skip(page * pageSize).Take(pageSize);
                itemsCount = obj.Where(whereExpression1).Where(whereExpression2).Where(whereExpression3).Count();
                return result;
            }
        }

        public static string ShortDescription(string description) 
        {
            if (description.Length > 50) 
            {
                return description.Substring(0, 50) + "...";
            }

            return description;
        }
    }
}
