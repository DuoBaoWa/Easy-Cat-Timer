using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CatTimer_WpfProject
{
    /// <summary>
    /// 设置的数据
    /// </summary>
    public class SettingData : INotifyPropertyChanged
    {
        /* 设置相关 */
        private int volume;//音量
        private LanguageType language;//语言
        private bool isTopmost;//是否置顶
        private bool isPomodoroEnabled;//是否开启番茄钟
        private int pomodoroWorkTime;//番茄钟工作时间
        private int pomodoroRestTime;//番茄钟休息时间
        private int pomodoroCycleCount;//番茄钟循环次数

        #region 公开属性
        /// <summary>
        /// 音量
        /// </summary>
        public int Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                PropertyChange("Volume");//更新UI
                AppManager.AppSystems.AudioSystem.OnVolumeChange(volume);//触发[音量更改]的事件
            }
        }

        /// <summary>
        /// 语言
        /// </summary>
        public LanguageType Language
        {
            get { return language; }
            set
            {
                language = value;
                PropertyChange("Language");//更新UI
            }
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTopmost
        {
            get { return isTopmost; }
            set
            {
                isTopmost = value;
                PropertyChange("IsTopmost");//更新UI
            }
        }

        /// <summary>
        /// 是否开启番茄钟
        /// </summary>
        public bool IsPomodoroEnabled
        {
            get { return isPomodoroEnabled; }
            set
            {
                isPomodoroEnabled = value;
                PropertyChange("IsPomodoroEnabled");//更新UI
            }
        }

        /// <summary>
        /// 番茄钟工作时间（分钟）
        /// </summary>
        public int PomodoroWorkTime
        {
            get { return pomodoroWorkTime; }
            set
            {
                pomodoroWorkTime = value;
                PropertyChange("PomodoroWorkTime");//更新UI
            }
        }

        /// <summary>
        /// 番茄钟休息时间（分钟）
        /// </summary>
        public int PomodoroRestTime
        {
            get { return pomodoroRestTime; }
            set
            {
                pomodoroRestTime = value;
                PropertyChange("PomodoroRestTime");//更新UI
            }
        }

        /// <summary>
        /// 番茄钟循环次数
        /// </summary>
        public int PomodoroCycleCount
        {
            get { return pomodoroCycleCount; }
            set
            {
                pomodoroCycleCount = value;
                PropertyChange("PomodoroCycleCount");//更新UI
            }
        }
        #endregion

        #region 构造方法
        public SettingData()
        {
            volume = 100;
            language = LanguageType.Chinese;
            isTopmost = false;
            isPomodoroEnabled = false;
            pomodoroWorkTime = 25;
            pomodoroRestTime = 5;
            pomodoroCycleCount = 4;
        }
        #endregion 构造方法


        #region 数据的双向绑定-更新方法
        /// <summary>
        /// 当属性改变的时候，就触发此方法
        /// </summary>
        /// <param name="propertyName">发生改变的属性的名字</param>
        private void PropertyChange(string propertyName)
        {
            if (PropertyChanged != null)//如果此事件被监听
            {
                //就发送通知
                //参数1：是哪个数据类的对象发生了改变？
                //参数2：发生改变的属性名
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 系统会自动监听此事件
        /// 如果此事件触发了，系统就会去通知相应的控件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
