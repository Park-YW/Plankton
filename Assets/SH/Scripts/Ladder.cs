using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "ladder")
        {
            Debug.Log("��ٸ� �ν�");
            Debug.Log(Input.GetAxis("Vertical"));
            if (Mathf.Abs(Input.GetAxis("Vertical")) >= 0f)
            {
                GameManager.Instance.isPlayerLadder = true;
                Debug.Log("��ٸ� ��ȣ�ۿ�");
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "ladder")
        {
            GameManager.Instance.isPlayerLadder = false;
            Debug.Log("��ٸ� ���");
        }
    }
}
