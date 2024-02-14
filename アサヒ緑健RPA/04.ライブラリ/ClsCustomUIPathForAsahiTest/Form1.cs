using System;
using System.Collections.Generic;
using ClsCustomUIPathForAsahi;
using System.Windows.Forms;

namespace ClsCustomUIPathForAsahiTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // 2023/04/22 Modify start lincrea 中西 9通に対応
            //// 配送スケジュール
            //string HaisoSchedule = "C:\\アサヒ緑健RPA\\開発\\TesData\\4-★NEW★【サンプルステップメール】7通.xlsx";
            //// 配送スケジュール
            string HaisoSchedule = "C:\\アサヒ緑健RPA\\04.ライブラリ\\TesData\\★23.05【サンプルステップメール】9通.xlsx";
            // 2023/04/22 Modify end lincrea 中西 9通に対応

            // 配送スケジュールシート名
            string HaisoScheduleSheetName = "基本データ";
            // 取得対象日
            string dayYYYYMMDD = "20230602";

            (int iRet, Dictionary<string, Dictionary<int, string>> dicRet, Dictionary<string, int> dicRet2) = ClsMngSchedule.TestGetTargetSchedule(HaisoSchedule, HaisoScheduleSheetName, dayYYYYMMDD);
        }
    }
}
