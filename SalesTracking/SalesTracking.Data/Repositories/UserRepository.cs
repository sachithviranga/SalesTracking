using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Auth;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.Stock;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public UserRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserByUserName(string email)
        {
            var user = await _context.User
                .Where(u => u.Email.ToLower() == email.ToLower())
                .Include(a => a.UserRole)
                .ThenInclude(i => i.Role)
                .ThenInclude(i => i.RoleClaim)
                .SingleOrDefaultAsync();

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByUserId(int id)
        {

            var user = await _context.User
                .Where(u => u.Id == id)
                .Include(a => a.UserRole)
                .SingleOrDefaultAsync();

            return _mapper.Map<UserDTO>(user);

        }

        public async Task<List<UserDTO>> GetUsers()
        {

            var user = await _context.User.Where(a => a.IsActive == true && a.Email != "superadmin@salestracking.com").ToListAsync();
            return _mapper.Map<List<UserDTO>>(user);

        }

        public async Task<int> AddUser(UserDTO user)
        {

            var saveObj = _mapper.Map<User>(user);
            await _context.User.AddAsync(saveObj);
            await _context.SaveChangesAsync();
            return saveObj.Id;

        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            var updateObj = await _context.User.FirstOrDefaultAsync(a => a.Id == user.Id);
            if (updateObj != null)
            {
                _context.Entry(updateObj).Collection(l => l.UserRole).Load();

                if (updateObj.UserRole.Any())
                {
                    _context.RemoveRange(updateObj.UserRole);
                }

                updateObj.FirstName = user.FirstName;
                updateObj.LastName = user.LastName;
                updateObj.PhoneNumber = user.PhoneNumber;
                updateObj.IsActive = user.IsActive;
                updateObj.UpdateBy = user.UpdateBy;
                updateObj.UpdateDate = user.UpdateDate;

                if (user.UserRole.Any())
                {
                    var userroles = _mapper.Map<List<UserRole>>(user.UserRole);
                    updateObj.UserRole = userroles;
                }
                _context.User.Update(updateObj);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<UserDTO>(updateObj);
        }
    }
}
