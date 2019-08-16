using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Interfaces;
using System;
using System.Linq;

namespace MvcFactbook.Code.Classes
{
    public class CompleteItem<T>
        where T : class, IComplete
    {
        private DbSet<T> data = null;
        private int? total = null;
        private int? complete = null;
        private int? incomplete = null;

        public DbSet<T> Data
        {
            get => data ?? throw new NullReferenceException("The Data object has not been set.");
            set => data = value;
        }

        public int? Total
        {
            get => Data.Count();
            set => total = value;
        }

        public int? Complete
        {
            get => Data.Count(x => x.Complete == true);
            set => total = value;
        }

        public int? Incomplete
        {
            get => Data.Count(x => x.Complete == false);
            set => total = value;
        }

        public CompleteItem(DbSet<T> data)
        {
            Data = data;
        }
    }
}
