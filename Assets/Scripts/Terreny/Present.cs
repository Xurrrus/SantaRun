using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, -20 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.tag == "Present"){
            if(other.tag == "Player"){
                PlayerManager.numberOfPresents += 1;
                //Debug.Log("Presents: " + PlayerManager.numberOfPresents);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }
}
