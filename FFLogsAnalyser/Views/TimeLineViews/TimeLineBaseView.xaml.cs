using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Caliburn.Micro;

namespace FFLogsAnalyser.Views
{
    /// <summary>
    /// Interaction logic for TimeLineBaseView.xaml
    /// </summary>
    public partial class TimeLineBaseView : UserControl
    {
        public TimeLineBaseView()
        {
            InitializeComponent();
        }

        private void TimeLineScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            TopHeader.ScrollToHorizontalOffset(e.HorizontalOffset);
            LeftHeader.ScrollToVerticalOffset(e.VerticalOffset);

        }
    }
}
