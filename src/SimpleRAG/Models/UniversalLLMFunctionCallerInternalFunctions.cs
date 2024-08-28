using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRAG.Models
{
    internal class UniversalLLMFunctionCallerInternalFunctions
    {
        //   [KernelFunction, Description("Call this when the workflow is done and there are no more functions to call")]
        //   public string Finished(
        //  [Description("Wrap up what was done and what the result is, be concise")] string finalmessage
        //)
        //   {
        //       return string.Empty;
        //       //no actual implementation, for internal routing only
        //   }
        [KernelFunction, Description("当工作流程完成，没有更多的函数需要调用时，调用这个函数")]
        public string Finished(
       [Description("总结已完成的工作和结果，尽量简洁明了。")] string finalmessage
     )
        {
            return string.Empty;
            //no actual implementation, for internal routing only
        }
        //[KernelFunction, Description("Gets the name of the spaceship of the user")]
        //public string GetMySpaceshipName()
        //{
        //    return "MSS3000";
        //}
        [KernelFunction, Description("获取用户飞船的名称")]
        public string GetMySpaceshipName()
        {
            return "嫦娥一号";
        }
        //   [KernelFunction, Description("Starts a Spaceship")]
        //   public void StartSpaceship(
        //  [Description("The name of the spaceship to start")] string ship_name
        //)
        //   {
        //       //no actual implementation, for internal routing only
        //   }

        [KernelFunction, Description("启动飞船")]
        public void StartSpaceship(
     [Description("启动的飞船的名字")] string ship_name
   )
        {
            //no actual implementation, for internal routing only
        }

    }
}
