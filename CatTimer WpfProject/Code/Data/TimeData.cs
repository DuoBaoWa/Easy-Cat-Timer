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
    /// 时间的数据
    /// </summary>
    public class TimeData : INotifyPropertyChanged
    {
        private DayTime currentTime;//当前的时间
        private DayTime inputTime;//用户输入的时间（用户想要倒计时多少分钟？）
        private PomodoroState pomodoroState;//番茄钟当前状态
        private int currentCycle;//当前番茄钟循环次数


        #region 公开属性
        /// <summary>
        /// 当前的时间
        /// </summary>
        public DayTime CurrentTime
        {
            get { return currentTime; }
        }

        /// <summary>
        /// 用户输入的时间（用户想要倒计时多少分钟？）
        /// </summary>
        public DayTime InputTime
        {
            get { return inputTime; }
        }

        /// <summary>
        /// 番茄钟当前状态
        /// </summary>
        public PomodoroState PomodoroState
        {
            get { return pomodoroState; }
            set
            {
                pomodoroState = value;
                PropertyChange("PomodoroState");
            }
        }

        /// <summary>
        /// 当前番茄钟循环次数
        /// </summary>
        public int CurrentCycle
        {
            get { return currentCycle; }
            set
            {
                currentCycle = value;
                PropertyChange("CurrentCycle");
            }
        }
        #endregion

        #region 构造方法
        public TimeData()
        {
            currentTime = new DayTime(0);
            inputTime = new DayTime(0);
            pomodoroState = PomodoroState.Work;
            currentCycle = 1;
        }
        #endregion

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
