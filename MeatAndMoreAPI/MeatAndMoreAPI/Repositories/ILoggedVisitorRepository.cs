using MeatAndMoreAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Repositories
{
    public interface ILoggedVisitorRepository
    {
        Task<LoggedVisitorDTO> GetLoggedVisitor(int id);
        Task<IEnumerable<LoggedVisitorDTO>> GetLoggedVisitors();
        Task<LoggedVisitorDTO> AddNewLoggedVisitor(LoggedVisitorDTO LoggedVisitorDTO);
        Task<LoggedVisitorDTO> PutLoggedVisitor(int id, LoggedVisitorDTO LoggedVisitorDTO);
        Task<LoggedVisitorDTO> DeleteLoggedVisitor(int id);
    }
}
