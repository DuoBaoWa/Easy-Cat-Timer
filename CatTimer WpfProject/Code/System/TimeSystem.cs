using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Threading;

namespace CatTimer_WpfProject
{
    /// <summary>
    /// 时间的系统
    /// </summary>
    public class TimeSystem
    {
        private DispatcherTimer timer;//计时器


        #region 构造方法
        public TimeSystem()
        {
            //设置定时器
            timer = new DispatcherTimer();//定时器：类似于Unity中的SuperInvoke插件
            timer.Interval = new TimeSpan(0, 0, 1);//定时器的间隔时间（1秒）：每隔多少秒，调用一次代码？
            timer.Tick += TimerOnTick;//要调用的代码
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 开始倒计时
        /// </summary>
        public void StartHandle()
        {
            //修改标识符
            AppManager.AppDatas.StateData.CurrentState = StateType.Run;

            //开始运行计时器
            timer.Start();

            // 如果是番茄钟模式或正向计时模式，开始时弹出通知
            if (AppManager.AppDatas.StateData.CurrentMode == TimerMode.Pomodoro)
            {
                AppManager.AppSystems.NotificationSystem.ShowNotification(true, "开始专注吧！");
            }
            else if (AppManager.AppDatas.StateData.CurrentMode == TimerMode.Forward)
            {
                AppManager.AppSystems.NotificationSystem.ShowNotification(true);
            }
        }


        /// <summary>
        /// 取消暂停倒计时
        /// </summary>
        public void UnPauseHandle()
        {
            //修改标识符
            AppManager.AppDatas.StateData.CurrentState = StateType.Run;
        }


        /// <summary>
        /// 暂停倒计时
        /// </summary>
        public void PauseHandle()
        {
            //修改标识符
            AppManager.AppDatas.StateData.CurrentState = StateType.Pause;
        }


        /// <summary>
        /// 停止倒计时
        /// </summary>
        public void StopHandle()
        {
            //修改标识符
            AppManager.AppDatas.StateData.CurrentState = StateType.Stop;

            //停止计时器
            timer.Stop();
        }
        #endregion


        #region 私有方法 -[定时器的事件]
        // 定时器的Tick事件：当定时器每次达到间隔时间时，都会触发一次Tick事件
        private void TimerOnTick(object sender, EventArgs e)
        {
            /* 判断是否进行计时？ */
            if (AppManager.AppDatas.StateData.CurrentState != StateType.Run) return;

            if (AppManager.AppDatas.StateData.CurrentMode == TimerMode.Countdown)
            {
                /* 倒计时逻辑 */
                /* 如果已经是0秒了，就停止此任务 */
                if (AppManager.AppDatas.TimeData.CurrentTime.DayToSecond <= 0)
                {
                    //停止此任务
                    StopHandle();

                    //更新[任务栏进度条]
                    AppManager.AppSystems.TaskbarSystem.SetProgressValueAndState(1, TaskbarItemProgressState.Paused);

                    //弹出通知
                    AppManager.AppSystems.NotificationSystem.ShowNotification();
                }
                else
                {
                    /* 如果要进行，就让时间减1秒 */
                    AppManager.AppDatas.TimeData.CurrentTime.AddOrRemoveSeconds(-1);

                    /* 更新[任务栏进度条] */
                    float _currentTimeSeconds = AppManager.AppDatas.TimeData.CurrentTime.DayToSecond;//当前倒计时的时间
                    float _inputTimeSeconds = AppManager.AppDatas.TimeData.InputTime.DayToSecond;//用户输入的时间
                    AppManager.AppSystems.TaskbarSystem.SetProgressValueAndState(
                        (_inputTimeSeconds - _currentTimeSeconds) / _inputTimeSeconds);//目前进度 = 当前用了多少秒 / 总时间
                }
            }
            else if (AppManager.AppDatas.StateData.CurrentMode == TimerMode.Pomodoro)
            {
                /* 番茄钟逻辑 */
                if (AppManager.AppDatas.TimeData.CurrentTime.DayToSecond <= 0)
                {
                    if (AppManager.AppDatas.TimeData.PomodoroState == PomodoroState.Work)
                    {
                        // 工作结束
                        if (AppManager.AppDatas.TimeData.CurrentCycle >= AppManager.AppDatas.SettingData.PomodoroCycleCount)
                        {
                            // 所有循环结束
                            StopHandle();
                            AppManager.AppSystems.TaskbarSystem.SetProgressValueAndState(1, TaskbarItemProgressState.Paused);
                            string msg = Application.Current.FindResource("Notification.Pomodoro.AllComplete") as string ?? "番茄钟任务已全部完成！";
                            AppManager.AppSystems.NotificationSystem.ShowNotification(false, msg);
                            // 播放完成音效
                            AppManager.AppSystems.AudioSystem.PlayAudio(AudioType.Complete);
                        }
                        else
                        {
                            // 进入休息
                            AppManager.AppDatas.TimeData.PomodoroState = PomodoroState.Rest;
                            AppManager.AppDatas.TimeData.CurrentTime.DayToSecond = AppManager.AppDatas.SettingData.PomodoroRestTime * 60;
                            string msg = Application.Current.FindResource("Notification.Pomodoro.WorkEnd") as string ?? "工作结束，休息一下吧！";
                            AppManager.AppSystems.NotificationSystem.ShowNotification(false, msg);
                            // 播放休息开始音效（可以复用猫咪坐下音效）
                            AppManager.AppSystems.AudioSystem.PlayAudio(AudioType.CatDown);
                        }
                    }
                    else
                    {
                        // 休息结束，进入下一个工作循环
                        AppManager.AppDatas.TimeData.PomodoroState = PomodoroState.Work;
                        AppManager.AppDatas.TimeData.CurrentCycle++;
                        AppManager.AppDatas.TimeData.CurrentTime.DayToSecond = AppManager.AppDatas.SettingData.PomodoroWorkTime * 60;
                        string format = Application.Current.FindResource("Notification.Pomodoro.RestEnd.Format") as string ?? "休息结束，第 {0} 轮工作开始！";
                        AppManager.AppSystems.NotificationSystem.ShowNotification(true, string.Format(format, AppManager.AppDatas.TimeData.CurrentCycle));
                        // 播放工作开始音效（可以复用猫咪站起来音效）
                        AppManager.AppSystems.AudioSystem.PlayAudio(AudioType.CatUp);
                    }
                }
                else
                {
                    // 时间递减
                    AppManager.AppDatas.TimeData.CurrentTime.AddOrRemoveSeconds(-1);

                    // 更新进度条
                    float _currentTimeSeconds = AppManager.AppDatas.TimeData.CurrentTime.DayToSecond;
                    float _totalTimeSeconds = AppManager.AppDatas.TimeData.PomodoroState == PomodoroState.Work ?
                        AppManager.AppDatas.SettingData.PomodoroWorkTime * 60 :
                        AppManager.AppDatas.SettingData.PomodoroRestTime * 60;

                    if (_totalTimeSeconds > 0)
                    {
                        AppManager.AppSystems.TaskbarSystem.SetProgressValueAndState((_totalTimeSeconds - _currentTimeSeconds) / _totalTimeSeconds);
                    }
                }
            }
            else
            {
                /* 正向计时逻辑 */
                AppManager.AppDatas.TimeData.CurrentTime.AddOrRemoveSeconds(1);

                /* 更新[任务栏进度条] - 正向计时通常不显示进度，或者显示为正在运行 */
                float _currentTimeSeconds = AppManager.AppDatas.TimeData.CurrentTime.DayToSecond;
                float _inputTimeSeconds = AppManager.AppDatas.TimeData.InputTime.DayToSecond;

                if (_inputTimeSeconds > 0)
                {
                    // 如果设置了目标时间，显示进度
                    AppManager.AppSystems.TaskbarSystem.SetProgressValueAndState(_currentTimeSeconds / _inputTimeSeconds);

                    // 如果到达目标时间，停止并通知
                    if (_currentTimeSeconds >= _inputTimeSeconds)
                    {
                        StopHandle();
                        AppManager.AppSystems.TaskbarSystem.SetProgressValueAndState(1, TaskbarItemProgressState.Paused);
                        AppManager.AppSystems.NotificationSystem.ShowNotification();
                    }
                }
                else
                {
                    // 如果没有设置目标时间（从0开始），显示不确定进度
                    AppManager.AppSystems.TaskbarSystem.SetProgressValueAndState(0.5, TaskbarItemProgressState.Indeterminate);
                }
            }
        }
        #endregion
    }

}
