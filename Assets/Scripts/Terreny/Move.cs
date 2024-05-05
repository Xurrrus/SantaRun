using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float velocitat;

    private Movement mv;

    void Start(){
        mv = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    void Update()
    {
        velocitat = mv.retornarVelocitat(); 
        transform.position += new Vector3(0, 0, -15)* velocitat *Time.deltaTime;
    }
}
