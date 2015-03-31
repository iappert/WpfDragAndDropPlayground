namespace SortListView.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// see: http://docs.telerik.com/data-access/quick-start-scenarios/wpf/quickstart-wpf-viewmodelbase-and-relaycommand
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action methodToExecute;

        private readonly Func<bool> canExecuteEvaluator;

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }
        
        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke();
        }
    }
}