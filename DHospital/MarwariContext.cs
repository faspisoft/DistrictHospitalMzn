using DHospital.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHospital
{
    public class MarwariContext : DbContext
    {
        public MarwariContext() : base("MyConn")
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
