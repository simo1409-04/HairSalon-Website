using HairSalon.Services.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Text;

namespace HairSalon.Data
{
    public class Repository(HairSalonDb context) : IRepository
    {

        public  IQueryable<T> All<T>() where T:class
        {

            return  context.Set<T>();

        }

        public  void Add<T>(T model) where T : class

        {

             context.Set<T>().Add(model);

        }

        public async Task<int> SaveChangesAsync() 
        {


            return await context.SaveChangesAsync();

        }




    }
}
