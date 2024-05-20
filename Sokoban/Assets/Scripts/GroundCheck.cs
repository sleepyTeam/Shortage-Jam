using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundCheck : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
    public bool grounded;
    public LayerMask ground;
    public LayerMask Obstacle;
    public float rayOffset;

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
        bc = GetComponent<BoxCollider>();

        raycastLength =bc.bounds.center.y  + rayOffset;
    }

    // Update is called once per frame
    void Update()
    {
        GCheck();
        BCheck();
    }
    private void GCheck()
    {
        bool hit = Physics.Raycast(bc.bounds.center, Vector3.down, raycastLength);
        Debug.DrawRay(bc.bounds.center, Vector3.down, Color.red);
        if (!hit)
        {
            grounded = false;
            rb.isKinematic = false;
        }
        else
        {
            grounded= true;
            rb.isKinematic = true;
        }
    }
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.layer == ground)
    //    {
    //        grounded = true;
    //        rb.isKinematic = true;
    //    }
    //}
    private void BCheck()
    {
        
        leftBlocked = Physics.Raycast(bc.bounds.center, Vector3.left, out leftHit, raycastLength, Obstacle);
        
        Debug.DrawRay(bc.bounds.center, Vector3.left, Color.green);
        if (leftHit.transform != null)
        {
            leftGO = leftHit.transform.gameObject;
        }
        else
        {
            leftGO=null;
        }
        rightBlocked = Physics.Raycast(bc.bounds.center, Vector3.right,out rightHit, raycastLength, Obstacle);
        if(rightHit.transform != null)
        {
            rightGO = rightHit.transform.gameObject;
        }
        else { rightGO=null; }

        Debug.DrawRay(bc.bounds.center, Vector3.right, Color.cyan);
        forwardBlocked = Physics.Raycast(bc.bounds.center, Vector3.forward,out forwardHit, raycastLength, Obstacle);
        if(forwardHit.transform != null)
        {
            forwardGO = forwardHit.transform.gameObject;
        }
        else { forwardGO=null; }
        Debug.DrawRay(bc.bounds.center, Vector3.forward, Color.blue);
        backwardBlocked = Physics.Raycast(bc.bounds.center, Vector3.back,out backwardHit, raycastLength, Obstacle);
        if(backwardHit.transform != null)
        {
            backwardGO = backwardHit.transform.gameObject;
        }
        else backwardGO=null;
        Debug.DrawRay(bc.bounds.center, Vector3.back, Color.black);


    }
}
