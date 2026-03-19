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
            return await _context.Set<Position>()
                .Select(p => new PositionDto
                {
                    Id = p.Id,
                    TenChucVu = p.TenChucVu,
                    MoTa = p.MoTa,
                    HoatDong = p.HoatDong
                })
                .ToListAsync();
        }

        public async Task<PositionDto?> GetByIdAsync(int id)
        {
            var p = await _context.Set<Position>().FindAsync(id);
            if (p == null) return null;

            return new PositionDto
            {
                Id = p.Id,
                TenChucVu = p.TenChucVu,
                MoTa = p.MoTa,
                HoatDong = p.HoatDong
            };
        }

        public async Task<PositionDto> CreateAsync(PositionDto dto)
        {
            var position = new Position
            {
                TenChucVu = dto.TenChucVu,
                MoTa = dto.MoTa,
                HoatDong = dto.HoatDong
            };

            _context.Set<Position>().Add(position);
            await _context.SaveChangesAsync();

            dto.Id = position.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, PositionDto dto)
        {
            var p = await _context.Set<Position>().FindAsync(id);
            if (p == null) return false;

            p.TenChucVu = dto.TenChucVu;
            p.MoTa = dto.MoTa;
            p.HoatDong = dto.HoatDong;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _context.Set<Position>().FindAsync(id);
            if (p == null) return false;

            _context.Set<Position>().Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}