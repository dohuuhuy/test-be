public static class EntityHelper
{
    public static Dictionary<string, object?> Pick<T>(T entity, params string[] properties)
    {
        var result = new Dictionary<string, object?>();
        var entityType = typeof(T);

        foreach (var property in properties)
        {
            var propInfo = entityType.GetProperty(property);
            if (propInfo != null)
            {
                var value = propInfo.GetValue(entity);
                result[property] = value; // Assign the value, it can be null
            }
            // If the property is not found, do not add it to the result
        }

        return result;
    }

    public static T Omit<T>(T entity, params string[] propertiesToOmit)
    {
        var result = Activator.CreateInstance<T>();
        var entityType = typeof(T);

        var propertiesToOmitSet = new HashSet<string>(propertiesToOmit);

        foreach (var propInfo in entityType.GetProperties())
        {
            if (!propertiesToOmitSet.Contains(propInfo.Name))
            {
                propInfo.SetValue(result, propInfo.GetValue(entity));
            }
        }

        return result;
    }
}
