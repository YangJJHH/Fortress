using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onReset;

    public static GameManager instance;

    public GameObject readyPannel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI messageText;

    public bool isRoundActive = false;

    private int score = 0;

    public ShooterRotator shooterRotator;

    public CamFollow cam;

    private void Awake()
    {
        UpdateUI();
        instance = this;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
    }

    void UpdateBestScore()
    {
        if(GetBestScore()< score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        
    }

    int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RoundRoutine());
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        bestScoreText.text = $"Best Score: {GetBestScore()}";
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();
        //라운드를 다시 처음부터 시작
        StartCoroutine(RoundRoutine());
        
    }

    IEnumerator RoundRoutine()
    {
        //Ready
        onReset.Invoke();
        readyPannel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;
        messageText.text = "Ready....";
        yield return new WaitForSeconds(1.5f);

        //Play
        isRoundActive = true;
        readyPannel.SetActive(false);
        shooterRotator.enabled = true;
        cam.SetTarget(shooterRotator.transform,CamFollow.State.Ready);

        while (isRoundActive)
        {

            yield return null;
        }

        //END
        readyPannel.SetActive(true);
        shooterRotator.enabled=false;
        messageText.text = "Wait For Next Round";

        yield return new WaitForSeconds(1.5f);
        Reset();

    }

}
