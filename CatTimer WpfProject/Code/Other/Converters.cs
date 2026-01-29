using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CatTimer_WpfProject
{
    /// <summary>
    /// 番茄钟状态转文字转换器
    /// </summary>
    public class PomodoroStateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PomodoroState state)
            {
                string key = state == PomodoroState.Work ? "Pomodoro.State.Work" : "Pomodoro.State.Rest";
                return Application.Current.FindResource(key) as string ?? (state == PomodoroState.Work ? "专注中" : "休息中");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 番茄钟模式转可见性转换器
    /// </summary>
    public class TimerModeToPomodoroVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimerMode mode)
            {
                return mode == TimerMode.Pomodoro ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 番茄钟状态转颜色转换器
    /// </summary>
    public class PomodoroStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PomodoroState state)
            {
                return state == PomodoroState.Work ? "#FF656565" : "#FF4CAF50"; // 专注灰，休息绿
            }
            return "#FF656565";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 番茄钟周期转文字转换器
    /// </summary>
    public class PomodoroCycleToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string format = Application.Current.FindResource("Pomodoro.Cycle.Format") as string ?? "第 {0} 轮";
                return string.Format(format, value);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
