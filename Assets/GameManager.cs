using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public GameObject winPanel;
    public float timeToWin = 120f;
    public bool gameOver;
    public GameObject baseTarget;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }
    
    public void LoadSceneNum(int num)
    {
        SceneManager.LoadScene(num);
    }
    private void Update()
    {
        if (!gameOver)
        {
            timeToWin -= Time.deltaTime;
            if (timeToWin <= 0)
            {
                gameOver = true;
                winPanel.SetActive(true);
                baseTarget.GetComponent<Base>().spawner.CancelInvoke();
            }
        }
    }

    public void QuitApp()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
