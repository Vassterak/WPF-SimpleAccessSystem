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

namespace AccessSystem.Components
{
    /// <summary>
    /// Interaction logic for HoursSelect.xaml
    /// </summary>
    public partial class HoursSelect : UserControl
    {


        public string HoursName
        {
            get { return (string)GetValue(HoursNameProperty); }
            set { SetValue(HoursNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoursName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoursNameProperty =
            DependencyProperty.Register("HoursName", typeof(string), typeof(HoursSelect), new PropertyMetadata("none"));


        public HoursSelect()
        {
            InitializeComponent();
        }
    }
}
