using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int numberOfPresents;
    public static bool gameOver;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TMP_Text kmText;

    public AudioSource musica;

    private float comptadorKM;
    private Movement mv;

    public Animator anim;

    public Animator renAnim;
    public Animator renAnim2;

    // Start is called before the first frame update
    void Start()
    {
        numberOfPresents = 0;
        gameOver = false;
        Time.timeScale = 1;
        comptadorKM = 0;
        mv = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Presents: " + numberOfPresents;
        Debug.Log(gameOver);
        if (gameOver){
            //Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            musica.Pause();
            anim.SetBool("haPerdut",true);
            renAnim.speed = 0f;
            renAnim2.speed = 0f;

        }
        comptadorKM += (15 *Time.deltaTime * mv.velocitatCarreteres)/1000;
        kmText.text = "KM: " + comptadorKM.ToString("F2");
    }

    public void carregarEscena(){

        SceneManager.LoadScene("Scenes/Game");

    }

    public void SortirJoc(){

        SceneManager.LoadScene(0);
    }
}