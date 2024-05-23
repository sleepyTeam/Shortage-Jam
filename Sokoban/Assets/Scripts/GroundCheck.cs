using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public GameObject groundCheckHolder;
    public BoxCollider groundCheckCollider;
    public LayerMask ground;
    public bool isFalling;
    public Rigidbody rb;
    public bool grounded;
    public float raycastLength = 0.15f;

    private void Start()
    {

    }
    private void Update()
    {
        GCheck();
    }

    private void OnCollisionEnter(Collision other)
    {
        grounded = true;
        rb.isKinematic = true;
    }
    private void GCheck()
    {
        bool hit = Physics.Raycast(groundCheckCollider.bounds.center, Vector3.down, raycastLength);
        Debug.DrawRay(groundCheckCollider.bounds.center, Vector3.down, Color.red);
        if (!hit)
        {
            isFalling = true;
            grounded = false;
            rb.isKinematic = false;
        }
        else isFalling = false;
    }
}
