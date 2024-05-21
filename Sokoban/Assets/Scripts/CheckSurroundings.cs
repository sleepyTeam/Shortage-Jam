using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSurroundings : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
   
    public LayerMask ground;
    public LayerMask Obstacle;

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }
    void Update()
    {
        SurroundingCheck();
    }
    private void SurroundingCheck()
    {
        
        #region Left
        leftBlocked = Physics.Raycast(bc.bounds.center, Vector3.left, out leftHit, raycastLength, Obstacle | ground);
        
        Debug.DrawRay(bc.bounds.center, Vector3.left, Color.green);
        if (leftHit.transform != null)
        {
            leftGO = leftHit.transform.gameObject;
        }
        else
        {
            leftGO = null;
        }
        #endregion
        #region Right
        rightBlocked = Physics.Raycast(bc.bounds.center, Vector3.right,out rightHit, raycastLength, Obstacle | ground);
        if(rightHit.transform != null)
        {
            rightGO = rightHit.transform.gameObject;
        }
        else { rightGO=null; }
        Debug.DrawRay(bc.bounds.center, Vector3.right, Color.cyan);
        #endregion Right
        #region Fwd
        forwardBlocked = Physics.Raycast(bc.bounds.center, Vector3.forward,out forwardHit, raycastLength, Obstacle | ground);
        if(forwardHit.transform != null)
        {
            forwardGO = forwardHit.transform.gameObject;
        }
        else { forwardGO=null; }
        Debug.DrawRay(bc.bounds.center, Vector3.forward, Color.blue);
        #endregion Fwd
        #region Bckwd
        backwardBlocked = Physics.Raycast(bc.bounds.center, Vector3.back,out backwardHit, raycastLength, Obstacle | ground);
        if(backwardHit.transform != null)
        {
            backwardGO = backwardHit.transform.gameObject;
        }
        else backwardGO=null;
        Debug.DrawRay(bc.bounds.center, Vector3.back, Color.black);
        #endregion Bckwd

    }
}
