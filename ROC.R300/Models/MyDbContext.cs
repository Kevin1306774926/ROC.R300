using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ROC.R300.Models
{
    public class MyDbContext:DbContext
    {
        static MyDbContext()
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDbContext>());
        }
        public MyDbContext():base("MyDbContext")
        {

        }

        public DbSet<VisitInfo> VisitRecords { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
    }
}