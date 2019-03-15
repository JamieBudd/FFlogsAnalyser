using FFLogsAnalyser.FFlogsClass;
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

namespace FFLogsAnalyser
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Rankings> rankings;
        static ReportFightID reportfightID;
        static List<ReportEvent> reportEvent = new List<ReportEvent>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            //set combo box "Region" values
            foreach (var item in Enum.GetValues(typeof(Region)))
            {
                cbRegion.Items.Add(item);
            }
            cbRegion.SelectedIndex = 0;

        }
        //get fflogsapi data based on inputs and display them in next window
        private void OnClick(object sender, RoutedEventArgs e)
        { 
            if (tbName.Text != "" && tbName.Text.Contains(' ') && cbServer.SelectedValue.ToString() != "")
            {
                string Region = cbRegion.SelectedValue.ToString();
                string Server = cbServer.SelectedValue.ToString();
                string[] Name = tbName.Text.Split(' ');
                //gets the url from the data input
                string rankingsUrl = Library.characterparse(Name, Server, Region);

                //puts the parse data into a list of Rankings class
                rankings = Library._download_serialized_json_data<List<Rankings>>(rankingsUrl);
                
                //shows the parses in the data grid
                    dgCharacterParses.Visibility = Visibility.Visible;
                    dgCharacterParses.ItemsSource = rankings;
                    MessageBox.Show("success");
                
                
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }

        private void dgCharacterParsesRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = dgCharacterParses.SelectedIndex;
            string reportID = rankings[index].ReportID;
            string reportUrl = Library.report(reportID);
            string reportfighturl = "";
            if (sender != null)
            {
                DataGridRow dgr = sender as DataGridRow;
            }
            
            reportfightID = Library._download_serialized_json_data<ReportFightID>(reportUrl);
            foreach (Fight item in reportfightID.fights)
            {
                if (item.id == rankings[index].FightID)
                {
                    int i = 0;
                    int start_time = item.start_time;
                    int end_time = item.end_time;

                    //get events from FFlogs API and put it in the buff class
                    reportfighturl = Library.reportbuffs(reportID, start_time, end_time);
                    reportEvent.Add(Library._download_serialized_json_data<ReportEvent>(reportfighturl));
                    try
                    {
                        while (reportEvent[i].nextPageTimestamp != 0)
                        {
                            reportfighturl = Library.reportbuffs(reportID, reportEvent[i].nextPageTimestamp, end_time);
                            reportEvent.Add(Library._download_serialized_json_data<ReportEvent>(reportfighturl));
                            i++;
                        }
                    }
                    catch (IndexOutOfRangeException Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                    foreach (ReportEvent item1 in reportEvent)
                    {
                        MessageBox.Show(item1.nextPageTimestamp.ToString());
                    }
                }
            }
        }
        

        //set combo box "Server" Values based on region value
        private void cbRegionSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            string check = cbRegion.SelectedValue.ToString();

            switch (check)
            {
                case "EU":

                    cbServer.Items.Clear();
                    foreach (var item in Enum.GetValues(typeof(ServerEU)))
                    {
                        cbServer.Items.Add(item);
                    }
                    cbServer.SelectedIndex = 0;
                    break;

                case "NA":
                    cbServer.Items.Clear();
                    foreach (var item in Enum.GetValues(typeof(ServerNA)))
                    {
                        cbServer.Items.Add(item);
                    }
                    cbServer.SelectedIndex = 0;
                    break;
                case "JP":
                    cbServer.Items.Clear();
                    foreach (var item in Enum.GetValues(typeof(ServerJP)))
                    {
                        cbServer.Items.Add(item);
                    }
                    cbServer.SelectedIndex = 0;
                    break;
                default:
                    cbServer.Items.Clear();
                    break;
            }
        }
    }
}
