using HRM.Data;
using HRM.DTOs;
using HRM.Models;
using HRM.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services
{
    public class PositionService : IPositionService
    {
        private readonly AppDbContext _context;

        public PositionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PositionDto>> GetAllAsync()
        {
            return await _context.Positions
                .Select(p => new PositionDto
                {
                    Id = p.Id,
                    PositionName = p.PositionName,
                    Description = p.Description,
                    IsActive = p.IsActive
                })
                .ToListAsync();
        }

        public async Task<PositionDto?> GetByIdAsync(int id)
        {
            var p = await _context.Positions.FindAsync(id);
            if (p == null) return null;

            return new PositionDto
            {
                Id = p.Id,
                PositionName = p.PositionName,
                Description = p.Description,
                IsActive = p.IsActive
            };
        }

        public async Task<PositionDto> CreateAsync(PositionDto dto)
        {
            var position = new Position
            {
                PositionName = dto.PositionName,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            dto.Id = position.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, PositionDto dto)
        {
            var p = await _context.Positions.FindAsync(id);
            if (p == null) return false;

            p.PositionName = dto.PositionName;
            p.Description = dto.Description;
            p.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _context.Positions.FindAsync(id);
            if (p == null) return false;

            _context.Positions.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}