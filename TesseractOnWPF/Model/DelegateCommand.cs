using System;
using System.Windows.Input;

namespace TesseractOnWPF.Model
{
  public sealed class DelegateCommand : ICommand
    {
        private Action _excute;
        private Func<bool> _canExecute;


        public DelegateCommand(Action execute) : this(execute, () => true)
        {

        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _excute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return _canExecute();
        }

        public void Execute()
        {
            _excute();
        }



        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #region ICommand

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        #endregion
    }

    public sealed class DelegateCommand<T> : ICommand
    {

        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        private static readonly bool IS_VALUE_TYPE;

        static DelegateCommand()
        {
            IS_VALUE_TYPE = typeof(T).IsValueType;
        }

        public DelegateCommand(Action<T> execute) : this(execute, o => true) { }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(T parameter)
        {
            return _canExecute(parameter);

        }

        public void Execute(T parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(Cast(parameter));
        }

        void ICommand.Execute(object parameter)
        {
            Execute(Cast(parameter));
        }

        private T Cast(object parameter)
        {
            if (parameter == null && IS_VALUE_TYPE)
            {
                return default(T);
            }

            return (T)parameter;
        }
    }
}