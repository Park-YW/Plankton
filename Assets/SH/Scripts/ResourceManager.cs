using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "Inventory/ItemList")]
public class ItemList : ScriptableObject
{
    public List<Item> items = new List<Item>(); // 모든 아이템 리스트
}

[System.Serializable]
public class Item
{
    public string itemName; // 아이템 이름
    public string itemType; // 아이템 유형
    public int itemQuantity = 0; // 기본 수량
    public int itemSize; // 아이템 크기 (인벤토리에서 차지하는 공간)
}


public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    public ItemList itemList; // 중앙에서 모든 아이템을 관리하는 리스트 (ScriptableObject)

    private List<Item> inven = new List<Item>(); // 플레이어 인벤토리
    private int maxInventorySpace = 60; //인벤 사이즈
    private int currentInventorySpace = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AddItem("흙", 10);
        AddItem("나무", 10);
        AddItem("돌", 10);
        AddItem("철", 10);
        AddItem("강철", 10);
        AddItem("티타늄", 10);

    }
    public bool AddItem(string itemName, int amount)
    {
        Item itemToAdd = itemList.items.Find(i => i.itemName == itemName);
        if (itemToAdd == null)
        {
            Debug.LogWarning("존재하지 않는 아이템입니다: " + itemName);
            return false;
        }

        int totalSize = itemToAdd.itemSize * amount;

        if (currentInventorySpace + totalSize <= maxInventorySpace)
        {
            Item existingItem = inven.Find(i => i.itemName == itemName);
            if (existingItem != null)
            {
                existingItem.itemQuantity += amount;
                Debug.Log("아이템 추가됨: " + existingItem.itemName + " x" + amount + " (차지하는 공간: " + totalSize + ")");
            }
            else
            {
                Item newItem = new Item
                {
                    itemName = itemToAdd.itemName,
                    itemType = itemToAdd.itemType,
                    itemQuantity = amount,
                    itemSize = itemToAdd.itemSize
                };

                inven.Add(newItem);
                Debug.Log("새 아이템 추가됨: " + newItem.itemName + " x" + newItem.itemQuantity + " (차지하는 공간: " + totalSize + ")");
            }

            currentInventorySpace += totalSize;
            return true;
        }
        else
        {
            Debug.LogWarning("인벤토리 공간이 부족합니다. 현재 공간: " + currentInventorySpace + " / " + maxInventorySpace);
            return false;
        }
    }

    public void RemoveItem(string itemName, int amount)
    {
        Item itemToRemove = inven.Find(i => i.itemName == itemName);
        if (itemToRemove != null && itemToRemove.itemQuantity >= amount)
        {
            int totalSize = itemToRemove.itemSize * amount;
            itemToRemove.itemQuantity -= amount;
            currentInventorySpace -= totalSize;
            Debug.Log("아이템 사용됨: " + itemToRemove.itemName + " x" + amount + " (공간 반환: " + totalSize + ")");

            if (itemToRemove.itemQuantity == 0)
            {
                inven.Remove(itemToRemove);
                Debug.Log("아이템 제거됨: " + itemToRemove.itemName);
            }
        }
        else
        {
            Debug.LogWarning("존재하지 않거나 수량이 부족한 아이템입니다: " + itemName);
        }
    }

    public bool HasItem(string itemName, int amount = 1)
    {
        Item item = inven.Find(i => i.itemName == itemName);
        return item != null && item.itemQuantity >= amount;
    }

    // 제작 기능 (아이템과 자원 모두 확인 후 제작)
    public bool CraftItem(string requiredItem, Dictionary<string, int> requiredResources, string craftedItem)
    {
        // 필요한 아이템이 있는지 확인
        if (!HasItem(requiredItem))
        {
            Debug.LogWarning("필요한 아이템이 없습니다: " + requiredItem);
            return false;
        }

        // 필요한 자원들이 있는지 확인
        foreach (var resource in requiredResources)
        {
            if (!HasItem(resource.Key, resource.Value))
            {
                Debug.LogWarning("자원이 부족합니다: " + resource.Key);
                return false;
            }
        }

        // 인벤토리 공간 확인
        Item craftedItemInfo = itemList.items.Find(i => i.itemName == craftedItem);
        if (craftedItemInfo == null)
        {
            Debug.LogWarning("존재하지 않는 제작 아이템입니다: " + craftedItem);
            return false;
        }

        int craftedItemSize = craftedItemInfo.itemSize;
        if (currentInventorySpace + craftedItemSize > maxInventorySpace)
        {
            Debug.LogWarning("인벤토리 공간이 부족하여 제작할 수 없습니다.");
            return false;
        }

        // 자원 소비
        foreach (var resource in requiredResources)
        {
            RemoveItem(resource.Key, resource.Value);
        }

        // 필요한 아이템 소비
        RemoveItem(requiredItem, 1);

        // 제작한 아이템 인벤토리에 추가
        AddItem(craftedItem, 1);
        Debug.Log("아이템 제작됨: " + craftedItem);

        return true;
    }

    // 자원 조회
    public int GetResourceAmount(string itemName)
    {
        Item resource = inven.Find(i => i.itemName == itemName);
        if (resource != null)
        {
            return resource.itemQuantity;
        }
        else
        {
            Debug.LogWarning("존재하지 않는 아이템입니다: " + itemName);
            return 0;
        }
    }
}

// 상자와의 상호작용 및 아이템 관리를 위해 ItemList ScriptableObject 하나만 사용하여 모든 아이템을 중앙에서 관리하도록 수정되었습니다.
// 개별 Item ScriptableObject를 제거하고, Item 클래스를 Serializable 클래스로 변경하여 ItemList에서 관리합니다.
// 플레이어의 초기 인벤토리를 설정하기 위해 흙 자원 20개를 추가하는 초기화 로직을 추가했습니다.
// 자원과 아이템을 구분하지 않고 Item 클래스를 통합하여 모든 아이템을 일관되게 관리하도록 수정했습니다.
