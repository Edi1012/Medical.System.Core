namespace Medical.System.Core.Attributes;


[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class BsonCollectionNameAttribute : Attribute
{
    public string Name { get; }

    public BsonCollectionNameAttribute(string name)
    {
        Name = name;
    }
}


