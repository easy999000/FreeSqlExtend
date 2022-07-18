using FreeSql;
using FreeSql.Internal.Model;
using FreeSqlExtend;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


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

}

