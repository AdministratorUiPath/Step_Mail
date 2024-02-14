using System;
using System.Collections.Generic;
using System.Activities;
using System.ComponentModel;
using System.IO;
using OfficeOpenXml;

namespace ClsCustomUIPathForAsahi
{

    /// <summary>
    /// 配送スケジュール管理
    /// </summary>
    public class ClsMngSchedule : CodeActivity
    {

        #region コンスト定義

        // データ発生行
        private const int top_row = 10;

        // 2023/04/22 Modify start lincrea 中西 9通に対応
        //// 当日日付列
        //private const int today_col = 14;

        //// 1通目列
        //private const int No1_col = 16;
        //// 2通目列
        //private const int No2_col = 19;
        //// 3通目列
        //private const int No3_col = 22;
        //// 4通目列
        //private const int No4_col = 25;
        //// 5通目列
        //private const int No5_col = 28;
        //// 6通目列
        //private const int No6_col = 31;
        //// 7通目列
        //private const int No7_col = 34;

        // 当日日付列
        private const int today_col = 16;

        // 1通目列
        private const int No1_col = 18;
        // 2通目列
        private const int No2_col = 21;
        // 3通目列
        private const int No3_col = 24;
        // 4通目列
        private const int No4_col = 27;
        // 5通目列
        private const int No5_col = 30;
        // 6通目列
        private const int No6_col = 33;
        // 7通目列
        private const int No7_col = 36;
        // 8通目列
        private const int No8_col = 39;
        // 9通目列
        private const int No9_col = 42;

        // 2023/04/22 Modify end lincrea 中西 9通に対応

        // 平日
        private const int HEIJITU = 0;
        // 休日
        private const int KYUJITU = 1;

        #endregion

        #region インプット＆アウトプット

        // 配送スケジュール（パス）
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> sPathHisoSchedule { get; set; }

        // 配送スケジュール（ファイル名）
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> sFileNameHisoSchedule { get; set; }

        // 配送スケジュール（シート名）
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> sSheetNameHisoSchedule { get; set; }

        // 日付
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> sDay { get; set; }

        // 結果
        [Category("Output")]
        [RequiredArgument]
        public OutArgument<int> ResultVal { get; set; }

        // 配送日
        [Category("Output")]
        [RequiredArgument]
        public OutArgument<Dictionary<string, Dictionary<int, string>>> ResultDicDays { get; set; }

        // 配送日　平日、休日判断
        [Category("Output")]
        [RequiredArgument]
        public OutArgument<Dictionary<string, int>> ResultDicDaysHejitsuKyujitsu { get; set; }

        #endregion

        #region 実行関数

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            // 配送スケジュール
            string HaisoSchedule = Path.Combine(sPathHisoSchedule.Get(context), sFileNameHisoSchedule.Get(context));
            // 配送スケジュールシート名
            string HaisoScheduleSheetName = sSheetNameHisoSchedule.Get(context);
            // 取得対象日
            string dayYYYYMMDD = sDay.Get(context);
            // 戻り値（取得対象日に該当する配送日（翌日が休日の場合、その値も取得）
            var dicHaisoData = new Dictionary<string, Dictionary<int, string>>();
            // 戻り値（配送日と平日・休日判断用）
            var dicHaisoHeiKyu = new Dictionary<string, int>();

            // 配送スケジュール取得
            var ret = GetTargetSchedule(dicHaisoData, dicHaisoHeiKyu, HaisoSchedule, HaisoScheduleSheetName, dayYYYYMMDD);

            // 戻り値
            ResultVal.Set(context, ret);
            // 配送スケジュール
            ResultDicDays.Set(context, dicHaisoData);
            // 配送日の平日休日
            ResultDicDaysHejitsuKyujitsu.Set(context, dicHaisoHeiKyu);
        }

