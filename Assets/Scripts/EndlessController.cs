using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndlessController : GameController
{
    private Constants constants = new Constants();
    [SerializeField] GameObject[] obstacles;
    [SerializeField] List<GameObject> obstaclePool = new List<GameObject>();
    [SerializeField] GameObject player;

    private int obstacleUnit = 1;

    private void Awake()
    {
        hasStarted = false;
        poolObstacles();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !hasStarted)
        {
            startGame();
        }

        Debug.Log(obstacleUnit * constants.blockLength - player.transform.position.x);
        if((obstacleUnit*constants.blockLength - player.transform.position.x) < 24.0f){
            Debug.Log("Spawn Obstacle");
            spawnObstacle();
        }
    }

    public override void updateScore()
    {
        score = score + scoreRate;
        scoreText.text = "Score: " + score;
    }

    void poolObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.zero);
            GameObject obstacleInstance = Instantiate(obstacles[i], Vector3.zero, rotation, this.transform);
            obstacleInstance.SetActive(false);
            obstaclePool.Add(obstacleInstance);
        }
    }

    public void spawnObstacle()
    {
        /*foreach (GameObject obstacle in obstaclePool)
        {
            if (!obstacle.activeSelf)
            {
                Vector3 position = new Vector3(constants.blockLength*obstacleUnit, 0.0f, 0.0f);

                obstacle.transform.position = position;
                obstacle.SetActive(true);
                obstacleUnit = obstacleUnit + 1;
                Debug.Log(obstacleUnit);
                break;
            }
        }*/

        while (true)
        {
            int obstacleNumber;
            if(score < 9)
            {
                obstacleNumber = Random.Range(0, 4);
            }else if (score >= 10 && score < 35)
            {
                obstacleNumber = Random.Range(0, 7);
            }
            else
            {
                obstacleNumber = Random.Range(0, obstacles.Length);
            }

            GameObject obstacleToBeSpawned = obstaclePool[obstacleNumber];

            if (obstacleToBeSpawned.activeSelf)//if active, find another one
            {
                continue;
            }
            else
            {
                Vector3 position = new Vector3(constants.blockLength * obstacleUnit, 0.0f, 0.0f);

                obstacleToBeSpawned.transform.position = position;
                obstacleToBeSpawned.SetActive(true);
                obstacleUnit = obstacleUnit + 1;
                break;
            }
        }

        
    }
}
