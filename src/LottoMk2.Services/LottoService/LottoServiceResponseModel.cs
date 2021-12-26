using System.Text.Json.Serialization;

namespace LottoMk2.Services.LottoService
{
    public class LottoServiceResponseModel
    {
        [JsonPropertyName("returnValue")]
        public string? ReturnValue { get; set; }

        /// <summary>
        /// 회차
        /// </summary>
        [JsonPropertyName("drwNo")]
        public int Round { get; set; }

        /// <summary>
        /// 추첨일
        /// </summary>
        [JsonPropertyName("drwNoDate")]
        public string? LotteryDate { get; set; }

        [JsonPropertyName("drwtNo1")]
        public int Num1 { get; set; }

        [JsonPropertyName("drwtNo2")]
        public int Num2 { get; set; }

        [JsonPropertyName("drwtNo3")]
        public int Num3 { get; set; }

        [JsonPropertyName("drwtNo4")]
        public int Num4 { get; set; }

        [JsonPropertyName("drwtNo5")]
        public int Num5 { get; set; }
        [JsonPropertyName("drwtNo6")]
        public int Num6 { get; set; }

        [JsonPropertyName("bnusNo")]
        public int NumBonus { get; set; }

        /// <summary>
        /// 전체 판매금액
        /// </summary>
        [JsonPropertyName("totSellamnt")]
        public double TotalRewards { get; set; }

        /// <summary>
        /// 1등 당첨자 수
        /// </summary>
        [JsonPropertyName("firstPrzwnerCo")]
        public int FirstPrizeWinners { get; set; }

        /// <summary>
        /// 1등 당첨금
        /// </summary>
        [JsonPropertyName("firstAccumamnt")]
        public double FirstWinningAmounts { get; set; }

        public bool IsSucceed => ReturnValue != null && ReturnValue.Equals("success", StringComparison.OrdinalIgnoreCase);

        public object? Extra { get; set; }
    }
}
