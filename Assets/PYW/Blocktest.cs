using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocktest : MonoBehaviour
{
    Rigidbody2D rb;
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.isKinematic = true;
        StartCoroutine(FallTimer());
    }
    IEnumerator FallTimer()
    {
        yield return new WaitForSeconds(1.5f);
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
