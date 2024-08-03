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

        public UserDTO GetUserByUserName(string email)
        {
            try
            {
                var user = _context.User
                    .Where(u => u.Email.ToLower() == email.ToLower())
                    .Include(a => a.UserRole)
                    .ThenInclude(i => i.Role)
                    .ThenInclude(i => i.RoleClaim)
                    .SingleOrDefault();

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserDTO GetUserByUserId(int id)
        {
            try
            {
                var user = _context.User
                    .Where(u => u.Id == id)
                    .Include(a => a.UserRole)
                    .SingleOrDefault();

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserDTO> GetUsers()
        {
            try
            {
                var user = _context.User.Where(a => a.IsActive == true && a.Email != "superadmin@salestracking.com").ToList();
                return _mapper.Map<List<UserDTO>>(user);

            }
            catch
            {
                throw;
            }
        }

        public int AddUser(UserDTO user)
        {
            try
            {
                var saveObj = _mapper.Map<User>(user);
                _context.User.Add(saveObj);
                _context.SaveChanges();
                return saveObj.Id;
            }
            catch
            {
                throw;

            }

        }

        public UserDTO UpdateUser(UserDTO user)
        {
            try
            {
                var updateObj = _context.User.FirstOrDefault(a => a.Id == user.Id);



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
                        //_context.UserRole.AddRange(userroles);
                        //foreach (var roledetail in userroles)
                        //{
                        //    updateObj.UserRole.Add(roledetail);
                        //}  
                    }
                    _context.User.Update(updateObj);
                    _context.SaveChanges();
                }
                return _mapper.Map<UserDTO>(updateObj);
            }
            catch
            {
                throw;

            }
        }
    }
}
