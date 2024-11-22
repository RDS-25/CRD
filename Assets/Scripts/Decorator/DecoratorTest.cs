using UnityEngine;

public class DecoratorTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Warrior warrior = new RealWarrior();
        warrior = new Sword(warrior);
        Debug.Log(warrior.Description + " " + warrior.Attack());

        warrior = new Shield(warrior);
        Debug.Log(warrior.Description + " " + warrior.Attack());

        warrior = new Sword(warrior);
        Debug.Log(warrior.Description + " " + warrior.Attack());

        warrior = new Sword(warrior);
        Debug.Log(warrior.Description + " " + warrior.Attack());

        warrior = new Shield(warrior);
        Debug.Log(warrior.Description + " " + warrior.Attack());
    }

}
