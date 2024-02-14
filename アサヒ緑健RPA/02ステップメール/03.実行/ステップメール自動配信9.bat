@echo off
REM *********************************************************
REM  ファイル名：RPA ステップメール自動登録
REM  作成日　　：2023/11/15
REM  更新日　　：
REM  作成者　　：SFH Hirabaru
REM  備考　　　：
REM *********************************************************

REM 日時（ログファイルに利用）
SET TEMPDATE=%date:~-10,4%%date:~-5,2%%date:~-2,2%
SET TEMPTIME2=%time: =0%
SET TEMPTIME=%TEMPTIME2:~0,2%%time:~3,2%%time:~6,2%

REM 実行ログディレクトリ
SET LOG_DIR=C:\アサヒ緑健RPA\02ステップメール\03.実行\log\test\

REM ログファイル名（フルパス）
SET LOGFILE=%LOG_DIR%%TEMPDATE%_%TEMPTIME%_RunLog.log

REM 設定ファイル名（フルパス）
SET SETTING_FILE9=C:\\\\アサヒ緑健RPA\\\\01管理\\\\10_設定\\\\設定ファイル_サンプル(緑効_9通).xlsx

REM 処理記録
SET LOG_PRO=C:\\\\アサヒ緑健RPA\\\\01管理\\\\30_処理記録\\\\

REM ------------------------------------------
REM 1．実行開始ログ出力
REM ------------------------------------------
echo ---------------------------------------->> %LOGFILE%
echo RPA ステップメール自動作成実行>> %LOGFILE%
echo ---------------------------------------->> %LOGFILE%
echo 実行開始日時：%TEMPDATE% %TEMPTIME%     >> %LOGFILE%

echo 【開始】RPA ステップメール自動作成実行
echo 【開始】RPA ステップメール自動作成実行 >> %LOGFILE%

REM UIPathルートに移動
REM リンクレア：cd C:\Users\user\AppData\Local\Programs\UiPath\Studio\
REM cd C:\Users\99999\AppData\Local\Programs\UiPath\Studio\

REM 本番環境
REM cd C:\Program Files\UiPath\Studio\

REM 平原PC
cd C:\Users\hirabaru\AppData\Local\Programs\UiPath\Studio\

REM ------------------------------------------
REM 2．サンプル(緑効_9通)
REM ------------------------------------------

REM ------------------------------------------
REM 2-1．サンプル(緑効_9通)顧客データ取得
REM ------------------------------------------
echo 【開始】通販管理システム　サンプル(緑効_9通)顧客データ取得
echo ---------------------------------------->> %LOGFILE%
echo 【開始】通販管理システム　サンプル(緑効_9通)顧客データ取得 >> %LOGFILE%

UiRobot.exe execute -p "ステップメール_通販管理システム顧客データ抽出プロジェクト" --input "{'argStr設定ファイルパス':'%SETTING_FILE9%','argStr処理記録フォルダ':'%LOG_PRO%','argStr設定シート名':'サンプル(緑効_9通)'}"

if %errorlevel% neq 0 (
 call :skip
 exit /b
)

echo 【終了】通販管理システム　サンプル(緑効_9通)顧客データ取得
echo 【終了】通販管理システム　サンプル(緑効_9通)顧客データ取得 >> %LOGFILE%
echo ---------------------------------------->> %LOGFILE%

REM ------------------------------------------
REM 2-2．サンプル(緑効_9通)メール自動登録
REM ------------------------------------------
echo 【開始】配配メール　サンプル(緑効_9通)メール自動登録
echo ---------------------------------------->> %LOGFILE%
echo 【開始】配配メール　サンプル(緑効_9通)メール自動登録 >> %LOGFILE%

UiRobot.exe execute -p "ステップメール_配配メール自動登録プロジェクト" --input "{'argStr設定ファイルパス':'%SETTING_FILE9%','argStr処理記録フォルダ':'%LOG_PRO%','argStr設定シート名':'サンプル(緑効_9通)'}"

if %errorlevel% neq 0 (
 call :skip
 exit /b
)

echo 【終了】配配メール　サンプル(緑効_9通)メール自動登録
echo 【終了】配配メール　サンプル(緑効_9通)メール自動登録 >> %LOGFILE%
echo ---------------------------------------->> %LOGFILE%


REM ------------------------------------------
REM 3．実行終了ログ出力
REM ------------------------------------------
REM 終了処理
:skip

echo 【終了】RPA ステップメール自動作成実行
echo 【終了】RPA ステップメール自動作成実行 >> %LOGFILE%

REM 終了時日時（ログファイルに利用）
SET TEMPDATE=%date:~-10,4%%date:~-5,2%%date:~-2,2%
SET TEMPTIME2=%time: =0%
SET TEMPTIME=%TEMPTIME2:~0,2%%time:~3,2%%time:~6,2%

echo 実行終了日時：%TEMPDATE% %TEMPTIME%     >> %LOGFILE%

exit /b

