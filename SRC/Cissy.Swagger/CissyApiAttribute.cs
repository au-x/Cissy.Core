using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cissy.Swagger
{
    /// <summary>
    /// 自定义路由 /api/{version=v1}/[controler]/[action]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class CissyApiAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 自定义路由构造函数
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="version"></param>
        public CissyApiAttribute(string version, string description = null) : base($"/api/{version.ToString()}/[controller]/[action]")
        {
            GroupName = version.ToString();
            Description = description;
        }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 接口描述
        /// </summary>
        public string Description { get; set; }
    }

}
