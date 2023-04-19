using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
