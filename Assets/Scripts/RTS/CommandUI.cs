using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandUI : MonoBehaviour
{
    public void DisplayCommands(List<ISelectable> selectables)
    {
        TextMeshProUGUI commandLabel = GetComponent<TextMeshProUGUI>();

        if (selectables.Count == 0)
        {
            commandLabel.text = "No Commands available.";
            return;
        }

        HashSet<string> commandNames = new HashSet<string>();
        foreach (ISelectable selectable in selectables )
        {
            List<ICommand> commands = (selectable as MonoBehaviour).GetComponent<CommandProvider>().GetAvailableCommands();
            foreach (ICommand command in commands)
            {
                commandNames.Add(command.Name);
            }
        }
        string commandStr = "Commands:\n";
        foreach (string names in  commandNames)
        {
            commandStr += " - " + names + "\n";
        }
        commandLabel.text = commandStr;

    }
}
