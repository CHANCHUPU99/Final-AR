using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {
    Rigidbody m_rigidbody;
    GameObject ball;
    void Start() {
        ball.GetComponent<Rigidbody>();
    }


    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            Debug.Log("se oprimio");
            //aqui se llama a la funcion lanzar 
        }
    }

    private void wallLogic() {

    }
}
