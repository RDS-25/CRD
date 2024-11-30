using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [Header("Basic Info")]
    public string buildingName;
    public GameObject prefab;
    public float constructionTime = 10f;

    [Header("Production Info")]
    public bool canProduceUnits = true;
    public UnitData[] producableUnits;
}
