using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class LanzamientoBalon : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 lanzamientoDirection;
    private Rigidbody rb;
    public Camera vuforiaCamera;

    public float fuerzaLanzamiento = 10f;
    public float maxFuerzaLanzamiento = 20f;

    private bool isDragging = false;
    private bool canMove = true;
    public string actualScene;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        vuforiaCamera = Camera.main;
        actualScene = SceneManager.GetActiveScene().name;
    }

    private void Update() {
        Quaternion deviceRotation = Input.gyro.attitude;
        Vector3 gravityDirection = deviceRotation * Vector3.down;    
        Physics.gravity = gravityDirection * 9.81f;
        if (canMove && Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    isDragging = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchEndPos = touch.position;
                    lanzamientoDirection = CalculateLaunchDirection();
                    break;

                case TouchPhase.Ended:
                    if (isDragging) {
                        Lanzar();
                    }
                    isDragging = false;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Empty")) {
            SceneManager.LoadScene(actualScene);
        }
    }
    private void Lanzar() {
        rb.useGravity = true;
        canMove = false;

        float dragDistance = Mathf.Clamp((touchEndPos - touchStartPos).magnitude, 0f, Mathf.Infinity);
        float forceMultiplier = dragDistance / Screen.height;

        rb.AddForce(lanzamientoDirection * (fuerzaLanzamiento + maxFuerzaLanzamiento * forceMultiplier), ForceMode.Impulse);
    }

    private Vector3 CalculateLaunchDirection() {
        Ray ray = vuforiaCamera.ScreenPointToRay(Input.GetTouch(0).position);
        return ray.direction;
    }
}
