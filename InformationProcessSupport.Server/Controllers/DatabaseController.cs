using InformationProcessSupport.Data;
using InformationProcessSupport.Server.Dtos;
using InformationProcessSupport.Server.Extensions;
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

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUserCollectionAsync()
        {
            try
            {
                var users = await _context.UserEntities.Include(x => x.GroupEntity).ToListAsync();

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
    }
}
