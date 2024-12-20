using UnityEngine;

public class ElectricCollision : MonoBehaviour
{
    public GameObject[] Door;

    public static int power = 0; // ��� ������Ʈ�� �����ϴ� ���� ����
    private void Update()
    {
        if (power == 1) { Door[0].SetActive(false); }
        if (power == 3) { Door[1].SetActive(false); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Electric ������ ����
        if (collision.gameObject.name.Contains("Electric"))
        {
            power++; // power ���� ����
            Debug.Log("Power increased: " + power);
        }
    }
}
