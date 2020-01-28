using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    [HideInInspector]
    public bool started = false;
    [HideInInspector]
    public bool gameOver = false;

    public float perfectTolerance = 0.1f;

    [Header("References")]
    public AnalyseLevelMakerFile analyser;
    public Transform defilement;
    public AudioSource audioSource;
    public AvatarBehaviour avatarBehaviour;
    public Button restartButton;
    public ScoreManager scoreManager;
    public GameObject perfectPrefab;

    private int previousTouchCount;

    private float timer;
    private int perfectStreak = 0;

    private void Update()
    {
        if (gameOver)
            return;

        if (!started)
        {
            WaitForStart();
            return;
        }

        Playing();

        if (Input.anyKeyDown || (Input.touchCount != 0 && previousTouchCount == 0))
            OnClick();

        previousTouchCount = Input.touchCount;
    }


    private void WaitForStart()
    {
        if (!Input.anyKeyDown && Input.touchCount == 0)
            return;

        started = true;
        audioSource.clip = analyser.clip;
        audioSource.Play();
    }

    private void Playing()
    {
        defilement.position += Vector3.up * Time.deltaTime;
        timer += Time.deltaTime;

        if (timer > analyser.levelMakerFile.events[0].endTime)
        {
            GameOver();
            avatarBehaviour.Explode();
        }
    }

    private void OnClick()
    {
        if (timer < analyser.levelMakerFile.events[0].startTime)
        {
            GameOver();
            avatarBehaviour.Crash();
            return;
        }

        float middle = (analyser.levelMakerFile.events[0].startTime + analyser.levelMakerFile.events[0].endTime) / 2;

        if (Mathf.Abs(middle - timer) <= perfectTolerance)
        {
            scoreManager.AddScore(30);
            perfectStreak++;
            Perfect();
        }
        else
        {
            scoreManager.AddScore(10);
            perfectStreak = 0;
        }

        avatarBehaviour.SwitchSide();
        analyser.levelMakerFile.events.RemoveAt(0);
    }


    private void Perfect()
    {
        GameObject perfectText = Instantiate(perfectPrefab, avatarBehaviour.visual.transform.position, Quaternion.identity);
        Destroy(perfectText, 1);
        perfectText.transform.GetChild(0).GetComponent<TextMesh>().text = "Perfect x" + perfectStreak;
    }



    public void GameOver()
    {
        gameOver = true;
        audioSource.Stop();
        restartButton.gameObject.SetActive(true);
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);//Ultra basic, but since you told me that the interface was generic and done by another team, I did not made a real menu
    }

}
