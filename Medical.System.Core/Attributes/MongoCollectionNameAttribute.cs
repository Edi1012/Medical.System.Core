namespace Medical.System.Core.Attributes;


[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class MongoCollectionNameAttribute : Attribute
{
    public string Name { get; }

    public MongoCollectionNameAttribute(string name)
    {
        Name = name;
    }
}


