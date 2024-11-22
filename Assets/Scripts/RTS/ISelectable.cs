public interface ISelectable
{
    void Select();
    void Deselect();
    bool IsSelected();
    void ExecuteCommand(ICommand command);
}
