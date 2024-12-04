using System.Collections;
using UnityEngine;
public class BlockBounce : MonoBehaviour
{
    public float bounceMultiplier = 2.0f; // 충돌 시 튀어오르는 힘의 배율
    public float destroyDelay = 2.0f; // 블록이 파괴되기 전 대기 시간

    private bool isDestroyScheduled = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 강철 블록일 때만 작동
        if (other.gameObject.layer == LayerMask.NameToLayer("BlockLayer") && other.CompareTag("Steel"))
        {
            Rigidbody2D blockRb = other.GetComponent<Rigidbody2D>();
            if (blockRb != null)
            {
                // 충돌한 속도에 따라 튀어오르는 힘 계산
                float collisionForce = blockRb.velocity.magnitude;
                Vector2 bounce = new Vector2(0, collisionForce * bounceMultiplier);

                blockRb.AddForce(bounce, ForceMode2D.Impulse);
                Debug.Log("블록이 튀어오릅니다: " + bounce);

                if (!isDestroyScheduled)
                {
                    isDestroyScheduled = true;
                    StartCoroutine(DestroyBlockAfterDelay(other.gameObject, destroyDelay));
                }
            }
        }
    }

    private IEnumerator DestroyBlockAfterDelay(GameObject block, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(block);
        ResourceManager.Instance.AddItem("강철", 5);
        isDestroyScheduled = false;
    }
}
