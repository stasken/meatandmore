﻿using MeatAndMoreAPI.Data;
using MeatAndMoreAPI.DTOs;
using MeatAndMoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MeatAndMoreContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserRepository(MeatAndMoreContext context, RoleManager<Role> roleManager, UserManager<User> userManager) 
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserDTO> GetUserDetails(string id)
        {
            return await _context.Users.Include(u => u.UserRoles)
                .Select(u => new UserDTO()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(x => new UserRolesDTO()
                    {
                        Name = x.Role.Name,
                        Description = x.Role.Description
                    }).ToList(),
                    Token = null
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await _context.Users.Include(u => u.UserRoles)
                .Select(u => new UserDTO()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(x => new UserRolesDTO()
                    {
                        Name = x.Role.Name,
                        Description = x.Role.Description
                    }).ToList(),
                    Token = null
                })
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
