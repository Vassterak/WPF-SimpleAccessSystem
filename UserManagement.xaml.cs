using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Shapes;

namespace AccessSystem
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        User userForEditing;
        SQLiteDataReader reader;
        int newestIDSave = 0;
        List<int> listOfGroupsIDs = new List<int>();
        public UserManagement()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            string sqlCommand = "SELECT * FROM Users";
            reader = DatabaseContent.GetValuesFromDB(sqlCommand);
            while (reader.Read())
            {
                ListViewUserList.Items.Insert(0, new User { Id = Convert.ToUInt32(reader.GetInt32(0)), GroupID = int.Parse(reader.GetInt32(1).ToString()), FirstName = reader.GetString(2), SurName = reader.GetString(3) });

                if (reader.GetInt32(0) <= newestIDSave)
                    newestIDSave = reader.GetInt32(0) + 1;

            }

            sqlCommand = "SELECT * FROM Groups";
            reader = DatabaseContent.GetValuesFromDB(sqlCommand);
            while (reader.Read())
            {
                listOfGroupsIDs.Add(reader.GetInt32(0));
                ComboboxGroupName.Items.Add(reader.GetInt32(0) + " - " + reader.GetString(1));

                //ListViewUserList.Items.Insert(0, new User { Id = Convert.ToUInt32(reader.GetInt32(0)), GroupID = int.Parse(reader.GetInt32(1).ToString()), FirstName = reader.GetString(2), SurName = reader.GetString(3) });
            }
            DatabaseContent.CloseDatabase();
        }

        private void ButtonAddPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int groupID = listOfGroupsIDs[ComboboxGroupName.SelectedIndex - 1];
                //MessageBox.Show(groupID.ToString());
                DatabaseContent.InsertIntoDatabase($"INSERT INTO Users (id, groupID,firstName,surName) values ('{newestIDSave}','{groupID}','{LabelName.Text}','{LabelSurname.Text}')");
                DatabaseContent.CloseDatabase();
                LoadUsers();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                throw;
            }
        }

        private void ButtonDeletePerson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSaveCurrentPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int groupID = listOfGroupsIDs[ComboboxGroupName.SelectedIndex - 1];
                //MessageBox.Show(groupID.ToString());
                DatabaseContent.InsertIntoDatabase($"UPDATE Users SET groupID = '{groupID}',firstName = '{LabelName.Text}',surName = '{LabelSurname.Text}' WHERE id = '{Convert.ToUInt32(LabelID.Content)}'");
                DatabaseContent.CloseDatabase();
                LoadUsers();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                throw;
            }
        }

        private void ListViewUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userForEditing = (User)ListViewUserList.SelectedItem;

            if (userForEditing != null)
            {
                LabelID.Content = userForEditing.Id;
                LabelName.Text = userForEditing.FirstName;
                LabelSurname.Text = userForEditing.SurName;
            }
        }
    }
}
