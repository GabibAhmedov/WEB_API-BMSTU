using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntermediateModels;
using ImmigrationDTOs;

namespace Converters
{
    public interface IConverter<DTO ,Inter>
    {
        public DTO ConvertToDTO(Inter Intermediate );

        public Inter ConvertToIntermediate(DTO DTO);

    }
}
