using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlExtend
{
    internal class FreeSqlManage
    {
        public FreeSqlManage(DBConnection conn)
        {
            Conn = conn;
        }


        private IFreeSql _FreeSql;
        private DBConnection _Conn;

        /// <summary>
        /// 返回消息
        /// </summary>
        public event Action<MsgType, string> WriteMsg;
        public DBConnection Conn
        {
            get
            {
                return _Conn;
            }
            private set
            {
                if (_Conn != value)
                {
                    _FreeSql = null;
                    _Conn = value;
                }
            }
        }

        /// <summary>
        /// 主要数据库操作对象
        /// </summary>
        public IFreeSql FreeSql
        {
            get
            {
                if (_FreeSql == null)
                {
                    _FreeSql = new FreeSql.FreeSqlBuilder()
                  .UseConnectionString(Conn.DbType, Conn.ConnectionStr)
                  .UseSlave(Conn.SlaveConnectionStr)
                  .Build();

                    ///耗时监控
                    _FreeSql.Aop.CommandAfter += Aop_CommandAfter;
                    ///拦截空where update和delete
                    _FreeSql.Aop.CommandBefore += Aop_CommandBefore;

                }

                return _FreeSql;
            }
        }

        private void Aop_CommandAfter(object sender, FreeSql.Aop.CommandAfterEventArgs e)
        {
            if (e.ElapsedMilliseconds > 10000)
            {
                ///耗时监控
                
                //   Debug.WriteLine("");
                //   Debug.WriteLine("Aop_CommandAfter");
                ////   Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(sender));
                //   Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(e));
                //   Debug.WriteLine("Aop_CommandAfter 222");
                if (WriteMsg != null)
                {
                    WriteMsg(MsgType.Error, e.Log);

                }
            }


        }
        private void Aop_CommandBefore(object sender, FreeSql.Aop.CommandBeforeEventArgs e)
        {

            //Debug.WriteLine("");
            //Debug.WriteLine("Aop_CommandBefore");
            ////  Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(sender));
            //Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(e));
            //Debug.WriteLine("Aop_CommandBefore 222");
        }

    }
}
