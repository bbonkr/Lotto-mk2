using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using LottoMk2.Data;
using LottoMk2.Data.Services.Models;
using LottoMk2.Entities;

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

        public Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            return context.Database.MigrateAsync(cancellationToken);
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

            var result = await query
                .OrderByDescending(x => x.LotteryDate)
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

        public async Task<int?> GetLatestRoundAsync(CancellationToken cancellationToken = default)
        {
            int? round = await context.LottoItems.OrderByDescending(x => x.Round)
                .Select(x => x.Round)
                .FirstOrDefaultAsync(cancellationToken);

            return round;
        }

        public Task<LottoModel> AddAsync(LottoModel model, CancellationToken cancellationToken = default)
        {
            var entry = mapper.Map<Lotto>(model);
            var added = context.Add(entry);

            var result = mapper.Map<LottoModel>(added.Entity);

            return Task.FromResult(result);
        }

        public Task<LottoModel> AddAsync(Lotto model, CancellationToken cancellationToken = default)
        {
            var added = context.Add(model);

            var result = mapper.Map<LottoModel>(added.Entity);

            return Task.FromResult(result);
        }

        public async Task<LottoModel> UpdateAsync(LottoModel model, CancellationToken cancellationToken = default)
        {
            var updateCandidate = await context.LottoItems
               .Where(x => x.Round == model.Round)
               .FirstOrDefaultAsync(cancellationToken);

            if (updateCandidate == null)
            {
                throw new Exception("Not found");
            }

            updateCandidate.FirstPrizeWinners = model.FirstPrizeWinners;
            updateCandidate.FirstWinningAmounts = model.FirstWinningAmounts;
            updateCandidate.LotteryDate = model.LotteryDate;
            updateCandidate.Num1 = model.Num1;
            updateCandidate.Num2 = model.Num2;
            updateCandidate.Num3 = model.Num3;
            updateCandidate.Num4 = model.Num4;
            updateCandidate.Num5 = model.Num5;
            updateCandidate.Num6 = model.Num6;
            updateCandidate.NumBonus = model.NumBonus;
            updateCandidate.TotalRewards = model.TotalRewards;

            var updated = context.Update(updateCandidate);

            var result = mapper.Map<LottoModel>(updated.Entity);

            return result;
        }

        public async Task<bool> DeleteAsync(LottoModel model, CancellationToken cancellationToken = default)
        {
            var removeCandidate = await context.LottoItems
                .Where(x => x.Round == model.Round)
                .FirstOrDefaultAsync(cancellationToken);

            if (removeCandidate == null)
            {
                throw new Exception("Not found");
            }

            context.Remove(removeCandidate);

            return true;
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync(cancellationToken);
        }

        private IQueryable<Entities.Lotto> AddNumberCriteria(IQueryable<Entities.Lotto> query, int? number, bool? includesBonusNumber)
        {
            if (!number.HasValue)
            {
                return query;
            }

            //return query.Where(x => new[] { x.Num1, x.Num2, x.Num3, x.Num4, x.Num5, x.Num6, includesBonusNumber.HasValue && includesBonusNumber.Value ? x.NumBonus : 0 }.Contains(number.Value));
            return query.Where(x =>
            x.Num1 == number.Value ||
            x.Num2 == number.Value ||
            x.Num3 == number.Value ||
            x.Num4 == number.Value ||
            x.Num5 == number.Value ||
            x.Num6 == number.Value ||
            (includesBonusNumber.HasValue && includesBonusNumber.Value ? x.NumBonus == number.Value : false)
            );
        }

        private readonly AppDbContext context;
        private readonly IMapper mapper;
    }
}
