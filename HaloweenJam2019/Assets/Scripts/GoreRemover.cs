using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoreRemover : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 5);
    }
}
