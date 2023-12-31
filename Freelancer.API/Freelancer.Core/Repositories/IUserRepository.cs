﻿using Freelancer.Core.Entities;

namespace Freelancer.Core.Repositories;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User project);
    Task<User?> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
}
