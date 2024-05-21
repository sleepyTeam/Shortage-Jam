using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private int levelIndex = 0;
    public GameObject spawnPoint;
    public GameObject endPoint;
    public GameObject player;
    private PlayerMovement pM;
    public GameObject playerPrefab;
    private float playerYOffset = .75f;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
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

    void NextLevel()
    {
        if(player != null && player.transform.position.x == endPoint.transform.position.x && player.transform.position.z == endPoint.transform.position.z)
        {
            levelIndex++;
            Debug.Log("Yo");
            SceneManager.LoadScene(levelIndex);
        }
    }
}
