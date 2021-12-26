using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using LottoMk2.Data.Services;
using LottoMk2.Entities;
using LottoMk2.Services.LottoService;

namespace LottoMk2.App.Features.UpdateData;

public class UpdateDataService
{
    public UpdateDataService(LottoService lottoService, LottoDataService dataService, IMapper mapper)
    {
        this.lottoService = lottoService;
        this.dataService = dataService;
        this.mapper = mapper;
    }

    public async Task UpdateAsync(CancellationToken cancellationToken =default)
    {
        var latestRoundValue = await dataService.GetLatestRoundAsync();
        var latestRound = (latestRoundValue ?? 0) + 1;
        var needCommit = false;

        for (var i = latestRound; true; i++)
        {
            var result = await lottoService.GetLottoDataAsync(i);
            if (result.IsSucceed)
            {
                var entry = mapper.Map<Lotto>(result);
                await dataService.AddAsync(entry, cancellationToken);
                needCommit = true;
            }
            else
            {
                break;
            }
        }

        if (needCommit)
        {
            await dataService.SaveAsync(cancellationToken);
        }
    }

    private readonly LottoService lottoService;
    private readonly LottoDataService dataService;
    private readonly IMapper mapper;
}
