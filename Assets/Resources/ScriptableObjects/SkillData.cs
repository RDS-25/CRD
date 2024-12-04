using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public string skillName;
    public Sprite skillIcon;
    public string skillDescription;
    public abstract float ApplySkill(Unit data);
}




