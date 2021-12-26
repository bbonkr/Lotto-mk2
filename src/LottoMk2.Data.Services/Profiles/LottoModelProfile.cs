using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace LottoMk2.Data.Services.Profiles;

public class LottoModelProfile : Profile
{
    public LottoModelProfile()
    {
        CreateMap<LottoMk2.Data.Services.Models.LottoModel, LottoMk2.Entities.Lotto>();
        CreateMap<LottoMk2.Entities.Lotto, LottoMk2.Data.Services.Models.LottoModel>();
    }
}
