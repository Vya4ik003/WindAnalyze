using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UI
{
    class RelayCommand : ICommand
    {
        private Action _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        public RelayCommand(Action action, Func<object, bool> canExecuteAction = null)
        {
            _execute = action;
            _canExecute = canExecuteAction;
        }

        public bool CanExecute(object _)
        {
            return _canExecute == null || _canExecute(_);
        }

        public void Execute(object _)
        {
            _execute();
        }
    }
}
