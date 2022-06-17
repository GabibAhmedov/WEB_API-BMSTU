using DataBaseAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Utils;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace DataBaseAPI.Importers
{
    public interface IDataImporter<T>
    {
        public Task<int> ImportDataAsync();
      


    }
}
