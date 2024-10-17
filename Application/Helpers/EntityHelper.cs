using System.Dynamic;
using System.Reflection;

public static class EntityHelper
{
    public static object Pick<T>(T entity, params string[] properties)
    {
        var type = typeof(T);
        var result = new ExpandoObject() as IDictionary<string, object>;

        foreach (var prop in properties)
        {
            var propertyInfo = type.GetProperty(prop, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null)
            {
                var value = propertyInfo.GetValue(entity);

                if (value != null)
                    result.Add(prop, value);
            }
        }

        return result;
    }

    public static object Omit<T>(T entity, params string[] properties)
    {
        var type = typeof(T);
        var result = new ExpandoObject() as IDictionary<string, object>;
        var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var propertyInfo in allProperties)
        {
            if (!properties.Contains(propertyInfo.Name))
            {
                var value = propertyInfo.GetValue(entity);
                if (value != null)
                    result.Add(propertyInfo.Name, value);
            }
        }

        return result;
    }
}
