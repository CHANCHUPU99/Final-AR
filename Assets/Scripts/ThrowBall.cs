using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {
    private Vector3 touchStartPos; // Posición inicial del toque
    private Vector3 touchEndPos;   // Posición final del toque
    private Rigidbody rb;

    public float fuerzaLanzamiento = 10f; // Fuerza de lanzamiento
    public float fuerzaElevacion = 5f;   // Fuerza de elevación inicial
    public float maxFuerzaLanzamiento = 20f; // Fuerza máxima de lanzamiento
    public float maxDistanciaLanzamiento = 5f; // Distancia máxima para lanzar

    private bool isDragging = false;
    private bool canMove = true; // Variable para controlar si la pelota se puede mover

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Hacemos que el objeto no responda a la física mientras no lo estamos lanzando.
    }

    private void Update() {
        // Manejo de entrada táctil
        if (canMove && Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0); // Tomamos el primer toque (asumimos un solo toque)

            switch (touch.phase) {
                case TouchPhase.Began:
                    isDragging = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchEndPos = touch.position;
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
        rb.isKinematic = false; // Habilitamos la física para que el objeto responda al lanzamiento
        canMove = false; // Desactivamos la capacidad de mover la pelota

        // Calculamos la dirección y fuerza del lanzamiento basándonos en la diferencia de posiciones
        Vector3 dragDirection = touchStartPos - touchEndPos;
        float dragDistance = Mathf.Clamp(dragDirection.magnitude, 0f, maxDistanciaLanzamiento);
        float forceMultiplier = dragDistance / maxDistanciaLanzamiento;

        Vector3 lanzamientoVector = new Vector3(dragDirection.x, 0f, dragDirection.y).normalized;
        float forceStrength = fuerzaLanzamiento + maxFuerzaLanzamiento * forceMultiplier;

        // Aplicamos la fuerza hacia arriba para la elevación inicial
        rb.AddForce(Vector3.up * fuerzaElevacion, ForceMode.Impulse);

        // Aplicamos la fuerza de lanzamiento
        rb.AddForce(lanzamientoVector * forceStrength, ForceMode.Impulse);

        // Reiniciamos las variables
        touchStartPos = Vector3.zero;
        touchEndPos = Vector3.zero;
    }
}

