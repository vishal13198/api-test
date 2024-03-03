using Microsoft.EntityFrameworkCore;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrganizationProjectRepository : IOrganizationProjectRepository
{
    private readonly AppDbContext _context;

    public OrganizationProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrganizationProject>> GetAllAsync()
    {
        return await _context.organizations_projects.ToListAsync();
    }

    public async Task<OrganizationProject> GetByIdAsync(int id)
    {
        return await _context.organizations_projects.FindAsync(id);
    }

    public async Task AddAsync(OrganizationProject entity)
    {
        await _context.organizations_projects.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrganizationProject entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.organizations_projects.FindAsync(id);
        if (entity != null)
        {
            _context.organizations_projects.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<OrganizationProject>> GetProjectsByOrgIdAsync(int orgId)
    {
        return await _context.organizations_projects
            .Where(op => op.OrgId == orgId)
            .ToListAsync();
    }
}
