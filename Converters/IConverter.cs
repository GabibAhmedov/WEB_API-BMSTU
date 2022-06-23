using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntermediateModels;
using ImmigrationDTOs;

namespace Converters;

public interface IConverter<TDTO, TInter> where TInter : new()
{
    public TDTO ConvertToDTO(TInter Intermediate);
    public sealed TInter ConvertToIntermediate(TDTO DTO) => new TInter();
}

