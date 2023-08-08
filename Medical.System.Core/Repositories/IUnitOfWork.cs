﻿using Medical.System.Core.Repositories.Interfaces;

namespace Medical.System.Core.Repositories;


public interface IUnitOfWork
{
    //IRepository<User> Users { get; }
    IUserRepository Users { get; }
    Task CompleteAsync();
}

