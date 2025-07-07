using ClassRegistration.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegistration.Infrastructure.Interfaces
{
    public interface IClassRepository
    {
        Task AddAsync(Class @class);
        Task<Class?> FindClassByTitle(string title);
        Task<IEnumerable<Class>> GetClassesAsync();
        Task SaveChangesAsync();
        Task<bool> UpdateClassAsync(
                Guid? ClassID = null,
                string? ClassName = null,
                string? ClassType = null,
                int? MaxOccupancy = null
            );

        Task<bool> DeleteClassAsync(string ClassName, string ClassType, Guid ClassID, int MaxOccupancy);
        Task<IEnumerable<Class>> GetAvailableClassesAsync();
    }
}
