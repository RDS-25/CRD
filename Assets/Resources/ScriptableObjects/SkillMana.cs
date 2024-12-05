using UnityEngine;

[CreateAssetMenu(fileName = "SkillMana", menuName = "Skills/SkillMana")]
public class SkillMana : SkillData
{
    public float range; // 범위 공격의 범위 지정
    [SerializeField] private LayerMask enemyLayer;
    public GameObject particlePrefab;
    public AudioClip skillSound;

    public override float ApplySkill(Unit data)
    {
        // data.unitData.magicPoint == 필요 마나통
        // data.mp == 현재 마나통
        if (data.unitData.magicPoint <= data.mp)
        {
            data.audioSource.PlayOneShot(skillSound);
            if (data.target==null)
            {
                // Debug.Log("지정할 대상이 없음");
                return 0;
            }
            data.mp -= data.unitData.magicPoint;
            // 타겟의 위치를 기준으로 범위 내의 모든 대상 찾기
            Collider[] hitColliders = Physics.OverlapSphere(data.target.position, range,enemyLayer);
            foreach (var hitCollider in hitColliders)
            {
                PropertyDisplay targetUnit = hitCollider.GetComponent<PropertyDisplay>();
                targetUnit.currenthp -= data.unitData.attackDamage * 3;
                GameObject particle = Instantiate(particlePrefab, data.target.position, Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
                
                return 0;
            }
            
        }
        return 0;
    }
}
