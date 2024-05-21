using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float travelDistance = 1f;
    private BoxCollider bc;
    private Rigidbody rb;
    public Vector3 playerPos;
    public Vector3 prevPos;
    public Vector3 playerOffset;
    public LevelManager lM;
    private Vector3 spawnPos;
    public CheckSurroundings playerCheck;
    public CheckSurroundings blockCS;

    public enum MoveVector
    {
        Left, Right, Forward, Backward
    }
    public MoveVector direction;

    // Start is called before the first frame update
    void Start()
    {
       
        lM = GameObject.FindGameObjectWithTag("LevelMan").GetComponent<LevelManager>();
        bc = GetComponent<BoxCollider>();
        playerPos = transform.position;
        rb = GetComponent<Rigidbody>();
        spawnPos = lM.spawnPoint.transform.position + playerOffset;
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();
        HorizontalMovement();
        Reset();
    }

    void VerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (playerCheck.leftGO != null)
            {
                blockCS = playerCheck.leftGO.GetComponent<CheckSurroundings>();
                if (blockCS.leftBlocked)
                {
                    Debug.Log("Path Blocked");
                }
                else
                {
                    Debug.Log("Up");
                    prevPos = transform.position;
                    playerPos.x -= travelDistance;
                    transform.position = playerPos;
                    direction = MoveVector.Left;
                }

            }
            else
            {
                Debug.Log("Up");
                prevPos = transform.position;
                playerPos.x -= travelDistance;
                transform.position = playerPos;
                direction = MoveVector.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (playerCheck.rightGO != null)
            {
                blockCS = playerCheck.rightGO.GetComponent<CheckSurroundings>();
                if (blockCS.rightBlocked)
                {
                    Debug.Log("Path Blocked");
                }
                else
                {
                    Debug.Log("Down");
                    prevPos = transform.position;
                    playerPos.x += travelDistance;
                    transform.position = playerPos;
                    direction = MoveVector.Right;
                }

            }
            else
            {
                Debug.Log("Down");
                prevPos = transform.position;
                playerPos.x += travelDistance;
                transform.position = playerPos;
                direction = MoveVector.Right;
            }
        }
    }
    void HorizontalMovement()
    {
        if (Input.GetKeyDown(KeyCode.D))
            if (playerCheck.forwardGO != null)
            {
                blockCS = playerCheck.forwardGO.GetComponent<CheckSurroundings>();
                if (blockCS.forwardBlocked)
                {
                    Debug.Log("Path Blocked");
                }
                else
                {
                    Debug.Log("Forward");
                    prevPos = transform.position;
                    playerPos.z += travelDistance;
                    transform.position = playerPos;
                    direction = MoveVector.Forward;
                }

            }
            else
            {
                Debug.Log("Forward");
                prevPos = transform.position;
                playerPos.z += travelDistance;
                transform.position = playerPos;
                direction = MoveVector.Forward;
            }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (playerCheck.backwardGO != null)
            {
                blockCS = playerCheck.backwardGO.GetComponent<CheckSurroundings>();
                if (blockCS.backwardBlocked)
                {
                    Debug.Log("Path Blocked");
                }
                else
                {
                    Debug.Log("Backward");
                    prevPos = transform.position;
                    playerPos.z -= travelDistance;
                    transform.position = playerPos;
                    direction = MoveVector.Backward;
                }
            }
            else
            {
                prevPos = transform.position;
                playerPos.z -= travelDistance;
                transform.position = playerPos;
                direction = MoveVector.Backward;
            }
        }
    }
    void Push(GameObject collision)
    {
        Vector3 collisionPos = collision.transform.position;

        if (direction == MoveVector.Left && blockCS.leftBlocked == false)
        {
            collisionPos.x -= travelDistance/2;
            collision.transform.position = collisionPos;
        }
        else if (direction == MoveVector.Right && blockCS.rightBlocked == false)
        {
            collisionPos.x += travelDistance / 2;
            collision.transform.position = collisionPos;
        }
        else if (direction == MoveVector.Forward && blockCS.forwardBlocked == false)
        {
            collisionPos.z += travelDistance / 2;
            collision.transform.position = collisionPos;
        }
        else if (direction == MoveVector.Backward && blockCS.backwardBlocked ==false)
        {
            collisionPos.z -= travelDistance / 2;
            collision.transform.position = collisionPos;
        }
    }

    private void Reset()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("reset Position");
            playerPos = spawnPos;
            transform.position = playerPos;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Push " + other.gameObject.name);
        Push(other.gameObject);
    }
}
