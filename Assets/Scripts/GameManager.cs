using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    int points = 0;

    [SerializeField] GameObject cameraPosition;
    [SerializeField] GameObject arCamera;

    void Start() {
        
    }

    public void cameraPos() {
        arCamera.transform.position = cameraPosition.transform.position;
    }
    public void reloadScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
