
namespace SortListView.MySortList
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using SortListView.Commands;

    public class MySortListViewModel : INotifyPropertyChanged, IMySortListViewModel
    {
        private int selectedIndex;

        private bool canMove;

        public MySortListViewModel()
        {
            var blah = new ObservableCollection<MySortListModel>();
            blah.CollectionChanged += this.SortedDataOnCollectionChanged;

            this.Model = blah;
            this.selectedIndex = -1;

            this.DragAndDropCommand = new RelayCommand(() => this.ExecuteDragAndDropCommand());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MySortListModel> Model { get; private set; }

        public ICommand DragAndDropCommand { get; private set; }

        public IEnumerable<MySortListModel> SortedData
        {
            get
            {
                return this.Model.OrderBy(x => x.SequenceNr).ToList();
            }
        }

        public MySortListModel SelectedItem { get; set; }

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                if (value != this.selectedIndex)
                {
                    this.selectedIndex = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged("CanMove");
                }
            }
        }

        public bool CanMove
        {
            get
            {
                return this.selectedIndex >= 0;
            }
        }

        public void LoadData()
        {
            this.Model.Add(new MySortListModel()
            {
                Action = "first Action",
                Name = "first Name",
                SequenceNr = this.Model.Count
            });
            this.Model.Add(new MySortListModel()
            {
                Action = "second Action",
                Name = "second Name",
                SequenceNr = this.Model.Count
            });
            this.Model.Add(new MySortListModel()
            {
                Action = "third Action",
                Name = "third Name",
                SequenceNr = this.Model.Count
            });
        }

        public void MoveUp()
        {
            var current = this.Model.First(x => x.SequenceNr == this.SelectedIndex);
            var previous = this.Model.FirstOrDefault(x => x.SequenceNr == this.SelectedIndex - 1);
            if (previous != null)
            {
                current.SequenceNr--;
                previous.SequenceNr++;
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("SortedData"));
            }
        }

        public void MoveDown()
        {
            var current = this.Model.First(x => x.SequenceNr == this.SelectedIndex);
            var following = this.Model.FirstOrDefault(x => x.SequenceNr == this.SelectedIndex + 1);
            if (following != null)
            {
                current.SequenceNr++;
                following.SequenceNr--;
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("SortedData"));
            }
        }

        public void MoveItemToPosition(int indexOfDragSource, int indexOfDragTarget)
        {
            var source = this.Model.FirstOrDefault(x => x.SequenceNr == indexOfDragSource);
            var target = this.Model.FirstOrDefault(x => x.SequenceNr == indexOfDragTarget);
            if (source != null && target != null)
            {
                if (indexOfDragSource > indexOfDragTarget)
                {
                    for (int i = indexOfDragSource; i >= indexOfDragTarget; i--)
                    {
                        var c = this.Model.FirstOrDefault(x => x.SequenceNr == i);
                        if (c != null)
                        {
                            c.SequenceNr++;
                        }
                    }
                }
                else if (indexOfDragTarget > indexOfDragSource)
                {
                    for (int i = indexOfDragTarget; i > indexOfDragSource; i--)
                    {
                        var c = this.Model.FirstOrDefault(x => x.SequenceNr == i);
                        if (c != null)
                        {
                            c.SequenceNr--;
                        }
                    }
                }

                source.SequenceNr = indexOfDragTarget;
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("SortedData"));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SortedDataOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("SortedData"));
            }
        }

        private void ExecuteDragAndDropCommand()
        {
        }
    }
}