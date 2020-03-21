using System.ComponentModel;

namespace MeiYiJia.Abp.Workflow.Model
{
    /// <summary>
    /// 工作流状态
    /// </summary>
    public enum WorkFlowStatus
    {
        [Description("可执行")]
        Runnable = 0,
        [Description("执行中")]
        Running = 1,
        [Description("未完成")]
        UnCompleted = 2,
        [Description("成功结束")]
        CompletedWithSuccess = 3,
        [Description("失败结束")]
        CompletedWithFail = 4
    }
}