using System.Collections.Generic;
using UnityEngine;

public class CommandProvider : MonoBehaviour
{
    public List<ICommand> GetAvailableCommands()
    {
        return new List<ICommand>(GetComponents<ICommand>());
    }
}
