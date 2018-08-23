using System.Collections.Generic;
using System.Linq;
using training_net.Repositories.Interfaces;
using training_net.Repositories.Database;
using training_net.Models;

namespace training_net.Repositories
{
  public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DataBaseContext context) : base(context){}
        public IEnumerable<Movie> GetLastestMovies(int rank) => this.Context.Movies.OrderBy(c => c.ReleaseDate.ToString("yyyy")).Take(rank).ToList();
    }
}
