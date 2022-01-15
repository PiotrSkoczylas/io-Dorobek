using System;
using System.Windows.Input;

namespace io_Dorobek.ViewModel
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _predicate;

        public RelayCommand(Action<object> action, Predicate<object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException($"No argument {nameof(action)}");
            }
            else
            {
                _execute = action;
                _predicate = predicate;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_predicate != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_predicate != null) CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_predicate == null)
            {
                return true;
            }
            return _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
