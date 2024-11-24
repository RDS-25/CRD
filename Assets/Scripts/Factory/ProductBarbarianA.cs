using UnityEngine;

public class ProductBarbarianA : MonoBehaviour, IUnitProduct
{
    string unitName = "White Barbarian";

    public string UnitName {  
        get { return unitName; } 
        set { unitName = value; }
    }

   

    public void Initialize()
    {
        gameObject.name = UnitName;
        gameObject.transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));

      
    }
}
