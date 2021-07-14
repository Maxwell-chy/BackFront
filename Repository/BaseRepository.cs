using Model;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity> where TEntity : class, new()
    {
        public BaseRepository(ISqlSugarClient context = null) : base(context)
        {
            base.Context = DbScoped.Sugar;
            base.Context.CodeFirst.InitTables(
            typeof(Announce),
            typeof(Customer),
            typeof(ExamInfo),
            typeof(Score),
            typeof(StuExam),
            typeof(Student),
            typeof(CompInfo),
            typeof(StuComp),
            typeof(Question),
            typeof(Answer),
            typeof(Teacher)
            );
        }
        public async Task<bool> InsertItem(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }
        public async Task<bool> DeleteItem(int id)
        {
            return await base.DeleteByIdAsync(id);
        }
        public async Task<bool> DeleteItem(Expression<Func<TEntity, bool>> func)
        {
            return await base.DeleteAsync(func);
        }
        public async Task<bool> UpdateItem(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }
        public virtual async Task<TEntity> FindItem(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public virtual async Task<TEntity> FindItem(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        public virtual async Task<List<TEntity>> FindItemList()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<TEntity>> FindItemList(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }
    }
}
