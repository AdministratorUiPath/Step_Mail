using System;
using System.Collections.Generic;
using UiPath.CodedWorkflows;
using System.Threading.Tasks;
using UiPath.Activities.Contracts;

namespace ステップメール_通販管理システム顧客データ抽出プロジェクト
{
    public class WorkflowRunnerService
    {
        private readonly Func<string, IDictionary<string, object>, TimeSpan?, bool, InvokeTargetSession, IDictionary<string, object>> _runWorkflowHandler;
        public WorkflowRunnerService(Func<string, IDictionary<string, object>, TimeSpan?, bool, InvokeTargetSession, IDictionary<string, object>> runWorkflowHandler)
        {
            _runWorkflowHandler = runWorkflowHandler;
        }

        /// <summary>
        /// Invokes the 00_ワークフロー処理/10_AsahiTsuhan/06FDM/GFDM0100.xaml
        /// </summary>
        public System.Collections.Generic.Dictionary<string, string> GFDM0100(System.Data.DataTable argD配信スケジュール, string BreakTriggerScopeInArgument, System.DateTime argSKeyDay, System.Collections.Generic.Dictionary<string, string> argDc設定ファイル)
        {
            var result = _runWorkflowHandler(@"00_ワークフロー処理\10_AsahiTsuhan\06FDM\GFDM0100.xaml", new Dictionary<string, object>{{"argD配信スケジュール", argD配信スケジュール}, {"BreakTriggerScopeInArgument", BreakTriggerScopeInArgument}, {"argSKeyDay", argSKeyDay}, {"argDc設定ファイル", argDc設定ファイル}}, default, default, default);
            return (System.Collections.Generic.Dictionary<string, string>)result["argDc設定ファイル"];
        }

        /// <summary>
        /// Invokes the Main.xaml
        /// </summary>
        public void Main(string argStr設定ファイルパス, string argStr設定シート名, string argStr処理記録フォルダ)
        {
            var result = _runWorkflowHandler(@"Main.xaml", new Dictionary<string, object>{{"argStr設定ファイルパス", argStr設定ファイルパス}, {"argStr設定シート名", argStr設定シート名}, {"argStr処理記録フォルダ", argStr処理記録フォルダ}}, default, default, default);
        }

        /// <summary>
        /// Invokes the 00_ワークフロー処理/20_汎用処理/処理記録.xaml
        /// </summary>
        public void 処理記録(string argStr記録メッセージ, string argStr処理記録フォルダ)
        {
            var result = _runWorkflowHandler(@"00_ワークフロー処理\20_汎用処理\処理記録.xaml", new Dictionary<string, object>{{"argStr記録メッセージ", argStr記録メッセージ}, {"argStr処理記録フォルダ", argStr処理記録フォルダ}}, default, default, default);
        }

        /// <summary>
        /// Invokes the 00_ワークフロー処理/10_AsahiTsuhan/06FDM/GFDM0000.xaml
        /// </summary>
        public void GFDM0000(bool argBフラグ)
        {
            var result = _runWorkflowHandler(@"00_ワークフロー処理\10_AsahiTsuhan\06FDM\GFDM0000.xaml", new Dictionary<string, object>{{"argBフラグ", argBフラグ}}, default, default, default);
        }

        /// <summary>
        /// Invokes the 00_ワークフロー処理/10_AsahiTsuhan/01MNU/GMNU0000.xaml
        /// </summary>
        public System.Collections.Generic.Dictionary<string, string> GMNU0000(bool argBフラグ, System.Collections.Generic.Dictionary<string, string> argDc設定ファイル)
        {
            var result = _runWorkflowHandler(@"00_ワークフロー処理\10_AsahiTsuhan\01MNU\GMNU0000.xaml", new Dictionary<string, object>{{"argBフラグ", argBフラグ}, {"argDc設定ファイル", argDc設定ファイル}}, default, default, default);
            return (System.Collections.Generic.Dictionary<string, string>)result["argDc設定ファイル"];
        }

        /// <summary>
        /// Invokes the 00_ワークフロー処理/20_汎用処理/設定ファイル読込.xaml
        /// </summary>
        public System.Collections.Generic.Dictionary<string, string> 設定ファイル読込(string arg設定ファイルパス, string argStr処理記録フォルダ, string arg設定シート名)
        {
            var result = _runWorkflowHandler(@"00_ワークフロー処理\20_汎用処理\設定ファイル読込.xaml", new Dictionary<string, object>{{"arg設定ファイルパス", arg設定ファイルパス}, {"argStr処理記録フォルダ", argStr処理記録フォルダ}, {"arg設定シート名", arg設定シート名}}, default, default, default);
            return (System.Collections.Generic.Dictionary<string, string>)result["argDc設定ファイル"];
        }

        /// <summary>
        /// Invokes the 00_ワークフロー処理/30_配信スケジュール/配信スケジュール取得.xaml
        /// </summary>
        public (System.Data.DataTable argD配信スケジュール, System.DateTime argSKeyDay, bool argFlg, string argSMessage, System.Collections.Generic.Dictionary<string, string> argDc設定ファイル, int argIMaxDay) 配信スケジュール取得(System.Collections.Generic.Dictionary<string, string> argDc設定ファイル, int argIMaxDay)
        {
            var result = _runWorkflowHandler(@"00_ワークフロー処理\30_配信スケジュール\配信スケジュール取得.xaml", new Dictionary<string, object>{{"argDc設定ファイル", argDc設定ファイル}, {"argIMaxDay", argIMaxDay}}, default, default, default);
            return ((System.Data.DataTable)result["argD配信スケジュール"], (System.DateTime)result["argSKeyDay"], (bool)result["argFlg"], (string)result["argSMessage"], (System.Collections.Generic.Dictionary<string, string>)result["argDc設定ファイル"], (int)result["argIMaxDay"]);
        }

        /// <summary>
        /// Invokes the 00_ワークフロー処理/20_汎用処理/異常終了.xaml
        /// </summary>
        public void 異常終了(System.Collections.Generic.Dictionary<string, string> argDc設定ファイル, System.Exception argExp異常)
        {
            var result = _runWorkflowHandler(@"00_ワークフロー処理\20_汎用処理\異常終了.xaml", new Dictionary<string, object>{{"argDc設定ファイル", argDc設定ファイル}, {"argExp異常", argExp異常}}, default, default, default);
        }

        /// <summary>
        /// Invokes the 00_ワークフロー処理/20_汎用処理/ファイルチェック.xaml
        /// </summary>
        public void ファイルチェック(string argStrファイルパス)
        {
            var result = _runWorkflowHandler(@"00_ワークフロー処理\20_汎用処理\ファイルチェック.xaml", new Dictionary<string, object>{{"argStrファイルパス", argStrファイルパス}}, default, default, default);
        }

        /// <summary>
        /// Invokes the ワークフロー.cs
        /// </summary>
        public void ワークフロー()
        {
            var result = _runWorkflowHandler(@"ワークフロー.cs", new Dictionary<string, object>{}, default, default, default);
        }
    }
}