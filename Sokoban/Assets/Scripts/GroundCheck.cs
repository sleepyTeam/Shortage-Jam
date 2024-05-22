using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public GameObject groundCheckHolder;
    public BoxCollider groundCheckCollider;
    public LayerMask ground;
    public Rigidbody rb;
    public bool grounded;
    public float raycastLength = 0.15f;

    public bool leftBlocked;
    public bool rightBlocked;
    public bool forwardBlocked;
    public bool backwardBlocked;

    public RaycastHit leftHit;
    public RaycastHit rightHit;
    public RaycastHit forwardHit;
    public RaycastHit backwardHit;

    public GameObject leftGO;
    public GameObject rightGO;
    public GameObject forwardGO;
    public GameObject backwardGO;

    public float raycastLength;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            grounded = false;
            rb.isKinematic = false;
        }
    }
}
