using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCheck : MonoBehaviour
{
    public LayerMask obstacle;

    public GameObject blockAbove;
    public GameObject blockBelow;
    public float raycastLength;
    public bool _connectedAbove;
    public bool _connectedBelow;
    private RaycastHit aboveHit;
    private RaycastHit belowHit;
    private Rigidbody rb;
    private BoxCollider bc;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        AboveCheck();
        BelowCheck();
        if(_connectedAbove)
        {
            Stack();
        }
        if(_connectedBelow)
        {

        }
    }
    private void AboveCheck()
    {
        _connectedAbove = Physics.Raycast(bc.bounds.center, Vector3.up, out aboveHit, raycastLength,obstacle);
    }
    private void BelowCheck()
    {
        _connectedBelow = Physics.Raycast(bc.bounds.center, Vector3.down, out belowHit, raycastLength, obstacle) ;
    }
    private void Stack()
    {
        blockAbove = aboveHit.transform.gameObject;
    }
}
