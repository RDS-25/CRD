using UnityEngine;

[CreateAssetMenu(fileName = "SkillCritical", menuName = "Skills/SkillCritical")]
public class SkillCritical : SkillData
{
    public float criticalMultiplier;
    public int criticalper;
    

    public override float ApplySkill(Unit data)
    {
      int chance = UnityEngine.Random.Range(1, 101);
      if (chance <= criticalper)
      {
          return criticalMultiplier * data.attackdamage;
      }

      return data.attackdamage;
    }
}


