using System.Windows.Input;

namespace Diary.Commands
{
    /// <summary>
    /// Represents an abstract base class for commands.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        protected void OnCanExecuteChanged(object? parameter)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
