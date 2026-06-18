using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public bool isGamePaused = false;

    public GameObject HUD;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pauseScoreText;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = $"Score: {score}\nBest: {PlayerPrefs.GetInt("HighScore")}";
        pauseScoreText.text = scoreText.text;
    }
    public void AddScore(int num)
    {
        score += num;
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            scoreText.text = $"Score: {score}\nBest: {PlayerPrefs.GetInt("HighScore")}";
            pauseScoreText.text = scoreText.text;
        }
        scoreText.text = $"Score: {score}\nBest: {PlayerPrefs.GetInt("HighScore")}";
        pauseScoreText.text = scoreText.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        HUD.SetActive(!HUD.activeSelf);
        isGamePaused = !isGamePaused;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        HUD.SetActive(false);
        isGameOver = true;
    }

    public void ChangeScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
