using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using WebApplication1.Data;

namespace WebApplication1.DataTransmission
{
    interface IMessageSystem
    {
        void Add(DbObject temp);
        public void Remove(string ID);
        public T Find<T>(string ID);
        public List<T> ToListAsync<T>();

    }
}
