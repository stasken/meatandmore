using MeatAndMoreAPI.Data;
using MeatAndMoreAPI.DTOs;
using MeatAndMoreAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MeatAndMoreAPI.Data.Enumerations;

namespace MeatAndMoreAPI.Controllers
{
        [Authorize(AuthenticationSchemes = "Bearer")]
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
            if (User.IsInRole("admin"))
            {
                return Ok(await _loggedVisitorRepository.GetLoggedVisitors().ConfigureAwait(false));
            }
            return Forbid(JwtBearerDefaults.AuthenticationScheme);
        }

        [AllowAnonymous]
        [HttpGet("logout")]
        public async Task<ActionResult<IEnumerable<LoggedVisitorDTO>>> GetLoggedVisitorsToLogOut()
        {
                return Ok(await _loggedVisitorRepository.GetLoggedVisitors().ConfigureAwait(false));
        }

        [AllowAnonymous]
        [HttpGet("types")]
        public Array GetTypeVisitors()
            {
                string[] types = new string[3];
                foreach (int i in Enum.GetValues(typeof(Enumerations.TypeVisit)))
                {
                    string stringValue = Enum.GetName(typeof(Enumerations.TypeVisit), i);
                    types[i] = stringValue;
                }
                return types;
            }


        [AllowAnonymous]
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

        [AllowAnonymous]
            [HttpPost]
        public async Task<ActionResult<LoggedVisitorDTO>> AddLoggedVisitor(LoggedVisitorDTO LoggedVisitorDTO)
            {
                var subResult = await _loggedVisitorRepository.AddNewLoggedVisitor(LoggedVisitorDTO).ConfigureAwait(false);

                return CreatedAtAction("GetLoggedVisitor", new { id = subResult.Id }, subResult);
            }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<string> PutLoggedVisitor(int id)
        {
            var subResult = await _loggedVisitorRepository.PutLoggedVisitor(id).ConfigureAwait(false);

            return subResult;
        }

        [AllowAnonymous]
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
