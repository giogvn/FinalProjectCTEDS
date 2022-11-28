using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SaveFirst.Repositories
{
    public class FinancialProductRepository : IRecord<FinancialProduct>
    { 
        public void Delete(int RecordId) => throw new NotImplementedException();

        public void Create(FinancialProduct newRecord) => throw new NotImplementedException();

        public List<FinancialProduct> FindAllFromSaver(int Id) => throw new NotImplementedException();

        public List<FinancialProduct> ReadAll()
        {
            List<FinancialProduct> list = new();

            using (WebClient wc = new())
            {
                var json = wc.DownloadString(FinancialProduct.PriceSource);
                var objects = JObject.Parse(json)["response"]["TrsrBdTradgList"];
                foreach (JObject obj in objects)
                {
                    FinancialProduct record = new FinancialProduct()
                    {
                        Name = obj["TrsrBd"]["nm"].ToString(),
                        Price = (float)obj["TrsrBd"]["minRedVal"]
                    };
                    list.Add(record);
                }
            }
            return list;
        }
        public void Update(FinancialProduct record) => throw new NotImplementedException();
    }
}