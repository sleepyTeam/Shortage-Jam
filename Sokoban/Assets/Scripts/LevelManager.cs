using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private int levelIndex;
    public GameObject spawnPoint;
    public GameObject endPoint;
    public GameObject player;
    private PlayerMovement pM;
    public GameObject playerPrefab;
    private float playerYOffset = .75f;

    void Awake()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Respawn();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Respawn()
    {
        if (player == null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Spawning New Player");
                
                GameObject player = Instantiate(playerPrefab, new Vector3( spawnPoint.transform.position.x, spawnPoint.transform.position.y + playerYOffset, spawnPoint.transform.position.z), Quaternion.identity);

            }
        }
    }
}
