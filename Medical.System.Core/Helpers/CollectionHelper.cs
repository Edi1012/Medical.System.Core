using Medical.System.Core.Attributes;

namespace Medical.System.Core.Helpers;
public static class CollectionHelper
{
    public static string GetCollectionName<T>()
    {
        return ((BsonCollectionNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(BsonCollectionNameAttribute)))?.Name;
    }
}

