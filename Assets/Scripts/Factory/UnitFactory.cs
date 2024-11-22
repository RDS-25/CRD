using UnityEngine;

public abstract class UnitFactory : MonoBehaviour
{
    public abstract IUnitProduct GetProduct(Vector3 position);
    public abstract void ReturnProduct(IUnitProduct product);
}
