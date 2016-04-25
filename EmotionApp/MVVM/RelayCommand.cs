using System;
using System.Windows.Input;

namespace EmotionApp.MVVM
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action invokeAction)
        {
            _action = invokeAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}
