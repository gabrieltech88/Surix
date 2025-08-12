using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Surix.Api.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Surix.Api.Data.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Surix.Api.Data.DAL
{
    public class SureDAL
    {
        private readonly SurixContext _context;
        private IMapper _mapper;

        public SureDAL(SurixContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateSure(SureCreateRequest dto, string id)
        {
            var sure = _mapper.Map<Sure>(dto);

            List<double> odds = new List<double> { dto.oddA, dto.oddB };

            double sumInverse = odds.Sum(o => 1.0 / o);
            double roi = (1.0 / sumInverse - 1.0) * 100;

            double lucro = dto.Stake * roi / 100.00;

            sure.ROI = roi;
            sure.Lucro = lucro;
            sure.UserId = id;

            _context.Sures.Add(sure);
            await _context.SaveChangesAsync();

            return sure.Id;
        }

        public async Task<PagedResult<Sure>> GetSures(string id, int pageNumber, int pageSize)
        {

            var query = _context.Sures.Where(s => s.UserId == id);
            var totalCount = await query.CountAsync();

            var sures = await _context.Sures
                .Where(s => s.UserId == id)
                .OrderByDescending(s => s.Date) // Ordena por data, do mais recente
                .Skip((pageNumber - 1) * pageSize) // Pula as páginas anteriores
                .Take(pageSize) // Pega o tamanho da página
                .ToListAsync();

            return new PagedResult<Sure>
            {
                Sures = sures,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


        }

    }
}