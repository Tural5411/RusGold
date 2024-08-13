using RusGold.Shared.Concrete.EntityFramework;
using RusGold.Data.Abstract;
using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RusGold.Data.Concrete.EntityFramework.Repositories
{
    public class ArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext Context) : base(Context)
        {
        }
    }
}
