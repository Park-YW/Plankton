using UnityEngine;

public class Laser : MonoBehaviour
{
    public LayerMask blockLayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & blockLayer) != 0)
        {
            Debug.Log("레이저에 블록 접촉: " + collision.gameObject.name);

            Destroy(collision.gameObject);
            Debug.Log("레이저가 블록을 삭제: " + collision.gameObject.name);
        }
    }
}
