using System;
using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    public GameObject[] blockPrefabs; // ������ ��� �����յ� 0�� ��, 1�� ����, 2�� ��, 3�� ö, 4�� ��ö, 5�� ƼŸ��
    public Camera mainCamera;      // �� ī�޶�
    public LayerMask backgroundLayer; // ��� ���̾� ����ũ
    public bool ableToSpawn;
    private string[] whatBlock = { "��","����","��","ö","��ö","ƼŸ��"}; //0�� ��, 1�� ����, 2�� ��, 3�� ö, 4�� ��ö, 5�� ƼŸ��
    private int index = 0;

    private void Start()
    {
        index = 0;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ SpawnBlockIfBackgroundOnly(whatBlock[index]); } //���콺 ��Ŭ�� �� ��� ����
        ChangeBlock(); //��ũ���� ���ؼ� ��� �ٲٱ�
    }

    void ChangeBlock()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // ��ũ�� ���� ��� index ����
        if (scroll > 0f)
        {
            index++;
            if (index >= whatBlock.Length) index = 0; // ������ �ε������� ó������ ��ȯ
        }
        // ��ũ�� �ٿ��� ��� index ����
        else if (scroll < 0f)
        {
            index--;
            if (index < 0) index = whatBlock.Length - 1; // ù �ε������� ���������� ��ȯ
        }

        Debug.Log("���� ���: " + whatBlock[index]);
    }

    void SpawnBlockIfBackgroundOnly(string block)
    {
        // ���콺 ��ġ�� �������� Ray ���� (2D ȯ��)
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        bool backgroundHit = false;
        bool otherObjectHit = false;

        // Raycast2D�� �浹�� ��� ������Ʈ �˻�
        foreach (RaycastHit2D hit in hits)
        {
            if (((1 << hit.collider.gameObject.layer) & backgroundLayer) != 0)
            {
                backgroundHit = true;
            }
            else
            {
                otherObjectHit = true;
            }
        }

        // ��游 �浹���� ��쿡�� ��� ����
        if (backgroundHit && !otherObjectHit && ableToSpawn)
        {
            if (ResourceManager.Instance.UseResource(block, 5)) //�ڿ� 5���� ����Ͽ� ��� �ϳ��� ���� �� ����
            {
                Instantiate(blockPrefabs[index], mousePosition, Quaternion.identity);
                Debug.Log("��� ���� ����!");
            }
        }
        else
        {
            Debug.Log("��� �̿��� ������Ʈ�� �����Ͽ� ��� �������� ����.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 ) //BackGroundLayer�� 3��
        {
            ableToSpawn = true;
        }
        else
        {
            ableToSpawn = false;
        }
    }
}