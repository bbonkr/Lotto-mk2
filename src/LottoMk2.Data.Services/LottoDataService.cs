using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using LottoMk2.Data;
using LottoMk2.Data.Services.Models;

using Microsoft.EntityFrameworkCore;

namespace LottoMk2.Data.Services
{
    public class LottoDataService
    {
        public LottoDataService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<LottoModel>> GetAllAsync(
            DateTime? start,
            DateTime? end,
            int? round,
            IEnumerable<int?>? numbers,
            bool? includesBonusNumber = false,
            CancellationToken cancellationToken = default)
        {
            if (context.LottoItems == null)
            {
                return new List<LottoModel>();
            }

            var query = context.LottoItems.AsNoTracking();

            if (start.HasValue)
            {
                query = query.Where(x => x.LotteryDate >= start.Value);
            }

            if (end.HasValue)
            {
                query = query.Where(x => x.LotteryDate <= end.Value.AddDays(1).Date);
            }

            if (round.HasValue)
            {
                query = query.Where(x => x.Round == round.Value);
            }

            if (numbers != null)
            {
                foreach (var number in numbers)
                {
                    query = AddNumberCriteria(query, number, includesBonusNumber);
                }
            }

            var result = await query.OrderBy(x => x.LotteryDate)
                .Select(x => mapper.Map<LottoModel>(x))
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<LottoModel?> GetByRoundAsync(int round, CancellationToken cancellationToken = default)
        {
            if (context.LottoItems == null)
            {
                return null;
            }

            var query = context.LottoItems.AsNoTracking();

            var result = await query.Where(x => x.Round == round)
                .Select(x => mapper.Map<LottoModel>(x))
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        public Task<LottoModel> AddAsync(LottoModel model, CancellationToken cancellationToken = default) { throw new NotImplementedException(); }

        public Task<LottoModel> UpdateAsync(LottoModel model, CancellationToken cancellationToken = default) { throw new NotImplementedException(); }

        public Task<bool> DeleteAsync(LottoModel model, CancellationToken cancellationToken = default) { throw new NotImplementedException(); }

        private IQueryable<Entities.Lotto> AddNumberCriteria(IQueryable<Entities.Lotto> query, int? number, bool? includesBonusNumber)
        {
            if (!number.HasValue)
            {
                return query;
            }

            return query.Where(x => new[] { x.Num1, x.Num2, x.Num3, x.Num4, x.Num5, x.Num6, includesBonusNumber.HasValue && includesBonusNumber.Value ? x.NumBonus : 0 }.Contains(number.Value));
        }

        private readonly AppDbContext context;
        private readonly IMapper mapper;
    }
}
