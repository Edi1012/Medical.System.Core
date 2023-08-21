using Medical.System.Core.Attributes;

namespace Medical.System.Core.Helpers;
public static class MongoCollectionHelper
{
    public static string GetCollectionName<T>()
    {
        return ((MongoCollectionNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(MongoCollectionNameAttribute)))?.Name;
    }
}

