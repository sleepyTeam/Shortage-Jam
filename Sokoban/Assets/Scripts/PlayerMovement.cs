using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int travelDistance = 1;
    private BoxCollider collider;
    private Rigidbody rb;
    public Vector3 playerPos;
    public Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        playerPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();
        HorizontalMovement();
    }

    void VerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            prevPos = transform.position;
            playerPos.x = playerPos.x -= travelDistance;
            transform.position = playerPos;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            prevPos = transform.position;
            playerPos.x += travelDistance; transform.position = playerPos; }
    }
    void HorizontalMovement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            prevPos = transform.position;
            playerPos.z += travelDistance;
            transform.position = playerPos;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            prevPos = transform.position;
            playerPos.z -= travelDistance;
            transform.position = playerPos;
        }
    }
    void Push(GameObject collision)
    {
        Vector3 collisionPos = collision.transform.position;
        if (prevPos.x > transform.position.x)
        {
            collisionPos.x -= travelDistance;
            collision.transform.position = collisionPos;
        }
        if (prevPos.x < transform.position.x)
        {
            collisionPos.x += travelDistance;
            collision.transform.position = collisionPos;
        }
        if (prevPos.z > transform.position.z)
        {
            collisionPos.z -= travelDistance;
            collision.transform.position = collisionPos;
        }
        if (prevPos.z < transform.position.z)
        {
            collisionPos.z += travelDistance;
            collision.transform.position = collisionPos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Push " + other.name);
        Push(other.gameObject);
    }
}
