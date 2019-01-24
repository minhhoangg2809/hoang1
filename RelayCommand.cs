 class RelayCommand<T> :ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

             _canExecute=canExecute;
             _execute=execute;
        }

        public bool CanExecute(object paramenter) 
        {
            return _canExecute == null ? true : _canExecute((T)paramenter);
        }

        public void Execute(object paramenter) 
        {
            _execute((T)paramenter);
        }

        public event EventHandler CanExecuteChanged 
        {
            add {CommandManager.RequerySuggested +=value; }
             remove {CommandManager.RequerySuggested -=value; }
        }
    }
}