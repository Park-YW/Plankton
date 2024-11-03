using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ��� �����ϱ� ���� ����
    private static GameManager _instance;
    public bool isAbleBlockSpawn;

    // �ν��Ͻ��� ������ �� �ִ� ���� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ������ ���ο� �ν��Ͻ� ����
            if (_instance == null)
            {
                GameObject singleton = new GameObject("GameManager");
                _instance = singleton.AddComponent<GameManager>();
                DontDestroyOnLoad(singleton);
            }
            return _instance;
        }
    }

    // �̱��� �ʱ�ȭ �� ������ ���� �޼���
    void Awake()
    {
        // �̹� �ν��Ͻ��� ������ �ڽ��� �ı�
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