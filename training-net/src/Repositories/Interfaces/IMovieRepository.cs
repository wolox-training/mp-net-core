using System.Collections.Generic;
using MvcMovie.Models;

namespace Queries.Core.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<Movie> GetLastestMovies(int rank);
    }
}
