
namespace SortListView
{
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            var mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;
            this.MySortListView.Initialize(mainViewModel.MySortListViewModel);
        }
    }
}
