using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscenes : MonoBehaviour
{
    public void EscenaSeguent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void EscenaAnerior()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void SortirJoc(){
        Application.Quit();
    }
}
