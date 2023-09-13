using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {
    private Vector3 touchStartPos; // Posici�n inicial del toque
    private Vector3 touchEndPos;   // Posici�n final del toque
    private Vector3 lanzamientoDirection; // Direcci�n del lanzamiento
    private Rigidbody rb;
    private Camera mainCamera;

    public float fuerzaLanzamiento = 10f; // Fuerza de lanzamiento
    public float fuerzaElevacion = 5f;   // Fuerza de elevaci�n inicial
    public float maxFuerzaLanzamiento = 20f; // Fuerza m�xima de lanzamiento
    public float maxDistanciaLanzamiento = 5f; // Distancia m�xima para lanzar

    private bool isDragging = false;
    private bool canMove = true; // Variable para controlar si la pelota se puede mover

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Hacemos que el objeto no responda a la f�sica mientras no lo estamos lanzando.
        mainCamera = Camera.main; // Obtener la c�mara principal
    }

    private void Update() {
        // Manejo de entrada t�ctil
        if (canMove && Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0); // Tomamos el primer toque (asumimos un solo toque)

            switch (touch.phase) {
                case TouchPhase.Began:
                    isDragging = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchEndPos = touch.position;
                    // Calculamos la direcci�n del lanzamiento en tiempo real mientras se mueve el dedo
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

    private void Lanzar() {
        rb.isKinematic = false; // Habilitamos la f�sica para que el objeto responda al lanzamiento
        canMove = false; // Desactivamos la capacidad de mover la pelota

        // Calculamos la direcci�n y fuerza del lanzamiento bas�ndonos en la diferencia de posiciones
        float dragDistance = Mathf.Clamp((touchEndPos - touchStartPos).magnitude, 0f, maxDistanciaLanzamiento);
        float forceMultiplier = dragDistance / maxDistanciaLanzamiento;

        // Aplicamos la fuerza hacia arriba para la elevaci�n inicial
        rb.AddForce(Vector3.up * fuerzaElevacion, ForceMode.Impulse);

        // Aplicamos la fuerza de lanzamiento en la direcci�n calculada
        float forceStrength = fuerzaLanzamiento + maxFuerzaLanzamiento * forceMultiplier;
        rb.AddForce(lanzamientoDirection * forceStrength, ForceMode.Impulse);

        // Reiniciamos las variables
        touchStartPos = Vector3.zero;
        touchEndPos = Vector3.zero;
    }

    private Vector3 CalculateLaunchDirection() {
        // Calculamos un rayo desde la c�mara hacia la posici�n actual del toque
        Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

        // Usamos la direcci�n del rayo como direcci�n de lanzamiento
        return ray.direction;
    }
}

