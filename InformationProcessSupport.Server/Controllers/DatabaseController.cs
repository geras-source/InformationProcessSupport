using InformationProcessSupport.Data;
using InformationProcessSupport.Data.Models;
using InformationProcessSupport.Server.Dtos;
using InformationProcessSupport.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InformationProcessSupport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DatabaseController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUserCollectionAsync()
        {
            try
            {
                var users = await _context.UserEntities.AsNoTracking().Include(x => x.GroupEntity).ToListAsync();

                if (users == null)
                {
                    return NotFound();
                }

                var usersDto = users.ConvertToDto();

                return Ok(usersDto);
            }
            catch (Exception e) // TODO: сделать логгер
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка при извлечении данных из базы данных");
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateUsersAsync([FromBody] IEnumerable<UsersDto> users)
        {
            if (!users.Any())
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Передан пустой список");
            }

            try
            {
                foreach (var user in users)
                {
                    var userFromDb = await _context.UserEntities.FirstOrDefaultAsync(x => x.UserId == user.UserId);

                    if (userFromDb == null) continue;

                    userFromDb.GroupId = await GetGroupIdByGroupName(groupName: user.GroupName);
                    userFromDb.Roles = user.Roles;
                    userFromDb.Nickname = user.Nickname;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка при попытке обновления данных.");
            }

            return Ok($"Данные успешно обновлены ({users.Count()}).");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteUsersAsync([FromBody] IEnumerable<UsersDto> users)
        {
            if (!users.Any())
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Передан пустой список");
            }

            try
            {
                var usersModel = users.Select(user => new UserModel
                {
                    UserId = user.UserId
                });

                _context.UserEntities.RemoveRange(usersModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка при попытке удаления данных.");
            }

            return Ok($"Данные успешно удалены ({users.Count()}).");
        }

       

        private async Task<int?> GetGroupIdByGroupName(string? groupName)
        {
            var groupEntity = await _context.GroupEntities.FirstOrDefaultAsync(x => x.GroupName == groupName);

            return groupEntity?.GroupId ?? null;
        }
    }
}
