using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserManager.Services.DTO
{
    public class UserDTO
    {
        public UserDTO(long id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
        public UserDTO()
        {

        }
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        [JsonIgnore]
        public string Password { get;  set; }
    }
}
