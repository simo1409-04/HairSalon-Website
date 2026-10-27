using System;
using System.Collections.Generic;
using System.Text;

namespace HairSalon.Services.Core.Contracts
{
    public interface IRepository
    {
        public IQueryable<T> All<T>() where T : class;

        public void Add<T>(T model) where T : class;


        public  Task<int> SaveChangesAsync();
        


    }
}
