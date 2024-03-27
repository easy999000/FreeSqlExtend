using FreeSql;
using FreeSql.DataAnnotations;
using FreeSql.Internal;
using FreeSql.Internal.CommonProvider;
using FreeSql.Internal.Model;
using FreeSqlExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using static FreeSql.Internal.GlobalFilter;

public static class FreeSqlExtension
{
    #region 分页方法


    /// <summary>
    /// 无数据返回,后续自定义返回类型
    /// </summary>
    /// <typeparam name="TSelect">查询类型</typeparam>
    /// <typeparam name="T1">第一实体类型</typeparam>
    /// <typeparam name="TReturn">返回类型</typeparam>
    /// <param name="Select"></param>
    /// <param name="Page"></param>
    /// <param name="outSelect"></param>
    /// <returns></returns>
    private static (int PageNumber, int PageSize, long Count, TSelect Select) ToPageBase<TSelect, T1>(this ISelect0<TSelect, T1> Select
        , PageParam Page)
        where T1 : class where TSelect : ISelect0<TSelect, T1>
    {
        // TSelect outSelect = Select.Page(Page);
        TSelect outSelect = Select.Page(Page.PageNumber, Page.PageSize);


        return (Page.PageNumber, Page.PageSize, Page.Count, outSelect);
    }
    /// <summary>
    /// 无数据返回,后续自定义返回类型
    /// </summary>
    /// <typeparam name="TSelect">查询类型</typeparam>
    /// <typeparam name="T1">第一实体类型</typeparam>
    /// <typeparam name="TReturn">返回类型</typeparam>
    /// <param name="Select"></param>
    /// <param name="Page"></param>
    /// <param name="outSelect"></param>
    /// <returns></returns>
    private static PageResult<TReturn> ToPage<TSelect, T1, TReturn>(this ISelect0<TSelect, T1> Select
        , PageParam Page, out TSelect outSelect)
        where T1 : class where TSelect : ISelect0<TSelect, T1>
    {
        var pageRes = ToPageBase(Select, Page);

        outSelect = pageRes.Select;

        PageResult<TReturn> res = new PageResult<TReturn>();
        res.PageNumber = pageRes.PageNumber;
        res.PageSize = pageRes.PageSize;
        res.Count = pageRes.Count;



        return res;
    }
    #region 返回单表
    ///// <summary>
    ///// 原类型返回
    ///// </summary>
    ///// <typeparam name="TSelect"></typeparam>
    ///// <typeparam name="T1"></typeparam>
    ///// <param name="Select"></param>
    ///// <param name="Page"></param>
    ///// <returns></returns>
    //public static PageResult<T1> ToPage<TSelect, T1>(this ISelect0<TSelect, T1> Select, PageParam Page)
    //        where T1 : class where TSelect : ISelect0<TSelect, T1>
    //{
    //    var res = ToPage<TSelect, T1, T1>(Select, Page, out TSelect outSelect);

    //    res.Data = outSelect.ToList();


    //    return res;
    //}


    public static PageResult<T1> ToPage<T1>(this ISelect<T1> Select
        , PageParam Page)
        where T1 : class
    {
        var res = ToPage<ISelect<T1>, T1, T1>(Select, Page, out ISelect<T1> outSelect);
        if (Page.Count < 1)
        {
            res.Count = outSelect.Count();
        }
        res.Data = outSelect.ToList();

        return res;
    }

    #endregion

