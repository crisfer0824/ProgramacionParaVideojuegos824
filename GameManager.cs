using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI startText;

    public TextMeshProUGUI gameOverText;

    private float timeSurvived = 0f;
    private bool isGameOver = false;
    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        if (!gameStarted || isGameOver) return;

        timeSurvived += Time.deltaTime;
        timeText.text = "Tiempo: " + Mathf.FloorToInt(timeSurvived);

        if (isGameOver && Input.GetKeyDown(KeyCode.R))
{
    Time.timeScale = 1f;
    UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
    );
}
    }

    IEnumerator StartCountdown()
    {
        startText.gameObject.SetActive(true);

        startText.text = "3";
        yield return new WaitForSecondsRealtime(1f);

        startText.text = "2";
        yield return new WaitForSecondsRealtime(1f);

        startText.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        startText.text = "START";
        yield return new WaitForSecondsRealtime(1f);

        startText.gameObject.SetActive(false);

        Time.timeScale = 1f;
        gameStarted = true;
    }

    public void EndGame()
{
    isGameOver = true;

    gameOverText.gameObject.SetActive(true);

    Time.timeScale = 0f;
}
}