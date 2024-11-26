using UnityEngine;

public class Laser : MonoBehaviour
{
    public LayerMask blockLayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & blockLayer) != 0)
        {
            Debug.Log("�������� ��� ����: " + collision.gameObject.name);

            Destroy(collision.gameObject);
            Debug.Log("�������� ����� ����: " + collision.gameObject.name);
        }
    }
}
