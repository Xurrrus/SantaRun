using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [Header("Moviment Horizontal")]
    public int liniaActualX = 1;
    public int liniaActualY = 0;
    public float distEntreLinies = 4f;
    public float tempsTranslacioCostats = 0.1f;
    public bool potDesplacament = true;

    [Header("Saltar")]
    public Rigidbody rb;
    private float forcaSalt = 12f;
    public bool tocaAlTerra;

    Coroutine desplacamentCoroutine, volarCoroutine;

    [Header("Volar")]
    public float t;
    public float tempsPujar, tempsBaixar;

    private float comptador;
    private bool activarTimer;

    public float velocitatCarreteres;

    private bool pararJoc = false;

    public GameObject pantallaMort;

    [Header("ControlsMovil")]
    public bool controlsMobils = false;
    private Vector2 touchStartPos;
    private bool isDragging = false;
    public float dragThreshold;


    void Start(){

        activarTimer = false;
        velocitatCarreteres = 1f;

    }
    void Update()
    {
        if(!controlsMobils && !pararJoc){
            jugarOrdinador();

        }
        else if(!pararJoc && controlsMobils){
           jugarMobil();
        }

        
        if(activarTimer) comptador += Time.deltaTime;


        if(comptador > 5){
            if(liniaActualY==1){
                volarCoroutine = StartCoroutine(Volar(false, tempsBaixar));
                liniaActualY--;
                activarTimer = false;
                comptador = 0;
            }
        }

        if(pararJoc){

            velocitatCarreteres = 0f;
            PlayerManager.gameOver = true;

        }


    }

    IEnumerator DesplacarHorizontal(Vector3 direction, float tempsTranslacio)
    {
        potDesplacament = false;
        Vector3 targetPosition = transform.position + direction;
        Vector3 posInicial = transform.position;
        float elapsedTime = 0f;
        t=0;

        while (elapsedTime < tempsTranslacio)
        {
            t = elapsedTime / tempsTranslacio;
            Vector3 newPosition = Vector3.Lerp(posInicial, targetPosition, t);
            transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        t=1;

        potDesplacament = true;
        transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z); 
        desplacamentCoroutine = null;
    }

    IEnumerator Volar(bool volar, float tempsTranslacio)
    {
        Vector3 targetPosition;
        if(volar){
            targetPosition = transform.position + Vector3.up * 20f;
            rb.useGravity = false;
        }
        else{
            targetPosition = transform.position + Vector3.down * 20f;
        }
        Vector3 posInicial = transform.position;
        float elapsedTime = 0f;
        t=0;

        while (elapsedTime < tempsTranslacio)
        {
            t = elapsedTime / tempsTranslacio;
            Vector3 newPosition = Vector3.Lerp(posInicial, targetPosition, t);
            transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        t=1;

        if(!volar) rb.useGravity = true;
        transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z); 
        volarCoroutine = null;
    }

    void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag == "Obstacle"){
            Debug.Log("xocat");
            pararJoc = true;
        }
    }

    void OnCollisionStay(Collision coll){
        if(coll.gameObject.tag == "Ground"){
            tocaAlTerra = true;
        }
        if(coll.gameObject.tag == "Obstacle"){
            Debug.Log("xocat");
            pararJoc = true;
        }
    }

    void OnCollisionExit(Collision coll){
        if(coll.gameObject.tag == "Ground"){
            tocaAlTerra = false;
        }
    }

    private void Saltar(){
        if(gameObject.transform.position.y < 0.1)rb.AddForce(transform.up * forcaSalt, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other){

        if(other.tag == "Fly"){
            if(liniaActualY==0){
                volarCoroutine = StartCoroutine(Volar(true, tempsPujar));
                liniaActualY++;
                activarTimer = true;
            }
        }

    }

    public void augmentarVelocitat(){

        velocitatCarreteres += 0.02f;

    }

    public float retornarVelocitat(){

        return velocitatCarreteres;

    }

    private void jugarOrdinador(){
        if (Input.GetKeyDown(KeyCode.RightArrow) && liniaActualX < 2 && potDesplacament && desplacamentCoroutine == null)
        {
            desplacamentCoroutine = StartCoroutine(DesplacarHorizontal(Vector3.right * distEntreLinies, tempsTranslacioCostats));
            liniaActualX++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && liniaActualX > 0 && potDesplacament && desplacamentCoroutine == null)
        {
            desplacamentCoroutine = StartCoroutine(DesplacarHorizontal(Vector3.left * distEntreLinies, tempsTranslacioCostats));
            liniaActualX--;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && tocaAlTerra)
        {
            Saltar();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && volarCoroutine == null)
        {
            if(liniaActualY==1){
                volarCoroutine = StartCoroutine(Volar(false, tempsBaixar));
                liniaActualY--;
            }
        }
    } 
    private void jugarMobil(){
         if (Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                
                // Comprova si l'arrossegament ha començat
                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                    isDragging = true;
                }
                
                else if (touch.phase == TouchPhase.Moved && isDragging)
                {
                    Vector2 dragDelta = touch.position - touchStartPos;

                    
                    if (Mathf.Abs(dragDelta.x) > dragThreshold)
                    {
                        
                        if (dragDelta.x > 0 && liniaActualX < 2 && potDesplacament && desplacamentCoroutine == null)
                        {
                            // Arrossegar cap a la dreta
                            desplacamentCoroutine = StartCoroutine(DesplacarHorizontal(Vector3.right * distEntreLinies, tempsTranslacioCostats));
                            liniaActualX++;
                        }
                        else if(liniaActualX > 0 && potDesplacament && desplacamentCoroutine == null)
                        {
                            // Arrossegar cap a l'esquerra
                            desplacamentCoroutine = StartCoroutine(DesplacarHorizontal(Vector3.left * distEntreLinies, tempsTranslacioCostats));
                            liniaActualX--;
                        }
                    }
                    else if (Mathf.Abs(dragDelta.y) > dragThreshold)
                    {
                       
                        if (dragDelta.y > 0 && tocaAlTerra)
                        {              
                            Saltar();
                        }

                    }
                    touchStartPos = touch.position;
                    
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isDragging = false;
                }
            }
    }

}
