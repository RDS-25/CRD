using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FactoryBarbarianA : UnitFactory
{
    public GameObject productPrefab;

    //public override IUnitProduct GetProduct(Vector3 position)
    //{
    //    GameObject go = Instantiate(productPrefab, position, Quaternion.identity);
    //    ProductBarbarianA newProduct = go.GetComponent<ProductBarbarianA>();
    //    newProduct.Initialize();

    //    return newProduct;
    //}

    ObjectPool<ProductBarbarianA> pool;

    private void Awake()
    {
        pool = new ObjectPool<ProductBarbarianA>(
            createFunc:CreateNewProduct,
            actionOnGet: product => product.gameObject.SetActive(true),
            actionOnRelease: product => product.gameObject.SetActive(false),
            actionOnDestroy: product => Destroy(product.gameObject),
            collectionCheck: false,
            defaultCapacity: 10
            );

        // Unity의 ObjectPool은 미리 생성은 안해둔다. 재활용은 해주지만
        // 만약 생성까지 미리 해두고 싶다면???
        // 이렇게 해야한다. 생성을 미리 해두고 싶지 않다면 안써도 되는 코드. 

        List<ProductBarbarianA> tempList = new List<ProductBarbarianA>();
        for (int i=0;i<10;i++)
        {
            tempList.Add(pool.Get());
        }
        for (int i=0;i<10;i++)
        {
            pool.Release(tempList[i]);
        }
    }

    public override IUnitProduct GetProduct(Vector3 position)
    {
        ProductBarbarianA product = pool.Get();
        product.transform.position = position;
        product.Initialize();
        return product;
    }

    public override void ReturnProduct(IUnitProduct product)
    {
        if (product is ProductBarbarianA babarianA)
        {
            pool.Release(babarianA);
        }
    }

    //void ActionOnGet(ProductBarbarianA product)
    //{
    //    product.gameObject.SetActive(true);
    //}

    ProductBarbarianA CreateNewProduct()
    {
        GameObject go = Instantiate( productPrefab );
        return go.GetComponent<ProductBarbarianA>();
    }
}
