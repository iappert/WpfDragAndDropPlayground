
namespace SortListView.MySortList
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class MySortListView
    {
        private IMySortListViewModel viewModel;

        private Point startPoint;

        private bool isDragging;

        public MySortListView()
        {
            this.InitializeComponent();
        }

        public static MySortListViewModel SampleData
        {
            get
            {
                var viewModel = new MySortListViewModel();

                var mappings = new List<MySortListModel>
                {
                    new MySortListModel { Name = "AAA", Action = "Action 1", SequenceNr = 1}, 
                    new MySortListModel { Name = "BBB", Action = "Action 3", SequenceNr = 2 }, 
                    new MySortListModel { Name = "CCC", Action = "Action 2", SequenceNr = 3 }, 
                    new MySortListModel { Name = "DDD", Action = "Action 4", SequenceNr = 4 }
                };

                mappings.ForEach(m => viewModel.Model.Add(m));

                return viewModel;
            }
        }

        public void Initialize(IMySortListViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }

                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);

            return null;
        }

        private void SortedListView_OnDragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void SortedListView_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                ListView listView = sender as ListView;
                int dragSource = (int)e.Data.GetData("myFormat");

                ListViewItem dragTarget = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (dragTarget == null || listView == null)
                {
                    this.isDragging = false;
                    return;
                }

                int targetSortListModel = listView.ItemContainerGenerator.IndexFromContainer(dragTarget);

                if (dragSource != -1 && targetSortListModel != -1)
                {
                    this.viewModel.MoveItemToPosition(dragSource, targetSortListModel);
                }
            }

            this.isDragging = false;
        }

        private void SortedListView_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = this.startPoint - mousePos;

            if (this.isDragging &&
                e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListView listView = sender as ListView;
                ListViewItem dragSourceObject =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (dragSourceObject == null || listView == null)
                {
                    this.isDragging = false;
                    return;
                }
                
                int sourceSortListModel = listView.ItemContainerGenerator.IndexFromContainer(dragSourceObject);


                DataObject dragData = new DataObject("myFormat", sourceSortListModel);
                DragDrop.DoDragDrop(dragSourceObject, dragData, DragDropEffects.Move);
            }
        }

        private void SortedListView_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.startPoint = e.GetPosition(null);
            if (FindAnchestor<MySortListView>((DependencyObject)e.OriginalSource) != null)
            {
                this.isDragging = true;
            }
        }
    }
}
