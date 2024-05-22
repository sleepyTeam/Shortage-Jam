using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPosition : MonoBehaviour
{
    public Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

}
