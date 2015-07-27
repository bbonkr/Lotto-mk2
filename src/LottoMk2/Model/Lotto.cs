using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LottoMk2.Model
{
    [Serializable]
    [DataContract]
    public class Lotto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Dt { get; set; }

        [DataMember]
        public int Num1 { get; set; }

        [DataMember]
        public int Num2 { get; set; }

        [DataMember]
        public int Num3 { get; set; }

        [DataMember]
        public int Num4 { get; set; }

        [DataMember]
        public int Num5 { get; set; }

        [DataMember]
        public int Num6 { get; set; }

        [DataMember]
        public int NumBonus { get; set; }
    }
}