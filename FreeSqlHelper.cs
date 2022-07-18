using FreeSql;
using FreeSql.Internal;
using FreeSql.Internal.CommonProvider;
using FreeSqlExtend;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using static FreeSqlGlobalExtensions;

public class FreeSqlHelper : BaseDbProvider, IFreeSql
{
    public FreeSqlHelper()
    {
    }
    /// <summary>
    /// 每个数据库的连接
    /// </summary>
    Dictionary<string, FreeSqlExtend.FreeSqlManage> DBDic = new Dictionary<string, FreeSqlExtend.FreeSqlManage>();

    /// <summary>
    /// 默认主数据库
    /// </summary>
    string _DefaultDBKey = "";

    /// <summary>
    /// 订阅消息
    /// </summary>
    public event Action<MsgType, string> WriteMsg;

    /// <summary>
    /// 获取特定数据库
    /// </summary>
    /// <param name="Key"></param>
    /// <returns></returns>
    public IFreeSql GetDB(string Key)
    {
        return DBDic[Key].FreeSql;
    }

    /// <summary>
    /// 默认常用数据库
    /// </summary>
    public IFreeSql DefaultDB
    {
        get
        {
            return GetDB(_DefaultDBKey);
        }
    }

    /// <summary>
    /// 获取特定数据库
    /// </summary>
    /// <param name="Key"></param>
    /// <returns></returns>
    public void ChangeDefaultDB(string Key)
    {
        _DefaultDBKey = Key;
    }


    /// <summary>
    /// 初始化db
    /// </summary>
    public void InitDB(SettingHelper setting)
    {
        //注意： IFreeSql 在项目中应以单例声明，而不是在每次使用的时候创建。
        //var setting = ActivatorUtilities.CreateInstance<SettingHelper>(Service);

        var conn = setting.GetDBConn();

        InitDB(conn);

    }

    /// <summary>
    /// 初始化db
    /// </summary>
    public void InitDB(List<DBConnection> conn)
    {
        //注意： IFreeSql 在项目中应以单例声明，而不是在每次使用的时候创建。 

        foreach (var item in conn)
        {
            var SqlManage = new FreeSqlExtend.FreeSqlManage(item);

            SqlManage.WriteMsg += SqlBuilder_WriteMsg;
            DBDic.Add(item.Name, SqlManage);
        }

        if (conn.Count > 0)
        {
            _DefaultDBKey = conn[0].Name;
        }
        else
        {
            _DefaultDBKey = "";
        }

        //注意： IFreeSql 在项目中应以单例声明，而不是在每次使用的时候创建。
    }

    private void SqlBuilder_WriteMsg(MsgType arg1, string arg2)
    {
        if (this.WriteMsg != null)
        {
            this.WriteMsg(arg1, arg2);
        }
    }


    #region iFreeSql接口实现

    BaseDbProvider _DefaultDBBasePro => DefaultDB as BaseDbProvider;
    public override IAdo Ado => _DefaultDBBasePro.Ado;
    public override IAop Aop => _DefaultDBBasePro.Aop;
    public override ICodeFirst CodeFirst => _DefaultDBBasePro.CodeFirst;
    public override IDbFirst DbFirst => _DefaultDBBasePro.DbFirst;
    public override GlobalFilter GlobalFilter => _DefaultDBBasePro.GlobalFilter;
    public override CommonExpression InternalCommonExpression => _DefaultDBBasePro.InternalCommonExpression;
    public override CommonUtils InternalCommonUtils => _DefaultDBBasePro.InternalCommonUtils;


    public override ISelect<T1> CreateSelectProvider<T1>(object dywhere)
    {
        return _DefaultDBBasePro.CreateSelectProvider<T1>(dywhere);
    }

    public override IInsert<T1> CreateInsertProvider<T1>()
    {
        return _DefaultDBBasePro.CreateInsertProvider<T1>();
    }

    public override IUpdate<T1> CreateUpdateProvider<T1>(object dywhere)
    {
        return _DefaultDBBasePro.CreateUpdateProvider<T1>(dywhere);
    }

    public override IDelete<T1> CreateDeleteProvider<T1>(object dywhere)
    {
        return _DefaultDBBasePro.CreateDeleteProvider<T1>(dywhere);
    }

