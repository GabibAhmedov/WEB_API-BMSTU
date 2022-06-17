using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseAPI.Importers
{
    public interface IVkImportAlternatives<T>
    {
        public Task<List<T>> ImportDataAlternativelyAsync();

    }
}
