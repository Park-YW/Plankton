using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSource : MonoBehaviour
{
    public static ImageSource Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
