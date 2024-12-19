using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private float speed = 100f;
    public float rotationSpeed = 100f; // 초당 회전 속도 (각도)
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        // 마우스 위치를 가져와서 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 2D 게임에서는 Z 좌표를 0으로 설정

        // 오브젝트를 마우스 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);

        
    }
    void FixedUpdate()
    {
        // Rigidbody2D를 사용하여 일정 속도로 회전
        rb.angularVelocity = rotationSpeed;
    }
}
