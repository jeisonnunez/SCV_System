using BeautySolutions.View.ViewModel;
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
using System.Collections;
using System.Windows.Media.Animation;

namespace Vista
{
  /// <summary>
  /// Interaction logic for UserControlMenuItem.xaml
  /// </summary>
  public partial class UserControlMenuItem : UserControl
  {
        public Menu _context;
        public UserControlMenuItem(ItemMenu itemMenu, Menu context)
        {
            InitializeComponent();

            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;

            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;
             
            this.DataContext = itemMenu;

            
        }
        
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            item.IsSelected = false;

            if (item != null)
            {

                _context.SwitchScreen(((SubItem)((ListViewItem)sender).DataContext).Screen, ((SubItem)((ListViewItem)sender).DataContext).IsValid);

                ClearListView();

            }



        }

        private void ClearListView()
        {

            ListViewMenu.SelectedItems.Clear();

            ListViewMenu.UnselectAll();

        }

        private void ExpanderMenu_Collapsed(object sender, RoutedEventArgs e)
        {

            ClearListView();


        }

        private void ExpanderMenu_Expanded(object sender, RoutedEventArgs e)
        {
            ClearListView();
            

        }
    }
}
