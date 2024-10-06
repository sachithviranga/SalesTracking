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

        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _context.Role.Where(a => a.IsActive == true && a.RoleName != "Super Admin").ToListAsync();
            return _mapper.Map<List<RoleDTO>>(roles);
        }

        public async Task<int> AddRole(RoleDTO role)
        {
            var saveObj = _mapper.Map<Role>(role);
            await _context.Role.AddAsync(saveObj);
            await _context.SaveChangesAsync();
            return saveObj.Id;
        }

        public async Task<RoleDTO> UpdateRole(RoleDTO role)
        {
            var updateObj = await _context.Role.FirstOrDefaultAsync(a => a.Id == role.Id);

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
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<RoleDTO>(updateObj);
        }

        public async Task<RoleDTO> GetRoleById(int id)
        {
            var roleid = await _context.Role.Where(a => a.Id == id)
                 .Include(a => a.RoleClaim)
                 .SingleOrDefaultAsync();

            return _mapper.Map<RoleDTO>(roleid);
        }

    }
}
