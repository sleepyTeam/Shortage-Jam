using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCatch : MonoBehaviour
{
    private BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

}
