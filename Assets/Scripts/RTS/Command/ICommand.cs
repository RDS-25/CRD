public interface ICommand
{
    string Name { get; }
    bool CanExecute(ISelectable executor);
    void Execute(ISelectable executor);
    void Cancel();
}
