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
using System.Data.SQLite;
using System.ComponentModel;

namespace AccessSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseContent.ConnectToDatabase();
            LoadAccessLogs();
        }

        private void LoadAccessLogs()
        {
            string sqlCommand = "SELECT * FROM Logs";
            SQLiteDataReader reader = DatabaseContent.GetValuesFromDB(sqlCommand);
            while (reader.Read())
            {
                ListViewLogsList.Items.Insert(0, new Log { Id = reader.GetInt32(0) , userID = uint.Parse(reader.GetInt32(1).ToString()), TimeStamp = reader.GetString(2).ToString()});
                //MessageBox.Show(reader.ToString());
            }
            DatabaseContent.CloseDatabase();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = "Po ukončení programu se ztratí veškerá neuložená data, chcete program vypnout?";
            string title = "Pozor ukončení programu.";

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    DatabaseContent.CloseDatabase();
                    break;

                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            string message = "Po ukončení programu se ztratí veškerá neuložená data, chcete program vypnout?";
            string title = "Pozor ukončení programu.";

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    DatabaseContent.CloseDatabase();
                    break;

                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void ButtonSaveGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDeleteGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonManagePeople_Click(object sender, RoutedEventArgs e)
        {
            UserManagement window = new UserManagement();
            window.ShowDialog();
        }
    }
}
