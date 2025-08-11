using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Surix.Api.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Surix.Api.Data.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

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

        public async Task<string> CreateSure(SureCreateRequest dto)
        {
            return "ok";
        }
        
    }
}