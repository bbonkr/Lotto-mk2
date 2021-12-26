using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace LottoMk2.Services.LottoService;

public class LottoService
{
    public const string Endporint = "https://www.dhlottery.co.kr/common.do?method=getLottoNumber&drwNo=";

    public LottoService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<LottoServiceResponseModel> GetLottoDataAsync(int round, CancellationToken cancellationToken = default)
    {
        var url = $"{Endporint}{round}";

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

        try
        {
            var response = await httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            if (response.Content == null)
            {
                throw new Exception("Response body is empty");
            }

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var responseModel = JsonSerializer.Deserialize<LottoServiceResponseModel>(json);

            if (responseModel == null)
            {
                throw new Exception("Response body could not recognize");
            }

            return responseModel;
        }
        catch (Exception ex)
        {
            return new LottoServiceResponseModel
            {
                ReturnValue = "failure",
                Extra = ex,
            };
        }
    }

    private readonly HttpClient httpClient;
}
