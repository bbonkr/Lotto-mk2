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
        request.Headers.AcceptEncoding.Clear();
        request.Headers.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("utf-8"));
        request.Headers.Accept.Clear();
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        
        try
        {
            var response = await httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            if (response.Content == null)
            {
                throw new Exception("Response body is empty");
            }

            var jsonBuffers = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            var json = Encoding.GetEncoding("iso-8859-1").GetString(jsonBuffers);
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
