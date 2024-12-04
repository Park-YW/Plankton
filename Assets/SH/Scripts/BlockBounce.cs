using System.Collections;
using UnityEngine;
public class BlockBounce : MonoBehaviour
{
    public float bounceMultiplier = 2.0f; // �浹 �� Ƣ������� ���� ����
    public float destroyDelay = 2.0f; // ����� �ı��Ǳ� �� ��� �ð�

    private bool isDestroyScheduled = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��ö ����� ���� �۵�
        if (other.gameObject.layer == LayerMask.NameToLayer("BlockLayer") && other.CompareTag("Steel"))
        {
            Rigidbody2D blockRb = other.GetComponent<Rigidbody2D>();
            if (blockRb != null)
            {
                // �浹�� �ӵ��� ���� Ƣ������� �� ���
                float collisionForce = blockRb.velocity.magnitude;
                Vector2 bounce = new Vector2(0, collisionForce * bounceMultiplier);

                blockRb.AddForce(bounce, ForceMode2D.Impulse);
                Debug.Log("����� Ƣ������ϴ�: " + bounce);

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
        ResourceManager.Instance.AddItem("��ö", 5);
        isDestroyScheduled = false;
    }
}
