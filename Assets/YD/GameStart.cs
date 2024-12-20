using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.UI;

public class GameStart : MonoBehaviour
{
    public TextMeshProUGUI pressAnyKeyText; // "아무 키나 누르세요" 텍스트
    private bool isFadingIn = true; // 텍스트가 서서히 나타나는 중인지 여부
    private float fadeSpeed = 2f;   // 텍스트 페이드 속도
    private Color originalColor;
    public GameObject UI;

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
        UI.SetActive(false);
    }
}
