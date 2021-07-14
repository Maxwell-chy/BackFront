
using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class AnnounceRepository : BaseRepository<Announce> { }
    public class CustomerRepository : BaseRepository<Customer> { }
    public class ExamInfoRepository : BaseRepository<ExamInfo> { }
    
    public class ScoreRepository : BaseRepository<Score> { }
    public class StuExamRepository : BaseRepository<StuExam> {
        public override async Task<List<StuExam>> FindItemList(Expression<Func<StuExam, bool>> func)
        {
            return await base.Context.Queryable<StuExam>()
                .Where(func)
        .Mapper(c => c.exam, c => c.examid, c => c.exam.examid)
        .Mapper(c => c.subj, c => c.subject, c => c.subj.subject)
        .ToListAsync();
        }

    }
    
}