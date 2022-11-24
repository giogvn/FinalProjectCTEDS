using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Interfaces
{
    internal interface IRecord<T>
    {
        List<T> ReadAll();
        void Create(T newRecord);
        void Update(T record);
        void Delete(string idRecord);
    }
}
