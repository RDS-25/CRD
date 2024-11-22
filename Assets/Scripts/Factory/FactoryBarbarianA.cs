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

        // Unity�� ObjectPool�� �̸� ������ ���صд�. ��Ȱ���� ��������
        // ���� �������� �̸� �صΰ� �ʹٸ�???
        // �̷��� �ؾ��Ѵ�. ������ �̸� �صΰ� ���� �ʴٸ� �Ƚᵵ �Ǵ� �ڵ�. 

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
