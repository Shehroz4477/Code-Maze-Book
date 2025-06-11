using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contract.Interfaces;

namespace Service.DataShaping;

public class DataShaper<T> : IDataShaper<T> where T : class
{
    public PropertyInfo[] Properties { get; set; }

    public DataShaper()
    {
        Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchData(entities, requiredProperties);
    }

    public ExpandoObject ShapeData(T entity, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchDataForEntity(entity, requiredProperties);
    }

    private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
    {
        var requiredProperties = new List<PropertyInfo>();

        if(!string.IsNullOrEmpty(fieldsString))
        {
            var fields = fieldsString.Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (var field in fields)
            {
                var property = Properties.FirstOrDefault(p => p.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                if(property != null)
                {
                    requiredProperties.Add(property);
                }
            }
        }
        else
        {
            requiredProperties = Properties.ToList();
        }

        return requiredProperties;
    }

    private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapeData = new ExpandoObject();

        foreach(var property in requiredProperties)
        {
            var objectPropertyValue = property.GetValue(entity);
            shapeData.TryAdd(property.Name, objectPropertyValue);
        }
        return shapeData;
    }

    private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entitis, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapeData = new List<ExpandoObject>();

        foreach (var entity in entitis)
        {
            var shapedObject = FetchDataForEntity(entity, requiredProperties);
            shapeData.Add(shapedObject);
        }

        return shapeData;
    }
}
