﻿using SqlSugar;
using SqlSugar.IOC;
using StuManage.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StuManage.Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity> where TEntity : class, new()
    {
        public BaseRepository(ISqlSugarClient context = null) : base(context)
        {
            base.Context = DbScoped.Sugar;
            //base.Context.CodeFirst.InitTables(
            //typeof(Student),
            //    typeof(Competition),
            //    typeof(Score),
            //typeof(Examinformation),
            //typeof(Announce),
            //typeof(User)
            //);
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

        public async Task<TEntity> FindItem(Expression<Func<TEntity, bool>> func)
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
