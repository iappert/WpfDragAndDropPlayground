
namespace SortListView
{
    using System.Windows;
    
    public partial class MainWindow : Window
    {
        private MySortListViewModel viewModel;

        private MySortListView view;

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Load_OnClick(object sender, RoutedEventArgs e)
        {
            this.MySortListView.LoadData();
        }

        private void MoveUp_OnClick(object sender, RoutedEventArgs e)
        {
            this.MySortListView.MoveUp();
        }

        private void MoveDown_OnClick(object sender, RoutedEventArgs e)
        {
            this.MySortListView.MoveDown();
        }
    }
}
