using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float travelDistance = 1f;
    float _cooldown = 0.2f;
    private BoxCollider bc;
    private Rigidbody rb;
    public Vector3 playerPos;
    public Vector3 prevPos;
    public Vector3 playerOffset;
    public LevelManager lM;
    private Vector3 spawnPos;
    public LayerMask ground;
    public CheckSurroundings playerCheck;
    public CheckSurroundings blockCS;
    private AudioSource aS;
    public AudioClip blockedSound;
    public AudioClip pushSound;
    public bool _canPush;
    public enum MoveVector
    {
        Left, Right, Forward, Backward
    }
    public MoveVector direction;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        _canPush = canPush();

    }

    void VerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (playerCheck.leftGO != null)
            {
                blockCS = playerCheck.leftGO.GetComponent<CheckSurroundings>();
                if (blockCS != null)
                {
                    if (blockCS.leftBlocked)
                    {
                        aS.clip = blockedSound;
                        aS.Play();
                        //Play Blocked Sound;
                    }
                    else
                    {

                        prevPos = transform.position;
                        playerPos.x -= travelDistance;
                        playerPos.y = transform.position.y;
                        transform.position = playerPos;
                        direction = MoveVector.Left;
                    }
                }
                else if (playerCheck.leftGO.layer == ground)
                {
                    aS.clip=blockedSound; aS.Play();
                    aS.Play();//Play Blocked Sound;
                }

            }
            else
            {

                prevPos = transform.position;
                playerPos.x -= travelDistance;
                playerPos.y = transform.position.y;
                transform.position = playerPos;
                direction = MoveVector.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (playerCheck.rightGO != null)
            {
                blockCS = playerCheck.rightGO.GetComponent<CheckSurroundings>();
                if (blockCS != null)
                {
                    if (blockCS.rightBlocked)
                    {
                        aS.clip = blockedSound;
                        aS.Play();
                        //Play Blocked Sound;
                    }
                    else
                    {

                        prevPos = transform.position;
                        playerPos.x += travelDistance;
                        playerPos.y = transform.position.y;
                        transform.position = playerPos;
                        direction = MoveVector.Right;
                    }
                }
                else if (playerCheck.rightGO.layer == ground)
                {
                    aS.clip = blockedSound;
                    aS.Play();//Play Blocked Sound;
                }      
            }
            else
            {
;
                prevPos = transform.position;
                playerPos.x += travelDistance;
                playerPos.y = transform.position.y;
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
                if(blockCS != null){
                    if (blockCS.forwardBlocked)
                    {
                        aS.clip = blockedSound;
                        aS.Play();//Play Block Sound

                    }
                    else
                    {

                        prevPos = transform.position;
                        playerPos.z += travelDistance;
                        playerPos.y = transform.position.y;
                        transform.position = playerPos;
                        direction = MoveVector.Forward;
                    }
                }
                else if(playerCheck.forwardGO.layer == ground)
                {
                    aS.clip = blockedSound;
                    aS.Play();               //Play Block Sound
                }
                       

            }
            else
            {
                prevPos = transform.position;
                playerPos.z += travelDistance;
                playerPos.y = transform.position.y;
                transform.position = playerPos;
                direction = MoveVector.Forward;
            }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (playerCheck.backwardGO != null)
            {
                blockCS = playerCheck.backwardGO.GetComponent<CheckSurroundings>();
                if(blockCS != null)
                {
                    if (blockCS.backwardBlocked)
                    {
                        aS.clip = blockedSound;
                        aS.Play(); //Play Blocked Sound;
                    }
                    else
                    {
   
                        prevPos = transform.position;
                        playerPos.z -= travelDistance;
                        playerPos.y = transform.position.y;
                        transform.position = playerPos;
                        direction = MoveVector.Backward;
                    }
                }
                else if(playerCheck.backwardGO.layer == ground)
                {
                    aS.clip = blockedSound;
                    aS.Play(); //Play Blocked Sound;
                }
            }
            else
            {
                
                prevPos = transform.position;
                playerPos.z -= travelDistance;
                playerPos.y = transform.position.y;
                transform.position = playerPos;
                direction = MoveVector.Backward;
            }
        }
    }
    void Push(GameObject collision)
    {
        VerticalCheck cVC = collision.GetComponent<VerticalCheck>();
        if (_canPush)
        {
            Debug.Log("Push: " + collision);
            
            Vector3 collisionPos = collision.transform.position;
            aS.clip = pushSound;

            if (direction == MoveVector.Left && blockCS.leftBlocked == false)
            {
                collisionPos.x -= travelDistance;
                collision.transform.position = collisionPos;
            }
            if (direction == MoveVector.Right && blockCS.rightBlocked == false)
            {
                collisionPos.x += travelDistance;
                collision.transform.position = collisionPos;
            }
            if (direction == MoveVector.Forward && blockCS.forwardBlocked == false)
            {
                collisionPos.z += travelDistance;
                collision.transform.position = collisionPos;
            }
            if (direction == MoveVector.Backward && blockCS.backwardBlocked == false)
            {
                collisionPos.z -= travelDistance;
                collision.transform.position = collisionPos;
            }
            if (cVC != null && cVC._connectedAbove)
            {
                Push(cVC.blockAbove);
            }
            aS.Play();
        }
        StartCoroutine(Cooldown());
    }
    bool canPush()
    {
        if (gameObject.GetComponent<GroundCheck>().isFalling)
        {
            return false;
        }
        else { return true; }

    }
    public void Reset()
    {
        playerPos = spawnPos;
        transform.position = playerPos;
       
    }
    private IEnumerator Cooldown()
    { 
        _canPush = false;
        yield return new WaitForSeconds(_cooldown);
        _canPush = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Push(other.gameObject);
        
    }
}
