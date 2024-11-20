using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "Inventory/ItemList")]
public class ItemList : ScriptableObject
{
    public List<Item> items = new List<Item>(); // ��� ������ ����Ʈ
}

[System.Serializable]
public class Item
{
    public string itemName; // ������ �̸�
    public string itemType; // ������ ����
    public int itemQuantity = 0; // �⺻ ����
    public int itemSize; // ������ ũ�� (�κ��丮���� �����ϴ� ����)
}


public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    public ItemList itemList; // �߾ӿ��� ��� �������� �����ϴ� ����Ʈ (ScriptableObject)

    private List<Item> inven = new List<Item>(); // �÷��̾� �κ��丮
    private int maxInventorySpace = 60; //�κ� ������
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
        AddItem("��", 10);
        AddItem("����", 10);
        AddItem("��", 10);
        AddItem("ö", 10);
        AddItem("��ö", 10);
        AddItem("ƼŸ��", 10);

    }
    public bool AddItem(string itemName, int amount)
    {
        Item itemToAdd = itemList.items.Find(i => i.itemName == itemName);
        if (itemToAdd == null)
        {
            Debug.LogWarning("�������� �ʴ� �������Դϴ�: " + itemName);
            return false;
        }

        int totalSize = itemToAdd.itemSize * amount;

        if (currentInventorySpace + totalSize <= maxInventorySpace)
        {
            Item existingItem = inven.Find(i => i.itemName == itemName);
            if (existingItem != null)
            {
                existingItem.itemQuantity += amount;
                Debug.Log("������ �߰���: " + existingItem.itemName + " x" + amount + " (�����ϴ� ����: " + totalSize + ")");
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
                Debug.Log("�� ������ �߰���: " + newItem.itemName + " x" + newItem.itemQuantity + " (�����ϴ� ����: " + totalSize + ")");
            }

            currentInventorySpace += totalSize;
            return true;
        }
        else
        {
            Debug.LogWarning("�κ��丮 ������ �����մϴ�. ���� ����: " + currentInventorySpace + " / " + maxInventorySpace);
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
            Debug.Log("������ ����: " + itemToRemove.itemName + " x" + amount + " (���� ��ȯ: " + totalSize + ")");

            if (itemToRemove.itemQuantity == 0)
            {
                inven.Remove(itemToRemove);
                Debug.Log("������ ���ŵ�: " + itemToRemove.itemName);
            }
        }
        else
        {
            Debug.LogWarning("�������� �ʰų� ������ ������ �������Դϴ�: " + itemName);
        }
    }

    public bool HasItem(string itemName, int amount = 1)
    {
        Item item = inven.Find(i => i.itemName == itemName);
        return item != null && item.itemQuantity >= amount;
    }

    // ���� ��� (�����۰� �ڿ� ��� Ȯ�� �� ����)
    public bool CraftItem(string requiredItem, Dictionary<string, int> requiredResources, string craftedItem)
    {
        // �ʿ��� �������� �ִ��� Ȯ��
        if (!HasItem(requiredItem))
        {
            Debug.LogWarning("�ʿ��� �������� �����ϴ�: " + requiredItem);
            return false;
        }

        // �ʿ��� �ڿ����� �ִ��� Ȯ��
        foreach (var resource in requiredResources)
        {
            if (!HasItem(resource.Key, resource.Value))
            {
                Debug.LogWarning("�ڿ��� �����մϴ�: " + resource.Key);
                return false;
            }
        }

        // �κ��丮 ���� Ȯ��
        Item craftedItemInfo = itemList.items.Find(i => i.itemName == craftedItem);
        if (craftedItemInfo == null)
        {
            Debug.LogWarning("�������� �ʴ� ���� �������Դϴ�: " + craftedItem);
            return false;
        }

        int craftedItemSize = craftedItemInfo.itemSize;
        if (currentInventorySpace + craftedItemSize > maxInventorySpace)
        {
            Debug.LogWarning("�κ��丮 ������ �����Ͽ� ������ �� �����ϴ�.");
            return false;
        }

        // �ڿ� �Һ�
        foreach (var resource in requiredResources)
        {
            RemoveItem(resource.Key, resource.Value);
        }

        // �ʿ��� ������ �Һ�
        RemoveItem(requiredItem, 1);

        // ������ ������ �κ��丮�� �߰�
        AddItem(craftedItem, 1);
        Debug.Log("������ ���۵�: " + craftedItem);

        return true;
    }

    // �ڿ� ��ȸ
    public int GetResourceAmount(string itemName)
    {
        Item resource = inven.Find(i => i.itemName == itemName);
        if (resource != null)
        {
            return resource.itemQuantity;
        }
        else
        {
            Debug.LogWarning("�������� �ʴ� �������Դϴ�: " + itemName);
            return 0;
        }
    }
}

// ���ڿ��� ��ȣ�ۿ� �� ������ ������ ���� ItemList ScriptableObject �ϳ��� ����Ͽ� ��� �������� �߾ӿ��� �����ϵ��� �����Ǿ����ϴ�.
// ���� Item ScriptableObject�� �����ϰ�, Item Ŭ������ Serializable Ŭ������ �����Ͽ� ItemList���� �����մϴ�.
// �÷��̾��� �ʱ� �κ��丮�� �����ϱ� ���� �� �ڿ� 20���� �߰��ϴ� �ʱ�ȭ ������ �߰��߽��ϴ�.
// �ڿ��� �������� �������� �ʰ� Item Ŭ������ �����Ͽ� ��� �������� �ϰ��ǰ� �����ϵ��� �����߽��ϴ�.
