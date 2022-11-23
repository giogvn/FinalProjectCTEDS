using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Interfaces
{
    internal interface iRecord
    {
        List<Record> ReadAll();
        void Create(Record newRecord);
        void Update(Record record);
        void Delete(string idRecord);
    }
}
