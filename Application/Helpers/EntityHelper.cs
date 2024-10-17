public static class EntityHelper
{
    public static T Pick<T>(T entity, params string[] properties)
    {
        var result = Activator.CreateInstance<T>();
        var entityType = typeof(T);

        foreach (var property in properties)
        {
            var propInfo = entityType.GetProperty(property);
            if (propInfo != null)
            {
                propInfo.SetValue(result, propInfo.GetValue(entity));
            }
        }

        return result;
    }
}
