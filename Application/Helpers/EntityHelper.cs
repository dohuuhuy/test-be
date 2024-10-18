public static class EntityHelper
{
    public static T Pick<T>(T entity, params string[] properties)
        where T : new()
    {
        var result = new T();
        var entityType = typeof(T);
        var propertiesToPick = new HashSet<string>(properties); // HashSet for quick lookup

        foreach (var propInfo in entityType.GetProperties())
        {
            // If the property is in the pick list, copy its value
            if (propertiesToPick.Contains(propInfo.Name))
            {
                var value = propInfo.GetValue(entity);
                propInfo.SetValue(result, value); // Set the value on the new instance
            }
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
