using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.ViewModels;
using EmployeeDirectory.ViewModels.Directory;
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

namespace EmployeeDirectory.Views
{
    /// <summary>
    /// Логика взаимодействия для DirectoryView.xaml
    /// </summary>
    public partial class DirectoryView : UserControl
    {
        public DirectoryView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is DirectoryViewModel dc)
                if (e.NewValue is Entity item)
                    dc.SelectedItem = item;
        }
    }
}
