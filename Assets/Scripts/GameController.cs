using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject[] Debris;
    [SerializeField] private int debrisPoolCount;
    [SerializeField] private float debrisSpawnTime;
    [SerializeField] private float scoreRate;
    [SerializeField] private float timeStopRate;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject restartCanvas;
    public float score;
    private bool hasStarted = false;

    private List<GameObject> debrisPool = new List<GameObject>();

    private void Awake()
    {
        poolDebris();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !hasStarted)
        {
            startGame();
        }

        if (hasStarted)
        {
            updateScore();
        }
    }

    void poolDebris()
    {
        for(int i = 0;i < debrisPoolCount; i++)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.zero);
            GameObject debrisInstance = Instantiate(Debris[Random.Range(0, 2)], Vector3.zero, rotation, this.transform);
            debrisInstance.SetActive(false);
            debrisPool.Add(debrisInstance);
        }
    }

    IEnumerator activateDebris()
    {
        while (true)
        {
            foreach (GameObject debris in debrisPool)
            {
                if (!debris.activeSelf)
                {
                    Vector3 position = new Vector3(10.0f, Player.transform.position.y + Random.Range(-1.0f, 1.0f), 0.0f);
                    var randomZRotation = debris.transform.GetChild(0).eulerAngles;
                    randomZRotation.z = Random.Range(0.0f, 360.0f);

                    debris.transform.position = position;
                    debris.transform.GetChild(0).eulerAngles = randomZRotation;
                    debris.SetActive(true);
                    break;
                }
            }

            //debrisSpawnTime = debrisSpawnTime + Random.Range(-0.5f, 0.5f);
            //Debug.Log(debrisSpawnTime);
            yield return new WaitForSeconds(debrisSpawnTime);
        }
    }

    public void startGame()
    {
        StopCoroutine("timeStop");
        foreach (GameObject debris in debrisPool)
        {
            debris.SetActive(false);
        }

        Player.SetActive(true);
        Time.timeScale = 1;
        score = 0;
        hasStarted = true;
        StartCoroutine("activateDebris");
    }

    IEnumerator timeStop()
    {
        while(Time.timeScale > 0.1f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.0f, timeStopRate);
            Debug.Log(Time.timeScale);
            yield return null;
        }
        hasStarted = false;
        restartCanvas.SetActive(true);
        Debug.Log("Game Stopped!");
    }

    public void stopGame()
    {
        StopCoroutine("activateDebris");
        StartCoroutine("timeStop");
        // Time.timeScale = 0;
    }

    public void updateScore()
    {
        score = score + scoreRate;
        scoreText.text = "Score: " + score;
    }
}
