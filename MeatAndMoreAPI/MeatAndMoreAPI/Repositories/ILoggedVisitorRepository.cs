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
        Task<string> PutLoggedVisitor(int id);
        Task<LoggedVisitorDTO> DeleteLoggedVisitor(int id);
    }
}
