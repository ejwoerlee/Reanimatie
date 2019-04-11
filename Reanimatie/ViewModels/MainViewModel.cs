using System;
using DevExpress.Mvvm;

namespace Reanimatie.ViewModels
{
    using System.Collections.Generic;
    using System.Net.Mime;
    using System.Security.AccessControl;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using DevExpress.Utils.Commands;
    using DevExpress.XtraExport.Xls;

    public class SolidColorBrushComparer : IEqualityComparer<SolidColorBrush>
    {
        public bool Equals(SolidColorBrush x, SolidColorBrush y)
        {
            return x.Color == y.Color &&
                   x.Opacity == y.Opacity;
        }

        public int GetHashCode(SolidColorBrush obj)
        {
            return new { C = obj.Color, O = obj.Opacity }.GetHashCode();
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private System.Windows.Threading.DispatcherTimer _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private SolidColorBrush _colorBlue = new SolidColorBrush(Colors.Blue);
        private SolidColorBrush _colorRed = new SolidColorBrush(Colors.Red);

        private string _txtBlue = string.Empty;
        private string _txtRed = "PUSH!";

        private SolidColorBrush _color = new SolidColorBrush(Colors.Blue);
        public SolidColorBrush CanvasColor
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChanged(nameof(CanvasColor));
            }
        }

        private string _textColor = string.Empty;

        public string TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                RaisePropertyChanged(nameof(TextColor));
            }
        }

        private string _timerLabelText = string.Empty;

        public string TimerLabelText
        {
            get { return _timerLabelText; }
            set
            {
                _timerLabelText = value;
                RaisePropertiesChanged(nameof(TimerLabelText));
            }
        }

        public ICommand StartCommand { get; set; }

        public MainViewModel() 
        {
            this.StartCommand = new DelegateCommand(ExecuteStartCommand);
            _dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void ExecuteStartCommand()
        {          
            _dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            CanvasColor = CanvasColor.Color == _colorBlue.Color ? _colorRed : _colorBlue;
            TextColor = TextColor == _txtBlue ? _txtRed : _txtBlue;
            TimerLabelText = DateTime.Now.ToString("ss.fff");
        }

        // There are many situations where you would need something in your application to occur at a given interval, and using the DispatcherTimer,
        // it's quite easy to accomplish. Just be aware that if you do something complicated in your Tick event, it shouldn't run too often, like
        // in the last example where the timer ticks each millisecond - that will put a heavy strain on the computer running your application.
        
        // Also be aware that the DispatcherTimer is not 100% precise in all situations. The tick operations are placed on the Dispatcher queue,
        // so if the computer is under a lot of pressure, your operation might be delayed.The.NET framework promises that the Tick event will
        // never occur too early, but can't promise that it won't be slightly delayed. However, for most use cases,
        // the DispatcherTimer is more than precise enough.

    }

   
}