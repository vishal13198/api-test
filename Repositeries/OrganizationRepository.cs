using Microsoft.EntityFrameworkCore;
using ShuttleInfraAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Organizations>> GetAllOrganizationsAsync()
    {
        return await _context.Organizations.ToListAsync();
    }

    public async Task<Organizations> GetOrganizationsByIdAsync(int id)
    {
        var project = await _context.Organizations.FindAsync(id);
        if (project == null)
        {
            return new Organizations();
        }
        return project;
    }

    public async Task<int> AddOrganizationsAsync(Organizations organization)
    {
        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();
        return organization.Id;
    }

    public async Task UpdateOrganizationsAsync(Organizations organization)
    {
        _context.Entry(organization).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrganizationsAsync(int id)
    {
        var organization = await _context.Organizations.FindAsync(id);
        if (organization != null)
        {
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
        }
    }


    //public Task<List<Organization>> GetOrganizationsWithProjectsAsync()
    //{
    //    throw new NotImplementedException();
    //}
}