using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장하기 위한 변수
    private static GameManager _instance;
    public bool isAbleBlockSpawn;

    // 인스턴스에 접근할 수 있는 정적 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없으면 새로운 인스턴스 생성
            if (_instance == null)
            {
                GameObject singleton = new GameObject("GameManager");
                _instance = singleton.AddComponent<GameManager>();
                DontDestroyOnLoad(singleton);
            }
            return _instance;
        }
    }

    // 싱글톤 초기화 및 설정을 위한 메서드
    void Awake()
    {
        // 이미 인스턴스가 있으면 자신을 파괴
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}