        /// <summary>
        /// テスト用実行関数
        /// </summary>
        /// <param name="HaisoSchedule"></param>
        /// <param name="HaisoScheduleSheetName"></param>
        /// <param name="dayYYYYMMDD"></param>
        /// <returns></returns>
        public static (int, Dictionary<string, Dictionary<int, string>>, Dictionary<string,int>) TestGetTargetSchedule(string HaisoSchedule, string HaisoScheduleSheetName, string dayYYYYMMDD)
        {

            // 戻り値（取得対象日に該当する配送日（翌日が休日の場合、その値も取得）
            var dicHaisoData = new Dictionary<string, Dictionary<int, string>>();
            // 戻り値（配送日と平日・休日判断用）
            var dicHaisoHeiKyu = new Dictionary<string, int>();

            // 配送スケジュール取得
            var ret = GetTargetSchedule(dicHaisoData, dicHaisoHeiKyu, HaisoSchedule, HaisoScheduleSheetName, dayYYYYMMDD);

            return (ret, dicHaisoData, dicHaisoHeiKyu);
        }

        #endregion

        #region 処理関数

        /// <summary>
        /// 対象スケジュール取得
        /// </summary>
        /// <param name="dicHaisoData">配送日ディクショナリ</param>
        /// <param name="dicHeiKyu">配送日 平日・休日判断ディクショナリ</param>
        /// <param name="fullpath">設定ファイル（フルパス）</param>
        /// <param name="sheetName">シートネーム</param>
        /// <param name="day">対象の日付</param>
        /// <returns>0:正常終了、-1:値がない、-2:設定ファイルがない、-3:例外発生</returns>
        private static int GetTargetSchedule(Dictionary<string, Dictionary<int, string>> dicHaisoData, Dictionary<string,int> dicHeiKyu, string fullpath, string sheetName, string day)
        {
            var ret = -1;
            try
            {
                // 引数の値(ターゲットとなる日付）
                var dDay = DateTime.ParseExact(day, "yyyyMMdd", null);

                // 設定ファイルの有無
                if (System.IO.File.Exists(fullpath))
                {
                    // ファイル
                    var fi = new FileInfo(fullpath);

                    // EPPlus利用(Ver 4系：LGPL）
                    using (var package = new ExcelPackage(fi))
                    {
                        // ワークシート取得
                        var worksheet = package.Workbook.Worksheets[sheetName];

                        // 日付開始行 
                        var row = top_row;

                        while (true)
                        {
                            // 日付の取得
                            var valDay = worksheet.GetValue(row, today_col).ToString();

                            // 値がなければ終了
                            if (bCheckVal(valDay) == false) break;

                            // 対象の日付
                            var targetDay = GetTargetDayDateTime(valDay);

                            // 本日の日付か確認
                            if (dDay.Equals(targetDay))
                            {
                                // 配送日設定用ディクショナリのインスタンス化
                                var dicMei = new Dictionary<int, string>();

                                // 配送日設定
                                SetRowData(worksheet, row, dicMei);

                                // 本日分の配送日を戻り値に設定
                                // yyyyMMddを設定
                                dicHaisoData.Add(targetDay.ToString("yyyyMMdd"), dicMei);
                                // 平日を設定
                                dicHeiKyu.Add(targetDay.ToString("yyyyMMdd"), HEIJITU);

                                // 翌日（翌々日も対応）用の処理
                                var nextRow = row + 1;

                                // 翌日の処理
                                while (true)
                                {
                                    // 翌日の配送日があるか(値がなければ、現在の日の翌日は休日でその日分も作成しないといけない
                                    if (bCheckVal(worksheet.GetValue(nextRow, No1_col)) == false)
                                    {

                                        // 配送日設定用ディクショナリのインスタンス化
                                        dicMei = new Dictionary<int, string>();

                                        // 配送日設定
                                        SetRowData(worksheet, nextRow, dicMei);

                                        // 対象の日付
                                        targetDay = GetTargetDayDateTime(worksheet.GetValue(nextRow, today_col).ToString());

                                        // 翌日分の配送日を戻り値に設定
                                        // yyyyMMddを設定
                                        dicHaisoData.Add(targetDay.ToString("yyyyMMdd") , dicMei);
                                        // 休日を設定
                                        dicHeiKyu.Add(targetDay.ToString("yyyyMMdd"), KYUJITU);
                                    }
                                    else
                                    {
                                        // LOOPを抜ける。
                                        break;
                                    }
                                    // 次の日
                                    nextRow++;
                                }
                                // 正常終了
                                ret = 0;
                                // 処理終了（もう値は設定している）
                                break;
                            }
                            // 次の行へ
                            row++;
                        }
                    }
                }
                else
                {
                    // 設定ファイルがない
                    ret = -2;
                }
            }
            catch (Exception)
            {
                // 何もしない
                ret = -3;
            }
            return ret;
        }

