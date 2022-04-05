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
        SQLiteDataReader reader;
        List<int> listOfGroupsIDs = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseContent.ConnectToDatabase();
            LoadAccessLogs();
            LoadGroups();
        }

        private void LoadGroups()
        {
            listOfGroupsIDs.Clear();
            comboxBox_groupsList.Items.Clear();

            reader = DatabaseContent.GetValuesFromDB("SELECT * FROM Groups");
            while (reader.Read())
            {
                listOfGroupsIDs.Add(reader.GetInt32(0));
                comboxBox_groupsList.Items.Add(reader.GetInt32(0) + " - " + reader.GetString(1));
            }
            DatabaseContent.CloseDatabase();
        }

        private void LoadAccessLogs()
        {
            SQLiteDataReader reader = DatabaseContent.GetValuesFromDB("SELECT * FROM Logs");
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
            try
            {
                DatabaseContent.InsertIntoDatabase($"INSERT INTO Groups (id, name) values ('{listOfGroupsIDs.Count+1}','{TextBoxGroupNewName.Text}');");
                //DatabaseContent.InsertIntoDatabase($"INSERT INTO Groups (id, name,MOfromTime,MOtoTime,TUfromTime,TUtoTime,WEfromTime,WEtoTime,THfromTime,THtoTime,FRfromTime,FRtoTime,SAfromTime,SAtoTime,SUfromTime,SUtoTime) vales ();");
                DatabaseContent.CloseDatabase();
                LoadGroups();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                throw;
            }
        }

        private void ButtonDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseContent.InsertIntoDatabase($"DELETE FROM Groups WHERE id = '{listOfGroupsIDs[comboxBox_groupsList.SelectedIndex]}'");
                DatabaseContent.CloseDatabase();
                MessageBox.Show("Úspěšně vymazáno.");
                LoadGroups();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                throw;
            }
        }

        private void ButtonManagePeople_Click(object sender, RoutedEventArgs e)
        {
            UserManagement window = new UserManagement();
            window.ShowDialog();
        }
    }
}
