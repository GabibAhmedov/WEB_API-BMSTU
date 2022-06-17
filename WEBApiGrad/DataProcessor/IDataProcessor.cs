using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WEBApiGrad.DataProcessor
{
    public interface IDataProcessor<T>
    {
        public Task<T> PrepareDataAsync();
    }
}
