using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoMk2.Data.Services.Models
{
    public class LottoModel
    {
        /// <summary>
        /// 회차
        /// </summary>
        public int Round { get; set; }

        /// <summary>
        /// 추첨일
        /// </summary>
        public DateTime LotteryDate { get; set; }

        public int Num1 { get; set; }

        public int Num2 { get; set; }

        public int Num3 { get; set; }

        public int Num4 { get; set; }

        public int Num5 { get; set; }

        public int Num6 { get; set; }

        public int NumBonus { get; set; }

        /// <summary>
        /// 전체 판매금액
        /// </summary>
        public double TotalRewards { get; set; }

        /// <summary>
        /// 1등 당첨자 수
        /// </summary>
        public int FirstPrizeWinners { get; set; }

        /// <summary>
        /// 1등 당첨금
        /// </summary>
        public int FirstWinningAmounts { get; set; }
    }
}
