using System.Collections.Generic;
using MvcMovie.Models;
using MvcMovie.Repositories.Database;
using Queries.Core.Repositories;
using System.Linq;

namespace Queries.Persistence.Repositories
{
  public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DataBaseContext context) : base(context){}

        public IEnumerable<Movie> GetLastestMovies(int rank)
        {
            return Context.Movies.OrderBy(c => c.ReleaseDate.ToString("yyyy")).Take(rank).ToList();
        } 
    }
}
