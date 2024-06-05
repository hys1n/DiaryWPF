namespace Diary.Commands
{
    /// <summary>
    /// A command that relays its functionality by invoking delegates to 
    /// determine whether the command can execute and to execute the command.   
    /// </summary>
    public class RelayCommand : CommandBase
    {

        public event EventHandler? CanExecuteChanged;

        private Action<object> executeAction {  get; set; }

        private Predicate<object> canExecutePredicate { get; set; }

        public RelayCommand(Action<object> executeAction, Predicate<object> canExecutePredicate = null)
        {
            this.executeAction = executeAction;
            this.canExecutePredicate = canExecutePredicate;
        }

        public override void Execute(object? parameter)
        {
            executeAction(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(null);
        }
    }
}
