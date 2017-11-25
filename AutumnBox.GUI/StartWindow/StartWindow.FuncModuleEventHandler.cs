/* =============================================================================*\
*
* Filename: Main.FuncFinishedHandler.cs
* Description: 
*
* Version: 1.0
* Created: 10/6/2017 03:31:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.GUI
{
    using System;
    using System.Windows;
    using AutumnBox.Basic.Function.Event;
    using AutumnBox.Basic.Function;
    using AutumnBox.Basic.Function.Modules;
    using AutumnBox.GUI.Helper;
    using AutumnBox.GUI.Windows;
    using AutumnBox.Support.CstmDebug;

    /// <summary>
    /// 各种界面事件
    /// </summary>
    public partial class StartWindow
    {
        /// <summary>
        /// 通用事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FuncFinish(object sender, FinishEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                UIHelper.CloseRateBox();
                switch (sender.GetType().Name)
                {
                    case nameof(FileSender):
                        PushFinish(e);
                        break;
                    //case nameof(BreventServiceActivator):
                    //    ActivatedBrvent(e);
                    //    break;
                    case nameof(ActivityLauncher):
                        ActivatedBrvent(e);
                        break;
                    case nameof(CustomRecoveryFlasher):
                        FlashCustomRecFinish(e);
                        break;
                    case nameof(RebootOperator):
                        break;
                    case nameof(SystemUnlocker):
                        UnlockMiSystemFinish(e);
                        break;
                    case nameof(XiaomiBootloaderRelocker):
                        RelockMiFinish(e);
                        break;
                    case nameof(AndroidFullBackup):
                        MMessageBox.FastShow(App.OwnerWindow, UIHelper.GetString("msgNotice"), UIHelper.GetString("msgLookTheScreen"));
                        break;
                    case nameof(IceBoxActivator):
                        switch ((ErrorType)e.Other)
                        {
                            case ErrorType.DeviceOwnerIsAlreadySet:
                                e.Result.Message = UIHelper.GetString("msgNoticeDeviceOwnerIsAlreadySet");
                                e.Result.Advise = UIHelper.GetString("advsDeviceOwnerIsAlreadySet");
                                break;
                            case ErrorType.IceBoxHaveNoInstall:
                                e.Result.Message = UIHelper.GetString("msgNoticeIceBoxHaveNoInstall");
                                e.Result.Advise = UIHelper.GetString("advsIceBoxHaveNoInstall");
                                break;
                            default:
                                e.Result.Message = UIHelper.GetString("msgUnknowError");
                                e.Result.Advise = UIHelper.GetString("advIceBoxActUnknowError");
                                break;
                        }
                        ModuleResultWindow.FastShow(e.Result);
                        break;
                    default:
                        ModuleResultWindow.FastShow(e.Result);
                        break;
                }
            });
        }

        /// <summary>
        /// 黑域启动完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivatedBrvent(FinishEventArgs e)
        {
            Logger.D(e.OutputData.Error.ToString());
            Logger.D(e.OutputData.Out.ToString());
            Logger.D("Enter the ActivatedBrevent Handler in the GUI");
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
            if (e.Result.Level != ResultLevel.Successful)
            {
                e.Result.Message = App.Current.Resources["errormsgBrventActivtedUnsuccess"].ToString();
                e.Result.Advise = App.Current.Resources["advsBrventActivtedUnsuccess"].ToString();
            }
            this.Dispatcher.Invoke(() =>
            {
                ModuleResultWindow.FastShow(e.Result);
            });
        }

        /// <summary>
        /// 解锁小米系统完成时的事件
        /// </summary>
        /// <param name="o"></param>
        private void UnlockMiSystemFinish(FinishEventArgs e)
        {
            Logger.D("Enter the Unlock Mi  System Finish Handler in the GUI");
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
        }

        /// <summary>
        /// 重新给小米手机上锁完成时的事件
        /// </summary>
        /// <param name="o"></param>
        private void RelockMiFinish(FinishEventArgs e)
        {
            Logger.D("Enter the Relock Mi Finish Handler in the GUI");
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
        }

        /// <summary>
        /// 推送文件到SDCARD完成的事件
        /// </summary>
        /// <param name="outputData">操作时的输出数据</param>
        private void PushFinish(FinishEventArgs e)
        {
            Logger.D("Enter the Push Finish Handler in the GUI");
            if (e.Result.Level == ResultLevel.Successful)
            {
                MMessageBox.FastShow(App.OwnerWindow, Application.Current.Resources["Notice"].ToString(), Application.Current.FindResource("msgPushOK").ToString());
            }
            else
            {
                if (e.Result.WasForcblyStop) { Logger.T("File send was force stoped by user"); return; };
                e.Result.Message = Application.Current.FindResource("msgPushFail").ToString();
                e.Result.Advise = Application.Current.FindResource("advsFileSendUnsuccess").ToString();
                ModuleResultWindow.FastShow(e.Result);
            }
        }

        /// <summary>
        /// 刷入自定义Recovery完成时发生的事件
        /// </summary>
        /// <param name="outputData">操作时的数据数据</param>
        private void FlashCustomRecFinish(FinishEventArgs e)
        {
            Logger.D("Flash Custom Recovery Finish");
            this.Dispatcher.Invoke(new Action(() =>
            {
                UIHelper.CloseRateBox();
            }));
            MMessageBox.FastShow(App.OwnerWindow, Application.Current.FindResource("Notice").ToString(), Application.Current.FindResource("msgFlashOK").ToString());
        }
    }
}
