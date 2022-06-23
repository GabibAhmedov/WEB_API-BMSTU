using System;
using IntermediateModels;
using ImmigrationDTOs;

namespace Converters;

public class PlotConverter
{
    public static PlotDTO ConvertToDTO(PlotInt plot)
    {
        return new PlotDTO()
        {
            Id = plot.Id,
            Name = plot.Name,
            Data = plot.Data
        };
    }

    public static PlotInt ConvertToIntermediate(PlotDTO plot)
    {
        return new PlotInt()
        {
            Id = plot.Id,
            Name = plot.Name,
            Data = plot.Data
        };
    }
}

