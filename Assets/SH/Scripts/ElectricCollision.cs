using UnityEngine;

public class ElectricCollision : MonoBehaviour
{
    public GameObject[] Door;

    public static int power = 0; // 모든 오브젝트가 공유하는 정적 변수
    private void Update()
    {
        if (power == 1) { Door[0].SetActive(false); }
        if (power == 3) { Door[1].SetActive(false); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Electric 프리팹 감지
        if (collision.gameObject.name.Contains("Electric"))
        {
            power++; // power 변수 증가
            Debug.Log("Power increased: " + power);
        }
    }
}
