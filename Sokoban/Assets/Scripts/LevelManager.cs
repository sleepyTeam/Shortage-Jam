using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private bool spinning = false;
    public bool isDone = false;
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
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (BlockPosition block in blockPositions)
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
        NextLevel();
    }

    void Respawn()
    {
        if (player == null)
        {
            Debug.Log("Spawning New Player");
            player = Instantiate(playerPrefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + playerYOffset, spawnPoint.transform.position.z), Quaternion.identity);
        }
        else
        {
            player.gameObject.GetComponent<PlayerMovement>().Reset();
        }
    }

    void ResetBlocks()
    {
        for (int i = 0; i < blockPositions.Count; i++)
        {
            BlockPosition block = blockPositions[i];
            if (block != null)
            {
                if (block.gameObject == null)
                {
                    GameObject NB = Instantiate(BlockPrefab, block.startPos, Quaternion.identity);
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

    void NextLevel()
    {
        if (player != null && endPoint != null && (player.transform.position.x == endPoint.transform.position.x && player.transform.position.z == endPoint.transform.position.z))
        {
            Destroy(endPoint.gameObject);
            StartCoroutine("End");
            isDone = true;
        }

        if (spinning)
        {
            player.transform.rotation = new Quaternion(0, Quaternion.identity.y + 5 * Time.deltaTime, 0, Quaternion.identity.w);
        }
    }

    private IEnumerator End()
    {
        spinning = true;

        yield return new WaitForSeconds(2);

        spinning = false;

        levelIndex++;
        if (levelIndex > SceneManager.sceneCount) levelIndex = 0;
        Debug.Log(levelIndex);
        SceneManager.LoadScene(levelIndex);
    }
}