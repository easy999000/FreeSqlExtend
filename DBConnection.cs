using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSqlExtend
{
    /// <summary>
    /// 
    /// </summary>
    public class DBConnection
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Position = "DBConnectionList";

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionStr { get; set; }

        /// <summary>
        /// 从库连接字符串
        /// </summary>
        public string[] SlaveConnectionStr { get; set; }

        /// <summary>
        /// 从库权重,长度需要和SlaveConnectionStr保持一致
        /// </summary>
        public int[] SlaveWeight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FreeSql.DataType DbType { get; set; }

    }
}
