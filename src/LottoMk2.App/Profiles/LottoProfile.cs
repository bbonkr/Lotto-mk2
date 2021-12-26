using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace LottoMk2.App.Profiles;

public class LottoProfile :Profile
{
    public LottoProfile()
    {
         CreateMap<LottoMk2.Services.LottoService.LottoServiceResponseModel, LottoMk2.Entities.Lotto>();
    }
}
