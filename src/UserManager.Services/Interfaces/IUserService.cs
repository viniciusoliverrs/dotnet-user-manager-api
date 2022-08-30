using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.Services.DTO;

namespace UserManager.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Create(UserDTO user);
        Task<UserDTO> Update(UserDTO user);
        Task Remove(long id);
        Task<UserDTO> GetById(long id);
        Task<List<UserDTO>> SearchByName(string name);
        Task<List<UserDTO>> SearchByEmail(string email);
        Task<UserDTO> GetByEmail(string email);
        Task<List<UserDTO>> GetAll();
    }
}
