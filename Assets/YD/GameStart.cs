using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStart : MonoBehaviour
{
    public TextMeshProUGUI pressAnyKeyText; // "아무 키나 누르세요" 텍스트
    private bool isFadingIn = true; // 텍스트가 서서히 나타나는 중인지 여부
    private float fadeSpeed = 2f;   // 텍스트 페이드 속도
    private Color originalColor;

    void Start()
    {
        // 초기 설정
        if (pressAnyKeyText != null)
        {
            originalColor = pressAnyKeyText.color;
        }
    }

    void Update()
    {
        HandleTextFade(); // 텍스트 깜박임 처리

        // 아무 키 입력 시
        if (Input.anyKeyDown)
        {
            LoadGameScene();
        }
    }

    void HandleTextFade()
    {
        if (pressAnyKeyText == null) return;

        Color color = pressAnyKeyText.color;

        if (isFadingIn)
        {
            color.a += Time.deltaTime * fadeSpeed;
            if (color.a >= 1f)
            {
                color.a = 1f;
                isFadingIn = false;
            }
        }
        else
        {
            color.a -= Time.deltaTime * fadeSpeed;
            if (color.a <= 0f)
            {
                color.a = 0f;
                isFadingIn = true;
            }
        }

        pressAnyKeyText.color = color;
    }

    void LoadGameScene()
    {
        // 인게임 씬으로 전환 (씬 이름을 "GameScene"으로 가정)
        SceneManager.LoadScene("Main");
    }
}
