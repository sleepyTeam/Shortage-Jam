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
    public GameObject BlockPrefab;
    private float playerYOffset = .75f;
    public List<BlockPosition> blockPositions = new List<BlockPosition>();
    public List<Vector3> startPositions = new List<Vector3>();
    

    void Awake()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach(BlockPosition block in blockPositions)
        {
            startPositions.Add(block.startPos);
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetBlocks();
        }
    }
    void Respawn()
    {
        if (player == null)
        {
            Debug.Log("Spawning New Player");
            player = Instantiate(playerPrefab, new Vector3( spawnPoint.transform.position.x, spawnPoint.transform.position.y + playerYOffset, spawnPoint.transform.position.z), Quaternion.identity);
        }
        else
        {
            player.gameObject.GetComponent<PlayerMovement>().Reset();
        }
    }
    void ResetBlocks()
    {
        for (int i = 0;  i < blockPositions.Count; i++)
        {
            BlockPosition block = blockPositions[i];
            if (block != null)
            {
                if (block.gameObject == null)
                {
                    GameObject NB =Instantiate(BlockPrefab, block.startPos, Quaternion.identity);
                }
                else block.gameObject.transform.position = block.startPos;
            }
            else
                {
                    GameObject NB = Instantiate(BlockPrefab, startPositions[i], Quaternion.identity);
                    blockPositions[i] = NB.GetComponent<BlockPosition>();
                }              
        }
        Respawn();
    }

}