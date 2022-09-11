using AutoMapper;
using EscNet.Cryptography.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.Core.Exceptions;
using UserManager.Domain.Entities;
using UserManager.Infra.Interfaces;
using UserManager.Services.DTO;
using UserManager.Services.Interfaces;

namespace UserManager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRijndaelCryptography _crypt;

        public UserService(IMapper mapper, IUserRepository userRepository,IRijndaelCryptography crypt)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _crypt = crypt;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);
            if (userExists is not null) throw new DomainException("Email já usado!");
            User user = _mapper.Map<User>(userDTO);

            user.Validate();
            user.ChangePassword(_crypt.Encrypt(user.Password));
            var userCreated  = await _userRepository.Create(user);
            var result = _mapper.Map<UserDTO>(userCreated);
            return result;
        }

        public async Task<List<UserDTO>> GetAll()
        {
           List<User> users = await _userRepository.GetAll();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var users = await _userRepository.GetByEmail(email);
            return _mapper.Map<UserDTO>(users);
        }

        public async Task<UserDTO> GetById(long id)
        {
            var users = await _userRepository.GetById(id);
            return _mapper.Map<UserDTO>(users);
        }

        public async Task Remove(long id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            List<User> users = await _userRepository.SearchByEmail(email);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            List<User> users = await _userRepository.SearchByName(name);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetById(userDTO.Id);
            if (userExists is null) throw new DomainException("Usuário não encontrado!");

            User user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_crypt.Encrypt(user.Password));
            var userCreated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userCreated);
        }
    }
}
