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

        // �� ���δ�Ʈ���� �ʱ�ȭ �� �� ������ ���⼭ �ϸ� �˴ϴ�.
        // �Ҹ������ų� 
        // ����Ʈ �����ų�
        GetComponent<AudioSource>().PlayOneShot(battleCry);

    }
}
