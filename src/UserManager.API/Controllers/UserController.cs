using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.API.Utilities;
using UserManager.API.ViewModels;
using UserManager.Core.Exceptions;
using UserManager.Infra.Interfaces;
using UserManager.Services.DTO;
using UserManager.Services.Interfaces;

namespace UserManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        #region Add
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);
                var userCreated = await _userService.Create(userDTO);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Usu�rio criado com sucesso!",
                    Data = userCreated
                });
            } catch(DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message,e.Errors));
            } catch(Exception)
            {
                return BadRequest(Responses.ApplicationErrorMessage());
            }
        }
        #endregion

        #region Edit
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UpdatedUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);
                var userUpdated = await _userService.Update(userDTO);
                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Usu�rio aualizado com sucesso!",
                    Data = userUpdated
                });
            } catch(DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message,e.Errors));
            } catch(Exception)
            {
                return BadRequest(Responses.ApplicationErrorMessage());
            }
        }
        #endregion

        #region Get All
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(new ResultViewModel
                {
                    Data = users,
                    Success = true,
                    Message = "Lista de usu�rios"
                });
            }
            catch (Exception)
            {
                return BadRequest(Responses.ApplicationErrorMessage());
            }

        }
        #endregion

        #region Get By Id
        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            try { 
                var user = await _userService.GetById(id);
                return Ok(new ResultViewModel {
                    Data = user,
                    Success = true,
                    Message = user is not null ? "Usu�rios encontrado" : "Usu�rio n�o encontrado"
                });
            }
            catch (Exception)
            {
                return BadRequest(Responses.ApplicationErrorMessage());
            }
        }
        #endregion

        #region Search By Email
        [HttpGet("search-by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                var users = await _userService.SearchByEmail(email);
                return Ok(new ResultViewModel
                {
                    Data = users,
                    Success = true,
                    Message = users.Any() ? "Usu�rios encontrado" : "Usu�rio n�o encontrado"
                });
            }
            catch (Exception)
            {
                return BadRequest(Responses.ApplicationErrorMessage());
            }
        }
        #endregion

        #region Search By Name
        [HttpGet("search-by-name")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            try
            {
                var users = await _userService.SearchByName(name);
                return Ok(new ResultViewModel
                {
                    Data = users,
                    Success = true,
                    Message = users.Any() ? "Usu�rios encontrado" : "Usu�rio n�o encontrado"
                });
            }
            catch (Exception)
            {
                return BadRequest(Responses.ApplicationErrorMessage());
            }
        }
        #endregion

        #region Delete 
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove(long id)
        {
           try {
                await _userService.Remove(id);
                return Ok(new ResultViewModel
                {
                    Data = null,
                    Success = true,
                    Message = "Usu�rio removido com sucesso!"
                });
            }
            catch (DomainException e)
            {
                return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return BadRequest(Responses.ApplicationErrorMessage());
            }
        }
        #endregion 
    }
}