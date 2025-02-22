using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongController : MonoBehaviour
{

    public string songName;
    public AudioClip song;
    public RadioController radioController;
    public TextMeshProUGUI nombreCancionTXT;
    public bool seleccionado;
    public GameObject fondoSeleccionado;
    public float musicMultiplicador;
    public int musicId;
    public bool laTienes;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("LaTienes" + gameObject.name, System.Convert.ToInt32(laTienes)) == 0) { laTienes = false; }
        else { laTienes = true; }
    }
    void Start()
    {
        seleccionado = radioController.audioSource.clip == song;
        fondoSeleccionado.SetActive(seleccionado);


    }

    void Update()
    {
        nombreCancionTXT.text = songName + ".MP3";

        seleccionado = radioController.audioSource.clip == song;
        fondoSeleccionado.SetActive(seleccionado);

        if (seleccionado)
        {
            radioController.musicId = musicId;
        }


        if(seleccionado) { radioController.musicMultiplicador = musicMultiplicador; }

        if (!laTienes)
        {
            gameObject.transform.SetAsLastSibling();
            nombreCancionTXT.gameObject.SetActive(false);
            fondoSeleccionado.gameObject.SetActive(false);
        }
        else
        {
            nombreCancionTXT.gameObject.SetActive(true);
        }

    }

    public void Guardar()
    {
        print("guardar");
        PlayerPrefs.SetInt("LaTienes" + gameObject.name, System.Convert.ToInt32(laTienes));
    }

    public void ReproducirCancion()
    {
        radioController.ReproducirCancion(song, musicId);
    }
}
