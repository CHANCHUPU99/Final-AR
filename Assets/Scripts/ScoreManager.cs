using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject transformBall;
    
    void Start()
    {
        scoreTxt.text = "Score: " + score.ToString();
        //actualScene = SceneManager.GetActiveScene().name;
    }

    private void Update() {
      
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("ball")) {
            score++;
            scoreTxt.text = "Score: " + score.ToString();
            ball.transform.position = transformBall.transform.position;
            Debug.LogWarning(score);

        }
    }
}
