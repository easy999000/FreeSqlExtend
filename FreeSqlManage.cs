﻿using FreeSql.Aop;
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
        /// sql耗时警告阈值 毫秒
        /// </summary>
        public uint SqlWarningMilliseconds { get; set; } = 1500;
        ///// <summary>
        ///// sql监控 查询过多条数
        ///// </summary>
        //public uint SqlWarningSelectCount { get; set; } = 2000;


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
        private object freesqlCreateLock = new object();
        /// <summary>
        /// 主要数据库操作对象
        /// </summary>
        public IFreeSql FreeSql
        {
            get
            {
                if (_FreeSql == null)
                {
                    lock (freesqlCreateLock)
                    {
                        if (_FreeSql == null)
                        {
                            var build = new FreeSql.FreeSqlBuilder()
                              .UseConnectionString(Conn.DbType, Conn.ConnectionStr)
                              .UseSlave(Conn.SlaveConnectionStr);
                           

                            if (Conn.SlaveWeight != null
                                && Conn.SlaveConnectionStr != null
                                )
                            {
                                //&& Conn.SlaveConnectionStr.Length == Conn.SlaveWeight.Length
                                //不做长度判断,如果长度不一致,报错好发现错误.
                                build = build.UseSlaveWeight(Conn.SlaveWeight);
                            }
                            _FreeSql = build.Build();

                            ///耗时监控
                            _FreeSql.Aop.CommandAfter += Aop_CommandAfter;
                            ///拦截空where update和delete
                            _FreeSql.Aop.CommandBefore += Aop_CommandBefore;

                            _FreeSql.Aop.AuditValue += Aop_AuditValue;
                        }
                    }
                }

                return _FreeSql;
            }
        }

        private void Aop_AuditValue(object sender, FreeSql.Aop.AuditValueEventArgs e)
        {
            if ((e.AuditValueType == AuditValueType.InsertOrUpdate
                || e.AuditValueType == AuditValueType.Insert
                || e.AuditValueType == AuditValueType.Update)
                && e.Column.CsType.FullName == "System.String")
            {
                if (e.Column.DbSize > 0 && e.Value != null && e.Value.ToString().Length > e.Column.DbSize)
                {
                    e.Value = e.Value.ToString().Substring(0, e.Column.DbSize);
                }
            }




        }

        private void Aop_CommandAfter(object sender, FreeSql.Aop.CommandAfterEventArgs e)
        {
            if (e.Exception != null)
            {
                if (WriteMsg != null)
                {
                    WriteMsg(MsgType.Error, $"sql异常.({e.Command.CommandText})");

                }
            }


            if (e.ElapsedMilliseconds > SqlWarningMilliseconds
                )
            {
                ///耗时监控 
                if (WriteMsg != null)
                {
                    WriteMsg(MsgType.Error, $"sql耗时:{e.ElapsedMilliseconds / 1000}秒.({e.Command.CommandText})");
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