    #region 返回类型扩展
    public static PageResult<TReturn> ToPage<T1, TReturn>(this ISelect<T1> Select
        , PageParam Page
        , Expression<Func<T1, TReturn>> Column)
        where T1 : class
    {
        var res = ToPage<ISelect<T1>, T1, TReturn>(Select, Page, out ISelect<T1> outSelect);
        if (Page.Count < 1)
        {
            res.Count = outSelect.Count();
        }
        res.Data = outSelect.ToList(Column);

        return res;
    }
    public static PageResult<TReturn> ToPage<T1, T2, TReturn>(this ISelect<T1, T2> Select
        , PageParam Page
        , Expression<Func<HzyTuple<T1, T2>, TReturn>> Column)
        where T1 : class where T2 : class
    {
        var res = ToPage<ISelect<T1, T2>, T1, TReturn>(Select, Page, out ISelect<T1, T2> outSelect);
        if (Page.Count < 1)
        {
            res.Count = outSelect.Count();
        }
        res.Data = outSelect.ToList(Column);

        return res;
    }
    public static PageResult<TReturn> ToPage<T1, T2, T3, TReturn>(this ISelect<T1, T2, T3> Select
        , PageParam Page
        , Expression<Func<HzyTuple<T1, T2, T3>, TReturn>> Column)
        where T1 : class where T2 : class where T3 : class
    {
        var res = ToPage<ISelect<T1, T2, T3>, T1, TReturn>(Select, Page, out ISelect<T1, T2, T3> outSelect);
        if (Page.Count < 1)
        {
            res.Count = outSelect.Count();
        }
        res.Data = outSelect.ToList(Column);

        return res;
    }
    public static PageResult<TReturn> ToPage<T1, T2, T3, T4, TReturn>(this ISelect<T1, T2, T3, T4> Select
        , PageParam Page
        , Expression<Func<HzyTuple<T1, T2, T3, T4>, TReturn>> Column)
        where T1 : class where T2 : class where T3 : class where T4 : class
    {
        var res = ToPage<ISelect<T1, T2, T3, T4>, T1, TReturn>(Select, Page, out ISelect<T1, T2, T3, T4> outSelect);
        res.Data = outSelect.ToList(Column);
        if (Page.Count < 1)
        {
            res.Count = outSelect.Count();
        }

        return res;
    }
    public static PageResult<TReturn> ToPage<T1, T2, T3, T4, T5, TReturn>(this ISelect<T1, T2, T3, T4, T5> Select
        , PageParam Page
        , Expression<Func<HzyTuple<T1, T2, T3, T4, T5>, TReturn>> Column)
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class
    {
        var res = ToPage<ISelect<T1, T2, T3, T4, T5>, T1, TReturn>(Select, Page, out ISelect<T1, T2, T3, T4, T5> outSelect);
        res.Data = outSelect.ToList(Column);
        if (Page.Count < 1)
        {
            res.Count = outSelect.Count();
        }

        return res;
    }
    public static PageResult<TReturn> ToPage<T1, T2, T3, T4, T5, T6, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6> Select
        , PageParam Page
        , Expression<Func<HzyTuple<T1, T2, T3, T4, T5, T6>, TReturn>> Column)
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class
    {
        var res = ToPage<ISelect<T1, T2, T3, T4, T5, T6>, T1, TReturn>(Select, Page, out ISelect<T1, T2, T3, T4, T5, T6> outSelect);
        res.Data = outSelect.ToList(Column);
        if (Page.Count < 1)
        {
            res.Count = outSelect.Count();
        }

        return res;
    }

    #endregion

    #region 返回类型DTO

    public static PageDTO<T1> ToPageDTO<T1>(this ISelect<T1> Select
        , PageParam Page)
        where T1 : class
    {
        var pageRes = ToPageBase(Select, Page);
        var res = new PageDTO<T1>();
        res.PageNumber = pageRes.PageNumber;
        res.PageSize = pageRes.PageSize;
        res.Count = pageRes.Count;
        res.PageExpression = pageRes.Select;
        return res;
    }

    public static PageDTO<T1, T2> ToPageDTO<T1, T2>(this ISelect<T1, T2> Select
        , PageParam Page)
        where T1 : class where T2 : class
    {
        var pageRes = ToPageBase(Select, Page);
        var res = new PageDTO<T1, T2>();
        res.PageNumber = pageRes.PageNumber;
        res.PageSize = pageRes.PageSize;
        res.Count = pageRes.Count;
        res.PageExpression = pageRes.Select;
        return res;
    }
    public static PageDTO<T1, T2, T3> ToPageDTO<T1, T2, T3>(this ISelect<T1, T2, T3> Select
        , PageParam Page)
        where T1 : class where T2 : class where T3 : class
    {
        var pageRes = ToPageBase(Select, Page);
        var res = new PageDTO<T1, T2, T3>();
        res.PageNumber = pageRes.PageNumber;
        res.PageSize = pageRes.PageSize;
        res.Count = pageRes.Count;
        res.PageExpression = pageRes.Select;
        return res;
    }
    public static PageDTO<T1, T2, T3, T4> ToPageDTO<T1, T2, T3, T4>(this ISelect<T1, T2, T3, T4> Select
        , PageParam Page)
        where T1 : class where T2 : class where T3 : class where T4 : class
    {
        var pageRes = ToPageBase(Select, Page);
        var res = new PageDTO<T1, T2, T3, T4>();
        res.PageNumber = pageRes.PageNumber;
        res.PageSize = pageRes.PageSize;
        res.Count = pageRes.Count;
        res.PageExpression = pageRes.Select;
        return res;
    }
    public static PageDTO<T1, T2, T3, T4, T5> ToPageDTO<T1, T2, T3, T4, T5>(this ISelect<T1, T2, T3, T4, T5> Select
        , PageParam Page)
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class
    {
        var pageRes = ToPageBase(Select, Page);
        var res = new PageDTO<T1, T2, T3, T4, T5>();
        res.PageNumber = pageRes.PageNumber;
        res.PageSize = pageRes.PageSize;
        res.Count = pageRes.Count;
        res.PageExpression = pageRes.Select;
        return res;
    }
    public static PageDTO<T1, T2, T3, T4, T5, T6> ToPageDTO<T1, T2, T3, T4, T5, T6>(this ISelect<T1, T2, T3, T4, T5, T6> Select
        , PageParam Page)
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class
    {
        var pageRes = ToPageBase(Select, Page);
        var res = new PageDTO<T1, T2, T3, T4, T5, T6>();
        res.PageNumber = pageRes.PageNumber;
        res.PageSize = pageRes.PageSize;
        res.Count = pageRes.Count;
        res.PageExpression = pageRes.Select;
        return res;
    }


    #endregion
    #endregion

    #region Update扩展
    /// <summary>
    /// 扩展dto更新方法, dto属性值null的不更新
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="Source"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IUpdate<T1> SetDtoIgnoreNull<T1>(this IUpdate<T1> Source, object dto)
    {
        var source2 = Source as UpdateProvider<T1>;

        if (source2 == null)
        {
            return Source;
        }


        if (dto == null) return Source;
        if (dto is Dictionary<string, object>)
        {
            var dic = dto as Dictionary<string, object>;
            foreach (var kv in dic)
            {
                if (kv.Value == null)
                {
                    continue;
                }
                if (source2._table.ColumnsByCs.TryGetValue(kv.Key, out var trycol) == false) continue;
                if (source2._ignore.ContainsKey(trycol.Attribute.Name)) continue;
                SetPriv(source2, trycol, kv.Value);
            }
            return Source;
        }
        var dtoProps = dto.GetType().GetProperties();
        foreach (var dtoProp in dtoProps)
        {
            if (dtoProp.GetValue(dto, null) == null)
            {
                continue;
            }
            if (source2._table.ColumnsByCs.TryGetValue(dtoProp.Name, out var trycol) == false) continue;
            if (source2._ignore.ContainsKey(trycol.Attribute.Name)) continue;
            SetPriv(source2, trycol, dtoProp.GetValue(dto, null));
        }
        return Source;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="Source2"></param>
    /// <param name="col"></param>
    /// <param name="value"></param>

    static void SetPriv<T1>(UpdateProvider<T1> Source2, ColumnInfo col, object value)
    {
        object val = null;
        if (col.Attribute.MapType == col.CsType) val = value;
        else val = Utils.GetDataReaderValue(col.Attribute.MapType, value);
        Source2._set.Append(", ").Append(Source2._commonUtils.QuoteSqlName(col.Attribute.Name)).Append(" = ");

        var colsql = Source2._noneParameter ? Source2._commonUtils.GetNoneParamaterSqlValue(Source2._params, "u", col, col.Attribute.MapType, val) :
            Source2._commonUtils.QuoteWriteParamterAdapter(col.Attribute.MapType, Source2._commonUtils.QuoteParamterName($"p_{Source2._params.Count}"));
        Source2._set.Append(Source2._commonUtils.RewriteColumn(col, colsql));
        if (Source2._noneParameter == false)
            Source2._commonUtils.AppendParamter(Source2._params, null, col, col.Attribute.MapType, val);
    }

    /// <summary>
    /// 自动匹配新旧数据,执行插入,更新,删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="Repo"></param>
    /// <param name="UpdateData"></param>
    /// <param name="OldData"></param>
    /// <param name="KeySelect"></param>
    public static void UpdateFull<TEntity, TKey>(this IBaseRepository<TEntity> Repo
        , List<TEntity> UpdateData
        , List<TEntity> OldData
        , Func<TEntity, TKey> KeySelect) where TEntity : class
    {
        var updateList = new List<TEntity>();
        var insertList = new List<TEntity>();
        var delList = new List<TEntity>();

        foreach (var item in UpdateData)
        {
            var ID = KeySelect(item);
            if (ID.Equals(default(TKey)))
            {
                insertList.Add(item);
            }
            else
            {
                updateList.Add(item);
            }
        }

        foreach (var item2 in OldData)
        {
            var ID2 = KeySelect(item2);

            if (!UpdateData.Any(f => ID2.Equals(KeySelect(f))))
            {
                delList.Add(item2);
            }

        }
        Repo.Insert(insertList);
        Repo.Update(updateList);
        Repo.Delete(delList);
    }



    #endregion

    #region 字符串 大于,小于

    static ThreadLocal<ExpressionCallContext> context = new ThreadLocal<ExpressionCallContext>();
    /// <summary>
    /// freesql字符串扩展方法,用在where条件表达式,
    /// 其他情况不要用
    /// 永远假值
    /// </summary>
    /// <param name="that"></param>
    /// <param name="arg1"></param>
    /// <returns></returns>
    [ExpressionCall]
    public static bool GreaterThan(this string that, string arg1)
    {
        var up = context.Value;
        up.Result = $"{up.ParsedContent["that"]}>{up.ParsedContent["arg1"]}";
        return false;
    }
    /// <summary>
    /// freesql字符串扩展方法,用在where条件表达式,
    /// 其他情况不要用
    /// 永远假值
    /// </summary>
    /// <param name="that"></param>
    /// <param name="arg1"></param>
    /// <returns></returns>
    [ExpressionCall]
    public static bool LessThan(this string that, string arg1)
    {
        var up = context.Value;

        up.Result = $"{up.ParsedContent["that"]}<{up.ParsedContent["arg1"]}";
        return false;
    }

    #endregion
}

