using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class LanzamientoBalon : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 lanzamientoDirection;
    private Rigidbody rb;
    private bool isDragging = false;
    private bool canMove = true;
    public float maxFuerzaLanzamiento = 20f;
    private Camera mainCamera;
    public float fuerzaLanzamiento = 10f;
    public float fuerzaElevacion = 5f;
    public float maxDistanciaLanzamiento = 5f;
    public float tiempoEsperaParaLanzar = 2f; 
    private bool canLaunch = true; 

    public TextMeshProUGUI scoreTxt;
    //public ParticleSystem particles;
    private int score = 0;

    private Vector3 initialPosition; 
    public GameObject ball;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        mainCamera = Camera.main;
        initialPosition = transform.position; // Almacena la posición inicial de la pelota

        scoreTxt.text = "Score: " + score.ToString();
    }

    private void Update() {
        if (canMove && Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    isDragging = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchEndPos = touch.position;
                    lanzamientoDirection = calculateLaunchDirection();
                    break;

                case TouchPhase.Ended:
                    if (isDragging) {
                        if (canLaunch) {
                            lanzar();
                            canLaunch = false;
                            Invoke("HabilitarLanzamiento", tiempoEsperaParaLanzar);
                        }
                    }
                    isDragging = false;
                    break;
            }
        }
    }

    private void lanzar() {
        rb.isKinematic = false;
        canMove = false;

        float dragDistance = Mathf.Clamp((touchEndPos - touchStartPos).magnitude, 0f, maxDistanciaLanzamiento);
        float forceMultiplier = dragDistance / maxDistanciaLanzamiento;

        rb.AddForce(Vector3.up * fuerzaElevacion, ForceMode.Impulse);

        float forceStrength = fuerzaLanzamiento + maxFuerzaLanzamiento * forceMultiplier;
        rb.AddForce(lanzamientoDirection * forceStrength, ForceMode.Impulse);

        touchStartPos = Vector3.zero;
        touchEndPos = Vector3.zero;
    }

    private Vector3 calculateLaunchDirection() {
        Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
        return ray.direction;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Canasta") && canMove) {
            score++;
            scoreTxt.text = "Score: " + score.ToString();
            //particles.Play();
            //canMove = false;             
            Destroy(ball);
            ball = Instantiate( ball, initialPosition, Quaternion.identity);
            habilitarLanzamiento();
            //canLaunch = true;
        }
        if (other.CompareTag("Empty")) {
            //canMove = false;
            Destroy(ball);
            ball = Instantiate(ball, initialPosition, Quaternion.identity);
            habilitarLanzamiento();
            //canLaunch = true;
        }
    }

    private void habilitarLanzamiento() {
        canMove = true;
    }
}
