using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField] GameObject ringGame;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject button;

    private void Start () {
       
    }
    public void startGame() {
        /*Instantiate(ringGame);
        Instantiate(ball);
        Instantiate(canvas);*/
        ringGame.SetActive(true);
        ball.SetActive(true);
        canvas.SetActive(true);
        button.SetActive(false);
    }
    public void changeScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex);
    }
}
