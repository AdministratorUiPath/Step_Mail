@echo off
REM *********************************************************
REM  �t�@�C�����FRPA �X�e�b�v���[�������o�^
REM  �쐬���@�@�F2023/11/15
REM  �X�V���@�@�F
REM  �쐬�ҁ@�@�FSFH Hirabaru
REM  ���l�@�@�@�F
REM *********************************************************

REM �����i���O�t�@�C���ɗ��p�j
SET TEMPDATE=%date:~-10,4%%date:~-5,2%%date:~-2,2%
SET TEMPTIME2=%time: =0%
SET TEMPTIME=%TEMPTIME2:~0,2%%time:~3,2%%time:~6,2%

REM ���s���O�f�B���N�g��
SET LOG_DIR=C:\�A�T�q�Ό�RPA\02�X�e�b�v���[��\03.���s\log\test\

REM ���O�t�@�C�����i�t���p�X�j
SET LOGFILE=%LOG_DIR%%TEMPDATE%_%TEMPTIME%_RunLog.log

REM �ݒ�t�@�C�����i�t���p�X�j
SET SETTING_FILE9=C:\\\\�A�T�q�Ό�RPA\\\\01�Ǘ�\\\\10_�ݒ�\\\\�ݒ�t�@�C��_�T���v��(�Ό�_9��).xlsx

REM �����L�^
SET LOG_PRO=C:\\\\�A�T�q�Ό�RPA\\\\01�Ǘ�\\\\30_�����L�^\\\\

REM ------------------------------------------
REM 1�D���s�J�n���O�o��
REM ------------------------------------------
echo ---------------------------------------->> %LOGFILE%
echo RPA �X�e�b�v���[�������쐬���s>> %LOGFILE%
echo ---------------------------------------->> %LOGFILE%
echo ���s�J�n�����F%TEMPDATE% %TEMPTIME%     >> %LOGFILE%

echo �y�J�n�zRPA �X�e�b�v���[�������쐬���s
echo �y�J�n�zRPA �X�e�b�v���[�������쐬���s >> %LOGFILE%

REM UIPath���[�g�Ɉړ�
REM �����N���A�Fcd C:\Users\user\AppData\Local\Programs\UiPath\Studio\
REM cd C:\Users\99999\AppData\Local\Programs\UiPath\Studio\

REM �{�Ԋ�
REM cd C:\Program Files\UiPath\Studio\

REM ����PC
cd C:\Users\hirabaru\AppData\Local\Programs\UiPath\Studio\

REM ------------------------------------------
REM 2�D�T���v��(�Ό�_9��)
REM ------------------------------------------

REM ------------------------------------------
REM 2-1�D�T���v��(�Ό�_9��)�ڋq�f�[�^�擾
REM ------------------------------------------
echo �y�J�n�z�ʔ̊Ǘ��V�X�e���@�T���v��(�Ό�_9��)�ڋq�f�[�^�擾
echo ---------------------------------------->> %LOGFILE%
echo �y�J�n�z�ʔ̊Ǘ��V�X�e���@�T���v��(�Ό�_9��)�ڋq�f�[�^�擾 >> %LOGFILE%

UiRobot.exe execute -p "�X�e�b�v���[��_�ʔ̊Ǘ��V�X�e���ڋq�f�[�^���o�v���W�F�N�g" --input "{'argStr�ݒ�t�@�C���p�X':'%SETTING_FILE9%','argStr�����L�^�t�H���_':'%LOG_PRO%','argStr�ݒ�V�[�g��':'�T���v��(�Ό�_9��)'}"

if %errorlevel% neq 0 (
 call :skip
 exit /b
)

echo �y�I���z�ʔ̊Ǘ��V�X�e���@�T���v��(�Ό�_9��)�ڋq�f�[�^�擾
echo �y�I���z�ʔ̊Ǘ��V�X�e���@�T���v��(�Ό�_9��)�ڋq�f�[�^�擾 >> %LOGFILE%
echo ---------------------------------------->> %LOGFILE%

REM ------------------------------------------
REM 2-2�D�T���v��(�Ό�_9��)���[�������o�^
REM ------------------------------------------
echo �y�J�n�z�z�z���[���@�T���v��(�Ό�_9��)���[�������o�^
echo ---------------------------------------->> %LOGFILE%
echo �y�J�n�z�z�z���[���@�T���v��(�Ό�_9��)���[�������o�^ >> %LOGFILE%

UiRobot.exe execute -p "�X�e�b�v���[��_�z�z���[�������o�^�v���W�F�N�g" --input "{'argStr�ݒ�t�@�C���p�X':'%SETTING_FILE9%','argStr�����L�^�t�H���_':'%LOG_PRO%','argStr�ݒ�V�[�g��':'�T���v��(�Ό�_9��)'}"

if %errorlevel% neq 0 (
 call :skip
 exit /b
)

echo �y�I���z�z�z���[���@�T���v��(�Ό�_9��)���[�������o�^
echo �y�I���z�z�z���[���@�T���v��(�Ό�_9��)���[�������o�^ >> %LOGFILE%
echo ---------------------------------------->> %LOGFILE%


REM ------------------------------------------
REM 3�D���s�I�����O�o��
REM ------------------------------------------
REM �I������
:skip

echo �y�I���zRPA �X�e�b�v���[�������쐬���s
echo �y�I���zRPA �X�e�b�v���[�������쐬���s >> %LOGFILE%

REM �I���������i���O�t�@�C���ɗ��p�j
SET TEMPDATE=%date:~-10,4%%date:~-5,2%%date:~-2,2%
SET TEMPTIME2=%time: =0%
SET TEMPTIME=%TEMPTIME2:~0,2%%time:~3,2%%time:~6,2%

echo ���s�I�������F%TEMPDATE% %TEMPTIME%     >> %LOGFILE%

exit /b

