using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int score = 0;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("ball")) {
            score++;
            Debug.LogWarning(score);
        }
    }
}
