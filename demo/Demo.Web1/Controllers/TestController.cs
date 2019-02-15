﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web1.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ZL.CommandCore.Abs;
using ZL.CommandCore.Authorization;
using ZL.CommandCore.Cachable;

namespace Demo.Web1.Controllers
{
    [Route("api/test/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController(IServiceProvider provider)
        {
            _p = provider;
        }

        /// <summary>
        /// 接口调用例子1
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public Test1Result Test1(Test1Parameter parameter)
        {
            return _p.GetService<Test1Command>().Execute(parameter) as Test1Result;
        }


        /// <summary>
        /// 接口调用例子2(带缓存)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Cache(Key = "test2")]
        public Test2Result Test2(Test2Parameter parameter)
        {
            return _p.GetService<Test2Command>().Execute(parameter) as Test2Result;
        }


        /// <summary>
        /// 接口调用例子3
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public Test3Result Test3(Test3Parameter parameter)
        {
            return _p.GetService<Test3Command>().Execute(parameter) as Test3Result;
        }

        private IServiceProvider _p;
    }
}