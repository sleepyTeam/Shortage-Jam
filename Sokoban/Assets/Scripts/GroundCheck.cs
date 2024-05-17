using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundCheck : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
    public bool grounded;
    public LayerMask ground;
    public float rayOffset;

    private float raycastLength;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();

        raycastLength =bc.bounds.center.y  - rayOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    private void Check()
    {
        bool hit = Physics.Raycast(bc.bounds.center, Vector3.down, raycastLength, ground);
        Debug.DrawRay(bc.bounds.center, Vector3.down, Color.red);
        if (!hit)
        {
            grounded = false;
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
            grounded = true;
        }
    }
}
