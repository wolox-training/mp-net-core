using System.Collections.Generic;
using training_net.Models;

namespace training_net.Repositories.Interfaces
{
  public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<Movie> GetLastestMovies(int rank);
    }
}
