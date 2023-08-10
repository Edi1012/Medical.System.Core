using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;


namespace Medical.System.Core.Models.Entities.Catalogs;

//public class User
//{
//    [BsonId]
//    [BsonRepresentation(BsonType.ObjectId)]
//    public string Id { get; set; }
//    public string UserName { get; set; }
//    public string Password { get; set; }
//}

//TODO:separate class role and login    
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Email { get; set; }
    public Login Login { get; set; } = new Login();
    public List<Role> Roles { get; set; } = new List<Role>();
    // Otros campos según sea necesario
}

public class Login
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}

public class Role
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
}
