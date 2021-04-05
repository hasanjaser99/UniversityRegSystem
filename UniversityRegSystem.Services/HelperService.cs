using System;
using System.Data.Entity;
using System.Linq;

namespace UniversityRegSystem.Services
{
    public class HelperService<T> where T : class
    {
        public static IQueryable<T> IncludeProps(string includeProperities, IQueryable<T> query)
        {
            foreach (var includeProp in includeProperities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }

            return query;
        }
    }
}