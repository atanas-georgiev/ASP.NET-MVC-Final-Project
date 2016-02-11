using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AutoMapper.QueryableExtensions;
using Planex.Data.Models;
using Planex.Web.App_Start;

namespace Planex.Web.Infrastructure.Mappings
{
    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}