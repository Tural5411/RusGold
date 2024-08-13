using RusGold.Data.Abstract;
using RusGold.Entities.Concrete;
using RusGold.Shared.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Data.Concrete.EntityFramework.Repositories
{
    public class QuestionRepository : EfEntityRepositoryBase<Questions>, IQuestionRepository
    {
        public QuestionRepository(DbContext Context) : base(Context)
        {
        }
    }
}
