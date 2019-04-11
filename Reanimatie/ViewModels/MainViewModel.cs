using System;
using DevExpress.Mvvm;

namespace Reanimatie.ViewModels
{
    using System.Collections.Generic;
    using System.Net.Mime;
    using System.Security.AccessControl;
    using System.Threading;
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
            //if (CanvasColor.Color == _colorBlue.Color)
            //{
            //    CanvasColor = _colorRed;
            //    return;
            //}

            //if (CanvasColor.Color == _colorRed.Color)
            //{
            //    CanvasColor = _colorBlue;
            //    return;
            //}

            if (TextColor == _txtBlue)
            {
                TextColor = _txtRed;
                return;
            }

            if (TextColor == _txtRed)
            {
                TextColor = _txtBlue;
                return;
            }
        }

    }

   
}