using FreeSql;
using FreeSql.Internal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSqlExtend
{
    /// <summary>
    /// 分页参数
    /// Count数量大于零的时候,不会重新统计总数,有利于性能提升
    /// </summary>
    public class PageParam : BasePagingInfo
    {
        public PageParam()
        {
            this.PageNumber = 1;
            this.PageSize = 20;
            this.Count = 0;
        }

        /// <summary>
        /// 页面数量
        /// </summary>
        public long PageCount
        {
            get
            { 
                if (PageSize < 1)
                {
                    return 0;
                }
                return Count / PageSize + (Count % PageSize > 0 ? 1 : 0);
            }
        }

    }


    public class PageResult<T> : PageParam
    {
        /// <summary>
        /// 
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();

        public void FromParam(PageParam param)
        {
            this.Count = param.Count;
            this.PageNumber = param.PageNumber;
            this.PageSize = param.PageSize;
        }
    }
    public class PageDTO<T1> : PageParam
    {
        public ISelect<T1> PageExpression { get; set; }

        public PageResult<DTO> ToPage<DTO>()
        {
            PageResult<DTO> res = new PageResult<DTO>();
            res.FromParam(this);
            res.Data = PageExpression.ToList<DTO>();
            if (this.Count < 1)
            {
                res.Count = PageExpression.Count();
            }

            return res;
        }
    }
    public class PageDTO<T1, T2> : PageParam where T2 : class
    {

        public ISelect<T1, T2> PageExpression { get; set; }

        public PageResult<DTO> ToPage<DTO>()
        {
            PageResult<DTO> res = new PageResult<DTO>();
            res.FromParam(this);
            res.Data = PageExpression.ToList<DTO>();
            if (this.Count < 1)
            {
                res.Count = PageExpression.Count();
            }

            return res;
        }
    }
    public class PageDTO<T1, T2, T3> : PageParam where T2 : class where T3 : class
    {

        public ISelect<T1, T2, T3> PageExpression { get; set; }

        public PageResult<DTO> ToPage<DTO>()
        {
            PageResult<DTO> res = new PageResult<DTO>();
            res.FromParam(this);
            res.Data = PageExpression.ToList<DTO>();
            if (this.Count < 1)
            {
                res.Count = PageExpression.Count();
            }

            return res;
        }
    }
    public class PageDTO<T1, T2, T3, T4> :
        PageParam where T2 : class where T3 : class where T4 : class
    {

        public ISelect<T1, T2, T3, T4> PageExpression { get; set; }

        public PageResult<DTO> ToPage<DTO>()
        {
            PageResult<DTO> res = new PageResult<DTO>();
            res.FromParam(this);
            res.Data = PageExpression.ToList<DTO>();
            if (this.Count < 1)
            {
                res.Count = PageExpression.Count();
            }

            return res;
        }
    }
    public class PageDTO<T1, T2, T3, T4, T5> :
        PageParam where T2 : class where T3 : class where T4 : class where T5 : class
    {

        public ISelect<T1, T2, T3, T4, T5> PageExpression { get; set; }

        public PageResult<DTO> ToPage<DTO>()
        {
            PageResult<DTO> res = new PageResult<DTO>();
            res.FromParam(this);
            res.Data = PageExpression.ToList<DTO>();
            if (this.Count < 1)
            {
                res.Count = PageExpression.Count();
            }

            return res;
        }
    }
    public class PageDTO<T1, T2, T3, T4, T5, T6> :
        PageParam where T2 : class where T3 : class
        where T4 : class where T5 : class where T6 : class
    {

        public ISelect<T1, T2, T3, T4, T5, T6> PageExpression { get; set; }

        public PageResult<DTO> ToPage<DTO>()
        {
            PageResult<DTO> res = new PageResult<DTO>();
            res.FromParam(this);
            res.Data = PageExpression.ToList<DTO>();
            if (this.Count < 1)
            {
                res.Count = PageExpression.Count();
            }

            return res;
        }
    }



}
