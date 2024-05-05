using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirCarreteres : MonoBehaviour
{
    void start(){
        transform.position = transform.position;
        transform.rotation = transform.rotation;
    }

    void OnTriggerEnter(Collider coll){
        if(coll.gameObject.CompareTag("Ground") || coll.gameObject.CompareTag("Obstacle")){
            Destroy(coll.gameObject);
        }
    }

}
