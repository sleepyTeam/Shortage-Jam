using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
    }
    private void Start()
    {
        Debug.LogError(levelIndex);
        player = GameObject.FindGameObjectWithTag("Player");
        foreach(BlockPosition block in blockPositions)
        {
            startPositions.Add(block.startPos);
        }

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