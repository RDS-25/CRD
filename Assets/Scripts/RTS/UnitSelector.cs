using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    
    public static List<ISelectable> selectedUnits = new List<ISelectable>();

    public void Select(ISelectable selectable)
    {
        selectedUnits.Add(selectable);
        selectable.Select();
    }

    public void Deselect(ISelectable selectable)
    {
        if (selectedUnits.Contains(selectable))
        {
            selectedUnits.Remove(selectable);
            selectable.Deselect();
        }
    }

    public void DeselectAll()
    {
        foreach (ISelectable selectable in selectedUnits)
        {
            selectable.Deselect();
        }
        selectedUnits.Clear();
    }

    public bool IsSelected(ISelectable selectable)
    {
        return selectedUnits.Contains(selectable);
    }

    public List<ISelectable> GetSelectedUnits()
    {
        return new List<ISelectable>(selectedUnits);
    }
}
