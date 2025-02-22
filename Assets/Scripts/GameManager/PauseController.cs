using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    GuardarController guardarController;

    public bool pause;
    public bool menuInicio;
    public GameObject panelPausa;
    public GameObject buttonPausa;
    public GameObject audioCheck;
    AudioListener audioListener;
    public Animator pantallaNegroAnim;

    float volume;
    void Start()
    {
        pantallaNegroAnim.gameObject.GetComponent<Image>().raycastTarget = true;
        Invoke(nameof(DesactivarInteraccionPantallaNegra), 0.4f);
        pantallaNegroAnim.SetTrigger("SetOn");
        pantallaCompleta = Screen.fullScreen;
        volume = AudioListener.volume;

        if (!menuInicio)
        {
            audioListener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
            Reanudar();
            audioCheck.SetActive(true);
            audioActivo = true;

            guardarController = GameObject.Find("GameManager").GetComponent<GuardarController>();
        }


    }

    public void DesactivarInteraccionPantallaNegra()
    {
        pantallaNegroAnim.gameObject.GetComponent<Image>().raycastTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            if(pause) { Reanudar(); }
            else { Pausar(); }
        }

        if(!menuInicio) { checkPantallaCompleta.SetActive(pantallaCompleta); }

    }

    public bool pantallaCompleta;
    public GameObject checkPantallaCompleta;

    public void CambiarVentana()
    {
        Screen.fullScreen = !Screen.fullScreen;
        pantallaCompleta = !pantallaCompleta;
    }

    public void Pausar()
    {
        panelPausa.SetActive(true);
        buttonPausa.SetActive(false);
        pause = true;
    }

    public void Reanudar()
    {
        panelPausa.SetActive(false);
        buttonPausa.SetActive(true);
        pause = false;
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool audioActivo;
    public void ActivarAudio()
    {
        if (audioActivo)
        {
            audioCheck.SetActive(false);
            audioActivo = false;
            //audioListener.enabled = false;
            AudioListener.volume = 0;
        }
        else
        {
            audioCheck.SetActive(true);
            audioActivo = true;
            //audioListener.enabled = true;
            AudioListener.volume = volume;
        }
    }

    public void SalirJuego()
    {
        print("SalirJuego");
        Application.Quit();
    }

    public void SalirAMenu()
    {
        pantallaNegroAnim.gameObject.GetComponent<Image>().raycastTarget = true;
        pantallaNegroAnim.SetBool("Encendida", true);
        guardarController.guardando = true;
        Invoke(nameof(CambiarAMenu), 1);
    }

    public void CambiarAMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }

    public void StartGame()
    {
        print("CargarEscena");
        pantallaNegroAnim.gameObject.GetComponent<Image>().raycastTarget = true;
        pantallaNegroAnim.SetBool("Encendida", true);
        Invoke(nameof(CambiarAJuego), 1);
    }

    public void CambiarAJuego()
    {
        SceneManager.LoadScene("GameScene");
    }


}
