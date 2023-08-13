using FluentValidation;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Medical.System.Core.Services.Interfaces;
using Medical.System.Core.UnitOfWork;
using Medical.System.Core.Validator;
using MongoDB.Bson;

namespace Medical.System.Core.Services.Implementations;

public class UsersService : IUsersService
{
    private readonly CreateUserValidator _createUserValidator;


    public IUnitOfWork UnitOfWork { get; }

    public UsersService(IUnitOfWork unitOfWork)
    {
        UnitOfWork              = unitOfWork;
        _createUserValidator    = new CreateUserValidator(unitOfWork.Users);
    }

    public async Task<User> CreateUserAsync(CreateUserDto userDto)
    {

        //TODO:use Automapper
        var user = new User
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Login = new Loggin()
            {
                Username = userDto.Login.Username,
                PasswordHash = userDto.Login.PasswordHash,
            },
            Roles = userDto.Roles.Select(x => new Role { Name = x.Name }).ToList(),
        };

        


        var validationResult = await _createUserValidator.ValidateAsync(userDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var result = UnitOfWork.Users.AddAsync(user);

        return user;
    }
}

