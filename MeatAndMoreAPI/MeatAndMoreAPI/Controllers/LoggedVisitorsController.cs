using MeatAndMoreAPI.Data;
using MeatAndMoreAPI.DTOs;
using MeatAndMoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Controllers
{
        [Produces("application/json")]
        [Route("api/[controller]")]
        [ApiController]
        public class LoggedVisitorsController : ControllerBase
        {
            private readonly MeatAndMoreContext _context;
            private readonly ILoggedVisitorRepository _loggedVisitorRepository;
            public LoggedVisitorsController(MeatAndMoreContext meatAndMoreContext, ILoggedVisitorRepository loggedVisitorRepository)
            {
                _context = meatAndMoreContext;
                _loggedVisitorRepository = loggedVisitorRepository;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<LoggedVisitorDTO>>> GetLoggedVisitors()
            {
                return Ok(await _loggedVisitorRepository.GetLoggedVisitors().ConfigureAwait(false));
            }


            [HttpGet("{id}")]
            public async Task<ActionResult<LoggedVisitorDTO>> GetLoggedVisitor(int id)
            {
                var subDTO = await _loggedVisitorRepository.GetLoggedVisitor(id).ConfigureAwait(false);

                if (subDTO == null)
                {
                    return NotFound();
                }

                return subDTO;
            }

            [HttpPost]
            public async Task<ActionResult<LoggedVisitorDTO>> AddLoggedVisitor(LoggedVisitorDTO LoggedVisitorDTO)
            {
                var subResult = await _loggedVisitorRepository.AddNewLoggedVisitor(LoggedVisitorDTO).ConfigureAwait(false);

                return CreatedAtAction("GetLoggedVisitor", new { id = subResult.Id }, subResult);
            }

        [HttpPut]
        public async Task<ActionResult<LoggedVisitorDTO>> PutLoggedVisitor(LoggedVisitorDTO LoggedVisitorDTO)
        {
            var subResult = await _loggedVisitorRepository.AddNewLoggedVisitor(LoggedVisitorDTO).ConfigureAwait(false);

            return CreatedAtAction("GetLoggedVisitor", new { id = subResult.Id }, subResult);
        }

        [HttpDelete]
            public async Task<ActionResult<LoggedVisitorDTO>> DeleteLoggedVisitor(int id)
            {
                var subResult = await _loggedVisitorRepository.DeleteLoggedVisitor(id).ConfigureAwait(false);

                if (subResult == null)
                {
                    return NotFound();
                }

                return subResult;
            }

        }
}
