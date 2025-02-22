using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioController : MonoBehaviour
{
    GuardarController guardarController;

    public GameObject pointFocusRadio;
    public bool focusRadio;
    public GameObject buttonFocusRadio;
    public GameObject botonesDesplazamiento;
    public GameObject camara;
    public CamaraControll camaraControll;
    public HabitacionesControler habitacionesControler;
    public AudioSource audioSource;
    public AudioSource radioAudioSource;
    public Image barraSonido;
    public GameObject butonPause;
    public GameObject butonPlay;

    public Button buttonPlayBut;
    public Button buttonRestartBut;

    public float musicMultiplicador;
    public bool playingMusic;
    public bool playingRadio;
    public bool radioTalking;
    public bool radioMusic;

    public AudioClip[] radioAudioTalking;

    public AudioClip[] cancionesRadio;

    public Animator radioAnim;
    public Animator musicAnim;

    public SongController[] canciones;
    public int cuantasCancionesTienes;

    public int musicId;

    public ParticleSystem particulasNegras;
    public ParticleSystem particulasCorcheas;

    void Start()
    {

        guardarController = GameObject.Find("GameManager").GetComponent<GuardarController>();

        radioTalking = true;

        if (!radioTalking)
        {
            int aleatorio = Random.Range(0, cancionesRadio.Length);
            radioAudioSource.clip = cancionesRadio[aleatorio];
            radioMusic = true;
            radioTalking = false;
            radioAudioSource.time = 0;
            radioAudioSource.Play();

            radioAudioSource.volume = 0.1f;
        }
        else
        {
            int aleatorio = Random.Range(0, radioAudioTalking.Length);
            radioAudioSource.clip = radioAudioTalking[aleatorio];
            radioMusic = false;
            radioTalking = true;
            radioAudioSource.time = 0;
            radioAudioSource.Play();

            radioAudioSource.volume = 0.5f;
        }
    }
    void Update()
    {

        if (guardarController.guardando)
        {
            for (int i = 0; i < canciones.Length; i++)
            {
                canciones[i].Guardar();
            }
        }

        if (focusRadio)
        {
            camara.transform.position = Vector3.Lerp(camara.transform.position, pointFocusRadio.transform.position, 5 * Time.deltaTime);
        }

        barraSonido.fillAmount = audioSource.time / audioSource.clip.length;

        butonPlay.SetActive(!audioSource.isPlaying);
        butonPause.SetActive(audioSource.isPlaying);

        playingMusic = audioSource.isPlaying;

        radioOnButton.SetActive(!playingRadio);
        radioOffButton.SetActive(playingRadio);

        if (playingMusic || (playingRadio && radioMusic))
        {
            //print("PlayParticulasMusica");
            if(!particulasCorcheas.isEmitting) { particulasCorcheas.Play(); }
            if(!particulasNegras.isEmitting) { particulasNegras.Play(); }
        }
        else
        {
            //print("StopParticulasMusica");
            particulasCorcheas.Stop();
            particulasNegras.Stop();
        }

        if(playingRadio) 
        {
            if (radioAudioSource.time >= radioAudioSource.clip.length - 0.1f) 
            {
                if(radioTalking) 
                {
                    int aleatorio = Random.Range(0, cancionesRadio.Length);
                    radioAudioSource.clip = cancionesRadio[aleatorio];
                    radioMusic = true;
                    radioTalking = false;
                    radioAudioSource.time = 0;
                    radioAudioSource.Play();

                    radioAudioSource.volume = 0.1f;
                }
                else 
                {
                    int aleatorio = Random.Range(0, radioAudioTalking.Length);
                    radioAudioSource.clip = radioAudioTalking[aleatorio];
                    radioMusic = false;
                    radioTalking = true;
                    radioAudioSource.time = 0;
                    radioAudioSource.Play();

                    radioAudioSource.volume = 0.5f;
                }
            }

            
        }

        if(cuantasCancionesTienes == 0)
        {
            cuantasCancionesTienes = 0;

            for(int i = 0; i < canciones.Length; i++) 
            {
                if (canciones[i].laTienes) { cuantasCancionesTienes++; }
            }

            buttonPlayBut.interactable = false;
            buttonRestartBut.interactable = false;
        }
        else
        {
            buttonPlayBut.interactable = true;
            buttonRestartBut.interactable = true;
        }

        radioAnim.SetBool("ON", playingRadio);
        musicAnim.SetBool("PlayingMusic", playingMusic);
        musicAnim.SetInteger("MusicId", musicId);

    }

    public void Restart()
    {
        audioSource.time = 0;
    }

    public void Pause()
    {
        audioSource.Pause();
        playingMusic = false;
    }

    public void Play()
    {
        audioSource.Play();
        playingMusic = true;

        musicAnim.SetTrigger("PlayMusic");

        if (playingRadio) 
        {
            radioAudioSource.mute = true;
            playingRadio = false;
        }
    }

    public GameObject radioOnButton;
    public GameObject radioOffButton;
    public void PlayRadio()
    {
        if(playingMusic)
        {
            Pause();
        }

        radioAudioSource.mute = false;
        playingRadio = true;
    }
    public void StopRadio()
    {
        radioAudioSource.mute = true;
        playingRadio = false;
    }

    public void FocusRadio()
    {
        buttonFocusRadio.SetActive(false);
        botonesDesplazamiento.SetActive(false);
        camaraControll.freezeCam = true;
        habitacionesControler.freezeCam = true;
        focusRadio = true;
    }

    public void StopFocusRadio()
    {
        buttonFocusRadio.SetActive(true);
        botonesDesplazamiento.SetActive(true);
        focusRadio = false;
        habitacionesControler.freezeCam = false;
    }

    public void ReproducirCancion(AudioClip cancion, int musicIdDisc)
    {
        musicId = musicIdDisc;
        audioSource.clip = cancion;
        audioSource.Play();
        musicAnim.SetTrigger("PlayMusic");

        if (playingRadio)
        {
            radioAudioSource.mute = true;
            playingRadio = false;
        }
    }
}