    public override IInsertOrUpdate<T1> CreateInsertOrUpdateProvider<T1>()
    {
        return _DefaultDBBasePro.CreateInsertOrUpdateProvider<T1>();
    }

    public override void Dispose()
    {
        foreach (var item in DBDic)
        {
            item.Value.FreeSql.Dispose();
        }
    }

    #endregion


}


public  class FreeSqlHelperStatic
{

    static FreeSqlHelper _StaticDB;
    public static FreeSqlHelper StaticDB
    {
        get
        {
            return _StaticDB;
        }
    }

    /// <summary>
    /// 初始化db
    /// </summary>
    public static void InitStaticDB(List<DBConnection> conn)
    {
        //注意： IFreeSql 在项目中应以单例声明，而不是在每次使用的时候创建。 

        //var setting = new SettingHelper(null);

        _StaticDB = new FreeSqlHelper();
        _StaticDB.InitDB(conn);

        //注意： IFreeSql 在项目中应以单例声明，而不是在每次使用的时候创建。
    }

    /// <summary>
    /// 初始化db
    /// </summary>
    public static void InitStaticDB(SettingHelper setting)
    {
        //注意： IFreeSql 在项目中应以单例声明，而不是在每次使用的时候创建。 

        //var setting = new SettingHelper(null);

        _StaticDB = new FreeSqlHelper();
        _StaticDB.InitDB(setting);

        //注意： IFreeSql 在项目中应以单例声明，而不是在每次使用的时候创建。
    }

    #region 查询
    public static ISelect<T1> Select<T1>() where T1 : class
    {
        return StaticDB.Select<T1>();
    }
    public static ISelect<T1> Select<T1>(object dywhere) where T1 : class
    {
        return StaticDB.Select<T1>(dywhere);
    }
    public static ISelect<T1, T2> Select<T1, T2>() where T1 : class where T2 : class
    {
        return StaticDB.Select<T1, T2>();
    }
    public static ISelect<T1, T2, T3> Select<T1, T2, T3>()
        where T1 : class where T2 : class where T3 : class
    {
        return StaticDB.Select<T1, T2, T3>();
    }
    public static ISelect<T1, T2, T3, T4> Select<T1, T2, T3, T4>()
            where T1 : class where T2 : class where T3 : class where T4 : class
    {
        return StaticDB.Select<T1, T2, T3, T4>();
    }
    public static ISelect<T1, T2, T3, T4, T5> Select<T1, T2, T3, T4, T5>()
            where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class
    {
        return StaticDB.Select<T1, T2, T3, T4, T5>();
    }
    public static ISelect<T1, T2, T3, T4, T5, T6> Select<T1, T2, T3, T4, T5, T6>()
            where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class
    {
        return StaticDB.Select<T1, T2, T3, T4, T5, T6>();
    }

    #endregion
    #region 插入

    /// <summary>
    /// 插入数据
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <returns></returns>
    public static IInsert<T1> Insert<T1>() where T1 : class
    {
        return StaticDB.Insert<T1>();
    }
    /// <summary>
    /// 插入数据，传入实体
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IInsert<T1> Insert<T1>(T1 source) where T1 : class
    {
        return StaticDB.Insert<T1>(source);
    }
    /// <summary>
    /// 插入数据，传入实体数组
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IInsert<T1> Insert<T1>(T1[] source) where T1 : class
    {
        return StaticDB.Insert<T1>(source);
    }
    /// <summary>
    /// 插入数据，传入实体集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IInsert<T1> Insert<T1>(List<T1> source) where T1 : class
    {
        return StaticDB.Insert<T1>(source);
    }
    /// <summary>
    /// 插入数据，传入实体集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IInsert<T1> Insert<T1>(IEnumerable<T1> source) where T1 : class
    {
        return StaticDB.Insert<T1>(source);
    }
    #endregion
    #region 更新
    /// <summary>
    /// 插入或更新数据，此功能依赖数据库特性（低版本可能不支持），参考如下：<para></para>
    /// MySql 5.6+: on duplicate key update<para></para>
    /// PostgreSQL 9.4+: on conflict do update<para></para>
    /// SqlServer 2008+: merge into<para></para>
    /// Oracle 11+: merge into<para></para>
    /// Sqlite: replace into<para></para>
    /// 达梦: merge into<para></para>
    /// 人大金仓：on conflict do update<para></para>
    /// 神通：merge into<para></para>
    /// MsAccess：不支持<para></para>
    /// 注意区别：FreeSql.Repository 仓储也有 InsertOrUpdate 方法（不依赖数据库特性）
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <returns></returns>
    public static IInsertOrUpdate<T1> InsertOrUpdate<T1>() where T1 : class
    {
        return StaticDB.InsertOrUpdate<T1>();
    }

