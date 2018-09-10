using System.Collections.Generic;
using System.Linq;
using training_net.Repositories.Interfaces;
using training_net.Repositories.Database;
using training_net.Models;

namespace training_net.Repositories
{
  public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataBaseContext context) : base(context){}
    }
}
