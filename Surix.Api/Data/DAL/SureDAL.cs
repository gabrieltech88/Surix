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
        private readonly UserManager<User> _userManager;

        public SureDAL(SurixContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
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
            var name = _userManager.Users.Where(u => u.Id == id).Select(u => u.Name).FirstOrDefault();

            var totalCount = await query.CountAsync();

            var sures = await _context.Sures
                .Where(s => s.UserId == id)
                .OrderByDescending(s => s.Id) // Ordena por data, do mais recente
                .Skip((pageNumber - 1) * pageSize) // Pula as páginas anteriores
                .Take(pageSize) // Pega o tamanho da página
                .ToListAsync();

            return new PagedResult<Sure>
            {
                Sures = sures,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Name = name
            };


        }

        public async Task<List<RoiPerDay>> GetRoi(string id)
        {
            int mesAtual = DateTime.Now.Month;   // retorna 1 a 12
            int anoAtual = DateTime.Now.Year;    // retorna o ano com 4 dígitos, ex: 2025

            var somaRoiPorDia = await _context.Sures
                .Where(s => s.UserId == id
                    && s.Date.Year == anoAtual
                    && s.Date.Month == mesAtual)
                .GroupBy(sure => new { sure.Date.Day, sure.Date.Month })
                .Select(s => new RoiPerDay
                {
                    Dia = s.Key.Day,
                    Mes = s.Key.Month,
                    SomaRoi = s.Sum(x => x.ROI) ?? 0
                })
                .OrderBy(x => x.Dia)
                .ToListAsync();

            return somaRoiPorDia;

        }

        public async Task<object> GetStats(string id)
        {
            int mesAtual = DateTime.Now.Month;   // retorna 1 a 12
            int anoAtual = DateTime.Now.Year;    // retorna o ano com 4 dígitos, ex: 2025

            var totaisMes = await _context.Sures
                .Where(s => s.UserId == id
                && s.Date.Year == anoAtual
                && s.Date.Month == mesAtual)
                .GroupBy(s => 1) // Agrupa tudo junto
                .Select(g => new
                {
                    RoiMensal = g.Sum(x => x.ROI) ?? 0,
                    LucroMensal = g.Sum(x => x.Lucro) ?? 0,
                    StakeTotal =  g.Sum(x => x.Stake),
                    QuantidadeSures = g.Count()
                })
                .FirstOrDefaultAsync();

            return totaisMes;
        }


    }
}