    /// <summary>
    /// 修改数据
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <returns></returns>
    public static IUpdate<T1> Update<T1>() where T1 : class
    {
        return StaticDB.Update<T1>();
    }
    /// <summary>
    /// 修改数据，传入动态条件，如：主键值 | new[]{主键值1,主键值2} | TEntity1 | new[]{TEntity1,TEntity2} | new{id=1}
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="dywhere">主键值、主键值集合、实体、实体集合、匿名对象、匿名对象集合</param>
    /// <returns></returns>
    public static IUpdate<T1> Update<T1>(object dywhere) where T1 : class
    {
        return StaticDB.Update<T1>(dywhere);
    }


    #endregion
    #region 删除


    /// <summary>
    /// 删除数据
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <returns></returns>
    public static IDelete<T1> Delete<T1>() where T1 : class
    {
        return StaticDB.Delete<T1>();
    }
    /// <summary>
    /// 删除数据，传入动态条件，如：主键值 | new[]{主键值1,主键值2} | TEntity1 | new[]{TEntity1,TEntity2} | new{id=1}
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="dywhere">主键值、主键值集合、实体、实体集合、匿名对象、匿名对象集合</param>
    /// <returns></returns>
    public static IDelete<T1> Delete<T1>(object dywhere) where T1 : class
    {
        return StaticDB.Delete<T1>(dywhere);
    }

    #endregion

    #region InsertDict/UpdateDict/InsertOrUpdateDict/DeleteDict
    /// <summary>
    /// 插入数据字典 Dictionary&lt;string, object&gt;
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static InsertDictImpl InsertDict(Dictionary<string, object> source)
    {
        return StaticDB.InsertDict(source);
    }
    /// <summary>
    /// 插入数据字典，传入 Dictionary&lt;string, object&gt; 集合
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static InsertDictImpl InsertDict(IEnumerable<Dictionary<string, object>> source)
    {
        return StaticDB.InsertDict(source);
    }
    /// <summary>
    /// 更新数据字典 Dictionary&lt;string, object&gt;
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static UpdateDictImpl UpdateDict(Dictionary<string, object> source)
    {
        return StaticDB.UpdateDict(source);
    }
    /// <summary>
    /// 更新数据字典，传入 Dictionary&lt;string, object&gt; 集合
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static UpdateDictImpl UpdateDict(IEnumerable<Dictionary<string, object>> source)
    {
        return StaticDB.UpdateDict(source);
    }
    /// <summary>
    /// 插入或更新数据字典，此功能依赖数据库特性（低版本可能不支持），参考如下：<para></para>
    /// MySql 5.6+: on duplicate key update<para></para>
    /// PostgreSQL 9.4+: on conflict do update<para></para>
    /// SqlServer 2008+: merge into<para></para>
    /// Oracle 11+: merge into<para></para>
    /// Sqlite: replace into<para></para>
    /// 达梦: merge into<para></para>
    /// 人大金仓：on conflict do update<para></para>
    /// 神通：merge into<para></para>
    /// MsAccess：不支持<para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static InsertOrUpdateDictImpl InsertOrUpdateDict(Dictionary<string, object> source)
    {
        return StaticDB.InsertOrUpdateDict(source);
    }
    public static InsertOrUpdateDictImpl InsertOrUpdateDict(IEnumerable<Dictionary<string, object>> source)
    {
        return StaticDB.InsertOrUpdateDict(source);
    }
    /// <summary>
    /// 删除数据字典 Dictionary&lt;string, object&gt;
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DeleteDictImpl DeleteDict(Dictionary<string, object> source)
    {
        return StaticDB.DeleteDict(source);
    }
    /// <summary>
    /// 删除数据字典，传入 Dictionary&lt;string, object&gt; 集合
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DeleteDictImpl DeleteDict(IEnumerable<Dictionary<string, object>> source)
    {
        return StaticDB.DeleteDict(source);
    }



    #endregion
}

