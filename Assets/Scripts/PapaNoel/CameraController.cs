using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetFollow;
    Vector3 initialPos;

    void Start(){
        initialPos = new Vector3(transform.position.x, transform.position.y-targetFollow.position.y, transform.position.z);
    }

    void Update(){
        transform.position = new Vector3(initialPos.x, targetFollow.position.y + initialPos.y, initialPos.z);
    }
}
