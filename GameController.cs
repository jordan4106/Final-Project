using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    public GameObject Button;

    public GameObject[] bosses;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public int bossCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public AudioSource LoseAudio;
    public AudioSource WinAudio;
    public AudioSource GameAudio;



    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text timewinText;

    private bool gameOver;
    private bool restart;
    private bool win;
    private bool timewin;
    public int score;

    
    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        timewinText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

        timer = mainTimer;
        canCount = false;

    }

    void Update()
    {

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
            gameOver = false;
            win = false;
        }

        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
            
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            for (int i = 0; i < bossCount; i++)
            {
                GameObject boss = bosses[Random.Range(0, bosses.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(boss, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

        



            if (gameOver)
            {
                restartText.text = "Press 'O' for Restart";
                restart = true;
                break;
            }
            if (win)
            {
                break;
            }

            if (timewin)
            {
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        if (canCount == false)
        {
        if (score >= 100)
        {
                winText.text = "You win! Game created by Jordan Marr!";
                win = true;
                gameOver = true;
                restart = true;
                GameAudio.Stop();
                WinAudio.Play();
            }
        }

        if (canCount == true)
        {
            if (timer <= 0.00 && score > 50)
            {
                timewinText.text = ("Congrats! Your score was " + score);
                timewin = true;
            }
        }
    }
        public void GameOver()
    {
        gameOverText.text = "Game Over! Game made by Jordan Marr!";
        gameOver = true;
        GameAudio.Stop();
        LoseAudio.Play();
    }
        public void ResetBtn()
    {
        timer = mainTimer;
        canCount = true;
        doOnce = true;
    }
}