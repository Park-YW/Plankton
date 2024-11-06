using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static ResourceManager Instance { get; private set; }

    // �ڿ� Dictionary
    private Dictionary<string, int> resources = new Dictionary<string, int>
    {
        { "��", 100 },
        { "����", 50 },
        { "��", 30 },
        { "ö", 20 },
        { "��ö", 10 },
        { "ƼŸ��", 5 }
    };

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ ����
        }
        else
        {
            Destroy(gameObject); // �ν��Ͻ� �ߺ� ����
        }
    }

    // �ڿ� �߰�
    public void AddResource(string resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] += amount;
        }
        else
        {
            Debug.LogWarning("�������� �ʴ� �ڿ� �����Դϴ�.");
        }
    }

    // �ڿ� �Һ�
    public bool UseResource(string resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType) && resources[resourceType] >= amount)
        {
            resources[resourceType] -= amount;
            return true;
        }
        else
        {
            Debug.LogWarning("�ڿ��� �����ϰų� �������� �ʽ��ϴ�.");
            return false;
        }
    }

    // �ڿ� ��ȸ
    public int GetResourceAmount(string resourceType)
    {
        if (resources.ContainsKey(resourceType))
        {
            return resources[resourceType];
        }
        else
        {
            Debug.LogWarning("�������� �ʴ� �ڿ� �����Դϴ�.");
            return 0;
        }
    }
}