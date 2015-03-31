namespace SortListView
{
    using System.Windows.Input;

    using SortListView.Commands;
    using SortListView.MySortList;

    public class MainViewModel
    {
        public MainViewModel()
        {
            this.MySortListViewModel = new MySortListViewModel();

            this.LoadDataCommand = new RelayCommand(() => this.MySortListViewModel.LoadData());
            this.MoveUpCommand = new RelayCommand(() => this.MySortListViewModel.MoveUp(), () => this.MySortListViewModel.CanMove);
            this.MoveDownCommand = new RelayCommand(() => this.MySortListViewModel.MoveDown(), () => this.MySortListViewModel.CanMove);
        }

        public ICommand LoadDataCommand { get; private set; }

        public ICommand MoveUpCommand { get; private set; }

        public ICommand MoveDownCommand { get; private set; }

        public IMySortListViewModel MySortListViewModel { get; private set; }
    }
}