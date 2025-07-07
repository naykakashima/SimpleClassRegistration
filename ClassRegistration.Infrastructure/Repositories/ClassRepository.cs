using ClassRegistration.Domain.Models;
using ClassRegistration.Infrastructure.Database;
using ClassRegistration.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ClassRegistration.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ClassDbContext _context;

        public ClassRepository(ClassDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Class @class)
        {
            _context.Add(@class);
            await _context.SaveChangesAsync();
        }

        public async Task<Class?> FindClassByTitle(string title)
        {
            return await _context.Set<Class>().FirstOrDefaultAsync(c => c.ClassName.ToLower() == title.ToLower());
        }

        public async Task<IEnumerable<Class>> GetClassesAsync()
        {
            return await _context.Set<Class>().ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetAvailableClassesAsync()
        {
            var availableClasses = await _context.Set<Class>().Where(c => c.IsClassFull == false).ToListAsync();
            return availableClasses;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateClassAsync(
                Guid? ClassID = null,
                string? ClassName = null,
                string? ClassType = null,
                int? Occupancy = null 
            )
        {

            if (ClassID == null && ClassName == null)
            {
                throw new ArgumentException("Either ClassID or ClassName must be provided");
            }

            Class? @class = ClassID.HasValue
                ? await _context.Classes.FindAsync(ClassID.Value)
                : await FindClassByTitle(ClassName!);

            if (@class == null)
            {
                return false;
            }

            if (ClassName != null)
                @class.ClassName = ClassName;

            if (ClassType !=  null)
                @class.ClassType = ClassType;

            if (Occupancy.HasValue)
                @class.Occupancy = Occupancy.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClassAsync(string ClassName, string ClassType, Guid ClassID, int MaxOccupancy)
        {
            var @class = await _context.Classes.FindAsync(ClassID);
            if (@class == null)
            {
                return false;
            }

            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
