

using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge) =>
        employees.Where(entity => entity.Age >= minAge && entity.Age <= maxAge);
    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
    {
        if(string.IsNullOrEmpty(searchTerm))
        {
            return employees;
        }

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return employees.Where(enitiy => enitiy.Name.ToLower().Contains(lowerCaseTerm));
    }
    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
    {
        if(string.IsNullOrEmpty(orderByQueryString))
        {
            return employees.OrderBy(entity =>  entity.Name);
        }

        var orderParams = orderByQueryString.Trim().Split(',');

        var propInfos = typeof(Employee).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var orderQueryBuilder = new StringBuilder();

        foreach( var param in orderParams)
        {
            if(string.IsNullOrWhiteSpace(param))
            {
                continue;
            }

            var propFromQueryName = param.Split(" ")[0];
            var objProp = propInfos.FirstOrDefault(propInfo => propInfo.Name.Equals(propFromQueryName, StringComparison.OrdinalIgnoreCase));
            if(objProp is null)
            {
                continue;
            }

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            orderQueryBuilder.Append($"{objProp.Name.ToString()} {direction},");
        }
        
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',',' ');

        if(string.IsNullOrWhiteSpace(orderQuery))
        {
            return employees.OrderBy(entity => entity.Name);
        }

        //return employees.OrderBy(entity => entity.Age);
        return employees.OrderBy(orderQuery);
    }
}
