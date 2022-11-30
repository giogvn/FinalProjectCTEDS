using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SaveFirst.Models
{
    public class FinancialProduct
    {
        public string? Name { get; set; }
        public float Price { get; set; }

        public static string PriceSource = "https://www.tesourodireto.com.br/json/br/com/b3/tesourodireto/service/api/treasurybondsinfo.json";

    }
}
