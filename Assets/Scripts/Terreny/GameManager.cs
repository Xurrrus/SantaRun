using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ObjecteInstanciat;
    public GameObject ObjecteInicial;
    public Transform instantiatePos;
    public float ObjecteLength;
    GameObject lastObjecte = null;

    double timerAugmentarVel;
    float velocitat;
    Movement mv;
    public Transform groundParent;
    public float currentDist;

    void Start(){
        lastObjecte = ObjecteInicial;

        velocitat = 1f;
        mv = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }


    void Update(){
        currentDist = Vector3.Distance(instantiatePos.position, lastObjecte.transform.position);
        if(currentDist > ObjecteLength){
            int randomNum = Random.Range(0, ObjecteInstanciat.Length);
            lastObjecte = Instantiate(ObjecteInstanciat[randomNum], instantiatePos.position, Quaternion.identity, groundParent);
        }

        AugmentarVelPlayer();
    }

    void AugmentarVelPlayer(){
        timerAugmentarVel += Time.deltaTime;
        if(timerAugmentarVel > 5){
            mv.augmentarVelocitat(); 
            velocitat = mv.retornarVelocitat(); 
            timerAugmentarVel = 0f;
        }
        Debug.Log(velocitat);
    }
}
