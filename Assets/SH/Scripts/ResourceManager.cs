using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static ResourceManager Instance { get; private set; }

    // 자원 Dictionary
    private Dictionary<string, int> resources = new Dictionary<string, int>
    {
        { "흙", 100 },
        { "나무", 50 },
        { "돌", 30 },
        { "철", 20 },
        { "강철", 10 },
        { "티타늄", 5 }
    };

    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 유지
        }
        else
        {
            Destroy(gameObject); // 인스턴스 중복 방지
        }
    }

    // 자원 추가
    public void AddResource(string resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] += amount;
        }
        else
        {
            Debug.LogWarning("존재하지 않는 자원 유형입니다.");
        }
    }

    // 자원 소비
    public bool UseResource(string resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType) && resources[resourceType] >= amount)
        {
            resources[resourceType] -= amount;
            return true;
        }
        else
        {
            Debug.LogWarning("자원이 부족하거나 존재하지 않습니다.");
            return false;
        }
    }

    // 자원 조회
    public int GetResourceAmount(string resourceType)
    {
        if (resources.ContainsKey(resourceType))
        {
            return resources[resourceType];
        }
        else
        {
            Debug.LogWarning("존재하지 않는 자원 유형입니다.");
            return 0;
        }
    }
}