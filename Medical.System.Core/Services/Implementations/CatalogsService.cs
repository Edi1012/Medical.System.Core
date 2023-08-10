using FluentValidation;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities.Catalogs;
using Medical.System.Core.Services.Interfaces;
using Medical.System.Core.UnitOfWork;
using Medical.System.Core.Validator;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Medical.System.Core.Services.Implementations;

public class CatalogsService : ICatalogsService
{
    //private readonly IMongoCollection<User> _users;
    //private const string DATABASE_NAME = "MedicalSystem";
    //private const string COLLECTION_NAME = "Catalogs_user";
    private readonly CreateUserValidator _createUserValidator;


    public IDatabaseResolverService DatabaseResolverService { get; }
    public IUnitOfWork UnitOfWork { get; }

    public CatalogsService(/*IDatabaseResolverService DatabaseResolverService*/ IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
        _createUserValidator = new CreateUserValidator(unitOfWork.Users);

        //this.DatabaseResolverService = DatabaseResolverService;
        //DatabaseResolverService.Init();
        //DatabaseResolverService.GetDatabase(DatabaseTypes.Catalogs);
        //_users = DatabaseResolverService.GetDatabase(DatabaseTypes.Catalogs).GetColl<User>(COLLECTION_NAME);
    }

    public async Task<User> CreateUserAsync(CreateUserDto userDto)
    {

        var user = new User
        {
            // Generate new ObjectId
            Id = ObjectId.GenerateNewId().ToString(),
            Login = new Login()
            {
                Username = userDto.UserName,
                PasswordHash = userDto.Password, // remember to hash this before saving in a real scenario!
            }
        };


        var validationResult = await _createUserValidator.ValidateAsync(userDto);

        if (!validationResult.IsValid)
        {

            throw new ValidationException(validationResult.Errors);
            //return BadRequest(validationResult.Errors);
            //return (validationResult.Errors.FirstOrDefault().ErrorMessage.ToString());
        }

        var result = UnitOfWork.Users.AddAsync(user);

        return user;
    }
}

