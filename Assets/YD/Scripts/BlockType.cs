using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewBlockType", menuName = "Create/BlockType")]
public class BlockType : ScriptableObject
{
    public string blockName;
    public float weight;
    public float friction;
    public GameObject blockPrefab;
}

public class BlockManager : MonoBehaviour
{
    public List<BlockType> blockTypes = new List<BlockType>();

    public void CreateNewBlock(Vector3 position, BlockType blockType)
    {
        if (blockType != null && blockType.blockPrefab != null)
        {
            GameObject newBlock = Instantiate(blockType.blockPrefab, position, Quaternion.identity);
            Rigidbody2D rb = newBlock.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.mass = blockType.weight;
                rb.sharedMaterial = new PhysicsMaterial2D { friction = blockType.friction };
            }
        }
    }
}
