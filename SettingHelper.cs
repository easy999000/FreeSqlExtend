using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSqlExtend
{
    public class SettingHelper
    {
        private readonly IConfiguration Configuration;
        public SettingHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<DBConnection> GetDBConn()
        {
            List<DBConnection> res = new List<DBConnection>();

            Configuration.GetSection("FreeSqlExtend").Bind(DBConnection.Position, res);
 

            return res;
        } 
    }
}
