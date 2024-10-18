using System.Dynamic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

public static class EntityHelper
{
    public static IDictionary<string, object> Pick<T>(T entity, params string[] properties)
    {
        var result = new Dictionary<string, object>();
        var entityType = typeof(T);
        var entityProperties = entityType.GetProperties();

        foreach (var property in entityProperties)
        {
            if (properties.Contains(property.Name))
            {
                var value = property.GetValue(entity);
                if (value != null)
                {
                    result[property.Name] = value;
                }
            }
        }

        return result;
    }

    public static IDictionary<string, object> Omit<T>(T entity, params string[] properties)
    {
        var result = new Dictionary<string, object>();
        var entityType = typeof(T);
        var entityProperties = entityType.GetProperties();

        foreach (var property in entityProperties)
        {
            if (!properties.Contains(property.Name))
            {
                var value = property.GetValue(entity);
                if (value != null)
                {
                    result[property.Name] = value;
                }
            }
        }

        return result;
    }
}
