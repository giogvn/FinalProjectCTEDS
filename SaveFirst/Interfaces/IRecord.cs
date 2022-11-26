using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Interfaces
{
    public interface IRecord<T>
    {
        List<T> ReadAll();
        void Create(T newRecord);
        void Update(T record);
        void Delete(int RecordId);
        List<T> FindAllFromSaver(int SaverId);
    }
}
