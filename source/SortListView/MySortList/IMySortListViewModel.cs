
namespace SortListView.MySortList
{
    public interface IMySortListViewModel
    {
        bool CanMove { get; }

        void MoveUp();

        void MoveDown();

        void MoveItemToPosition(int indexOfDragSource, int indexOfDragTarget);

        void LoadData();
    }
}