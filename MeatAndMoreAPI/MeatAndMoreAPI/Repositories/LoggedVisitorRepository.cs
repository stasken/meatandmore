using MeatAndMoreAPI.Data;
using MeatAndMoreAPI.DTOs;
using MeatAndMoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Repositories
{
    public class LoggedVisitorRepository : ILoggedVisitorRepository
    {
        private readonly MeatAndMoreContext _context;

        public LoggedVisitorRepository(MeatAndMoreContext meatAndMoreContext)
        {
            _context = meatAndMoreContext;
        }

        public async Task<LoggedVisitorDTO> AddNewLoggedVisitor(LoggedVisitorDTO loggedVisitorDTO)
        {
            if (loggedVisitorDTO == null) { throw new ArgumentNullException(nameof(loggedVisitorDTO)); }

            var visitorResult = _context.LoggedVisitors.Add(new LoggedVisitor
            {
                FirstName = loggedVisitorDTO.FirstName,
                LastName = loggedVisitorDTO.LastName,
                TypeVisit = loggedVisitorDTO.TypeVisit,
                CompanyName = loggedVisitorDTO.CompanyName,
                LicensePlate = loggedVisitorDTO.LicensePlate,
                LoggedIn = loggedVisitorDTO.LoggedIn,
                LoggedOut = loggedVisitorDTO.LoggedOut,
                InsideBuilding = true
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);

            loggedVisitorDTO.Id = visitorResult.Entity.Id;

            return loggedVisitorDTO;
        }

        public async Task<LoggedVisitorDTO> DeleteLoggedVisitor(int id)
        {
            var visitor = await _context.LoggedVisitors.FirstOrDefaultAsync(lv => lv.Id == id).ConfigureAwait(false);
            if (visitor == null)
            {
                return null;
            }

            _context.LoggedVisitors.Remove(visitor);

            await _context.SaveChangesAsync();

            return new LoggedVisitorDTO()
            {
                FirstName = visitor.FirstName,
                LastName = visitor.LastName,
                TypeVisit = visitor.TypeVisit,
                CompanyName = visitor.CompanyName,
                LicensePlate = visitor.LicensePlate,
                LoggedIn = visitor.LoggedIn,
                LoggedOut = visitor.LoggedOut,
                InsideBuilding = visitor.InsideBuilding
            };
        }

        public async Task<LoggedVisitorDTO> GetLoggedVisitor(int id)
        {
            return await _context.LoggedVisitors.Select(lv => new LoggedVisitorDTO
            {
                Id = lv.Id,
                FirstName = lv.FirstName,
                LastName = lv.LastName,
                TypeVisit = lv.TypeVisit,
                LicensePlate = lv.LicensePlate,
                CompanyName = lv.CompanyName,
                LoggedIn = lv.LoggedIn,
                LoggedOut = lv.LoggedOut,
                InsideBuilding = lv.InsideBuilding
            })
                .AsNoTracking()
                .FirstOrDefaultAsync(lv => lv.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<LoggedVisitorDTO>> GetLoggedVisitors()
        {
            return await _context.LoggedVisitors.Select(lv => new LoggedVisitorDTO
            {
                Id = lv.Id,
                FirstName = lv.FirstName,
                LastName = lv.LastName,
                TypeVisit = lv.TypeVisit,
                LicensePlate = lv.LicensePlate,
                CompanyName = lv.CompanyName,
                LoggedIn = lv.LoggedIn,
                LoggedOut = lv.LoggedOut,
                InsideBuilding = lv.InsideBuilding
            })
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<LoggedVisitorDTO>> GetLoggedVisitorsToLogOut()
        {
            return await _context.LoggedVisitors.Select(lv => new LoggedVisitorDTO
            {
                Id = lv.Id,
                FirstName = lv.FirstName,
                LastName = lv.LastName,
                InsideBuilding = lv.InsideBuilding
            })
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<string> PutLoggedVisitor(int id)
        {
            try
            {
                LoggedVisitor loggedVisitor = await _context.LoggedVisitors.FirstOrDefaultAsync(lv => lv.Id == id);
                loggedVisitor.LoggedOut = DateTime.Now;
                loggedVisitor.InsideBuilding = false;

                await _context.SaveChangesAsync().ConfigureAwait(false);

            } catch (DbUpdateConcurrencyException)
            {
                if (!LoggedVisitorExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return "logged Out";
        }
        private bool LoggedVisitorExists(int id)
        {
            return _context.LoggedVisitors.Any(e => e.Id == id);
        }
    }
}
