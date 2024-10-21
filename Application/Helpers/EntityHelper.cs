using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application
{
    public class EntityHelper
    {
        public static dynamic Pick<T>(T entity, params string[] properties)
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

        public static dynamic Omit<T>(T entity, params string[] properties)
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

        public static T Clone<T>(T data)
            where T : class, new()
        {
            var serialized = JsonConvert.SerializeObject(data);
            var clonedData = JsonConvert.DeserializeObject<T>(serialized);

            return clonedData ?? new T(); 
        }
    }
}
