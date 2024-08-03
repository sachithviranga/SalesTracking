using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class RoleDataRepository : IRoleDataRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public RoleDataRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<RoleDTO> GetRoles()
        {
            try
            {
                var roles = _context.Role.Where(a => a.IsActive == true && a.RoleName != "Super Admin").ToList();
                return _mapper.Map<List<RoleDTO>>(roles);
            }
            catch
            {
                throw;

            }
        }

        public int AddRole(RoleDTO role)
        {
            try
            {
                var saveObj = _mapper.Map<Role>(role);
                _context.Role.Add(saveObj);
                _context.SaveChanges();
                return saveObj.Id;
            }
            catch
            {
                throw;

            }

        }

        public RoleDTO UpdateRole(RoleDTO role)
        {
            try
            {
                var updateObj = _context.Role.FirstOrDefault(a => a.Id == role.Id);

                if (updateObj != null)
                {
                    _context.Entry(updateObj).Collection(l => l.RoleClaim).Load();

                    if (updateObj.RoleClaim.Any())
                    {
                        _context.RemoveRange(updateObj.RoleClaim);
                    }

                    updateObj.IsActive = role.IsActive;
                    updateObj.UpdateBy = role.UpdateBy;
                    updateObj.UpdateDate = role.UpdateDate;

                    if (role.RoleClaim.Any())
                    {
                        var roleclaims = _mapper.Map<List<RoleClaim>>(role.RoleClaim);
                        updateObj.RoleClaim = roleclaims;
                    }
                    _context.Role.Update(updateObj);
                    _context.SaveChanges();
                }
                return _mapper.Map<RoleDTO>(updateObj);
            }
            catch
            {
                throw;

            }
        }

        public RoleDTO GetRoleById(int id)
        {
            try
            {
                var roleid = _context.Role.Where(a => a.Id == id)
                     .Include(a => a.RoleClaim)
                     .SingleOrDefault();

                return _mapper.Map<RoleDTO>(roleid);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