        /// <summary>
        /// 配送日をディクショナリに設定
        /// </summary>
        /// <param name="worksheet">対象のエクセルシート</param>
        /// <param name="RowNumber">対象の行</param>
        /// <param name="dicMei">配送日設定ディクショナリ</param>
        private static void SetRowData(ExcelWorksheet worksheet, int RowNumber, Dictionary<int, string> dicMei)
        {

            // 初期化
            dicMei.Clear();

            // 2023/04/22 Mod start lincrea 中西 9通に対応
            // // ７回目の配送日までLOOP
            // for (int cnt = No1_col; cnt <= No7_col; cnt++)
            // ９回目の配送日までLOOP
            for (int cnt = No1_col; cnt <= No9_col; cnt++)
            // 2023/04/22 Mod end lincrea 中西 9通に対応
            {
                // 値の設定があるか？
                if (bCheckVal(worksheet.GetValue(RowNumber, cnt)))
                {
                    // セルの値を取得
                    var valCell = worksheet.GetValue(RowNumber, cnt).ToString();

                    // キーワード
                    var keyWord = -1;
                    switch (cnt)
                    {
                        case No1_col:
                            keyWord = 1;
                            break;
                        case No2_col:
                            keyWord = 2;
                            break;
                        case No3_col:
                            keyWord = 3;
                            break;
                        case No4_col:
                            keyWord = 4;
                            break;
                        case No5_col:
                            keyWord = 5;
                            break;
                        case No6_col:
                            keyWord = 6;
                            break;
                        case No7_col:
                            keyWord = 7;
                            break;
                        // 2023/04/22 Add start lincrea 中西 9通に対応
                        case No8_col:
                            keyWord = 8;
                            break;
                        case No9_col:
                            keyWord = 9;
                            break;
                            // 2023/04/22 Add end lincrea 中西 9通に対応
                    }

                    // 該当の列(配送日設定セル)であれば
                    if (keyWord != -1)
                    {
                        // 文字列変換
                        var val = GetTargetDayDateTime(valCell).ToString("yyyyMMdd");
                        // 設定
                        dicMei.Add(keyWord, val);
                    }
                }

            }
        }

        /// <summary>
        /// 日付（シリアル値）をDateTime型に変換
        /// </summary>
        /// <param name="CellData">セルの値</param>
        /// <returns>DateTime</returns>
        private static DateTime GetTargetDayDateTime(string CellData)
        {
            // 数値変換
            int.TryParse(CellData, out int ivalDay);

            // シリアル値を日付に変換
            return DateTime.FromOADate(ivalDay);
        }

        /// <summary>
        /// Null/空チェック
        /// </summary>
        /// <param name="val">対象オブジェクト</param>
        /// <returns>True：値あり、False:値なし</returns>
        private static bool bCheckVal(object val)
        {
            if (val == null) return false;

            if (string.IsNullOrEmpty(val.ToString())) return false;

            return true;
        }

        #endregion

    }
}
