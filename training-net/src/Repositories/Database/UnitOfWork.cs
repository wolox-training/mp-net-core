using training_net.Repositories.Interfaces;

namespace training_net.Repositories.Database
{
  public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            this._context = context;
            this.MovieRepository = new MovieRepository(this._context);

        }

        public IMovieRepository MovieRepository { get; private set; }

        public int Complete()
        {
            return this._context.SaveChanges();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
