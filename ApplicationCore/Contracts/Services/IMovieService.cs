using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        // home/index action method
        Task<List<MovieCardModel>> GetTop30GrossingMovies();
        Task<MovieDetailsModel> GetMovieDetails(int movieId);
    }
}
