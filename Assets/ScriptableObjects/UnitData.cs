using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public GameObject prefab;
    public float health;
    public float moveSpeed;
    public float attackRange;
    public float attackDamage;
    public float productionTime;
}
