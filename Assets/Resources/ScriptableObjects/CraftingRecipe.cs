using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/CraftingRecipe", order = int.MaxValue)]
public class CraftingRecipe : ScriptableObject
{
    [Header("제작에 필요한 재료 아이템들")] [SerializeField]
    public CraftingItemInfo[] reqItems;

    [Header("제작 결과물 아이템")] [SerializeField]
    public GameObject resultItem;

    [SerializeField] public Sprite createSprite;

    [Header("제작에 필요한 아이템 설명")] [SerializeField]
    public string description;


    public void CombineItem()
    {
        // 필요한 아이템들이 충분히 있는지 확인
        bool canCraft = true;
        foreach (var reqItem in reqItems)
        {
            int requiredCount = reqItems.Count(item => item.item.ID == reqItem.item.ID);
            int sceneCount = CountItemsInScene(reqItem.item);

            if (sceneCount < requiredCount)
            {
                // Debug.Log(
                //     $"Required item {reqItem.item.name} is missing in the scene. Needed: {requiredCount}, Found: {sceneCount}");
                canCraft = false;
                break;
            }
        }

        // 모든 필요한 아이템이 충분히 있으면 조합
        if (canCraft)
        {
            // Debug.Log($"Successfully crafted {resultItem.name}!");
            // 결과물을 처리하는 로직을 여기에 구현
            // 필요한 아이템 제거
            foreach (var reqItem in reqItems)
            {
                RemoveItemsFromScene(reqItem.item, reqItems.Count(item => item.item.ID == reqItem.item.ID));
            }

            // 결과 아이템 생성
            var a = ObjectPool.Instance.GetObjectUnit(resultItem);
            a.transform.position = Vector3.zero;
        }
        else
        {
            // Debug.Log("Crafting failed. Required items are missing in the scene.");
        }
    }

    private int CountItemsInScene(UnitData item)
    {
        // 씬에서 특정 ID를 가진 아이템의 수를 세는 로직
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        return units.Count(obj => obj.GetComponent<Unit>()?.unitData.ID == item.ID && obj.tag != "NotSearch");
    }

    private void RemoveItemsFromScene(UnitData item, int count)
    {
        // 씬에서 특정 ID를 가진 아이템을 제거하되, 특정 태그를 가진 유닛은 제외
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        int removed = 0;

        foreach (var obj in units)
        {
            if (removed >= count) break;

            if (obj.GetComponent<Unit>()?.unitData.ID == item.ID && obj.tag != "NotSearch")
            {
                ObjectPool.Instance.ReturnObject(obj.gameObject);
                removed++;
            }
        }
    }
}

[System.Serializable]
public struct CraftingItemInfo
{
    [SerializeField] public UnitData item;
}