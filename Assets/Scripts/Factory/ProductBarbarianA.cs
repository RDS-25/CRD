using UnityEngine;

public class ProductBarbarianA : MonoBehaviour, IUnitProduct
{
    string unitName = "White Barbarian";

    public string UnitName {  
        get { return unitName; } 
        set { unitName = value; }
    }

    public AudioClip battleCry;

    public void Initialize()
    {
        gameObject.name = UnitName;
        gameObject.transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));

        // 각 프로덕트마다 초기화 할 것 있으면 여기서 하면 됩니다.
        // 소리지르거나 
        // 이펙트 나오거나
        GetComponent<AudioSource>().PlayOneShot(battleCry);

    }
}
