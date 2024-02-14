using UiPath.CodedWorkflows;

namespace ステップメール_通販管理システム顧客データ抽出プロジェクト
{
    public partial class CodedWorkflow : CodedWorkflowBase
    {
        public CodedWorkflow()
        {
            _ = new System.Type[]{};
            workflows = new WorkflowRunnerService(this.RunWorkflow);
        }

        protected WorkflowRunnerService workflows { get; private set; }
    }
}