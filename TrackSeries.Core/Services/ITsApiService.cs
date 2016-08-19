using System.Collections.Generic;
using System.Threading.Tasks;
using TrackSeries.Core.Models;

namespace TrackSeries.Core.Services
{
    public interface ITsApiService
    {
        Task<List<SerieFollowersVM>> GetStatsTopSeries();
        Task<SerieVM> GetSerieByIdAll(int id);
        Task<SerieInfoVM> GetSerieById(int id);
        Task<List<SerieSearch>> GetSeriesSearch(string name);
        Task<SerieFollowersVM> GetStatsSerieHighlighted();
    }
}