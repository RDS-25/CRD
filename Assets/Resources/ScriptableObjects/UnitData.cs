using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public string ID;
    public string unitName;
    public Sprite attackicon;
    public float health;
    public float magicPoint;
    public float moveSpeed;
    public float attackRange;
    public float attackDamage;
    public float productionTime;
    
    public SkillData[] skills;
    public CraftingRecipe[] craftingRecipes;
}
