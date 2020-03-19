using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OKEX.Auto.Core.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.ORM.EF
{
    //使用api 迁移时测试
    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DefaultEFDBContext>
    //{
    //    public DefaultEFDBContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<DefaultEFDBContext>();
    //        optionsBuilder.UseSqlServer("Data Source=blog.db");

    //        return new DefaultEFDBContext(optionsBuilder.Options);
    //    }
    //}
}