using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private float speed = 100f;
    public float rotationSpeed = 100f; // �ʴ� ȸ�� �ӵ� (����)
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        // ���콺 ��ġ�� �����ͼ� ���� ��ǥ�� ��ȯ
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 2D ���ӿ����� Z ��ǥ�� 0���� ����

        // ������Ʈ�� ���콺 ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);

        
    }
    void FixedUpdate()
    {
        // Rigidbody2D�� ����Ͽ� ���� �ӵ��� ȸ��
        rb.angularVelocity = rotationSpeed;
    }
}
