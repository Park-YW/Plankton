using UnityEngine;

public class Save : MonoBehaviour
{
    private Vector3 savedPosition; // 플레이어 위치를 저장할 변수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player") // 태그로 플레이어 확인
        {
            // 플레이어의 현재 위치를 저장
            savedPosition = collision.transform.position;
            Debug.Log("세이브 위치 저장: " + savedPosition);

            // 플레이어의 PlayerMovement 컴포넌트를 가져와 세이브 위치 전달
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.SetSavePoint(savedPosition); // 저장된 위치 전달
            }
            else
            {
                Debug.LogWarning("PlayerMovement 컴포넌트를 찾을 수 없습니다.");
            }
        }
    }
}