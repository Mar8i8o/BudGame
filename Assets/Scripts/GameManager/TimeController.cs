using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public double totalSegundos;

    public float timeSpeed;

    public int horas;
    public int minutos;
    public int segundos;

    public int dia;
    public int mes;

    public int diaDelYear;

    public TextMeshProUGUI horaTXT;

    public Gradient gradient;
    public Gradient gradientPantalla;
    public SpriteRenderer sprite;
    public Image imagenPantalla;
    void Awake()
    {
        System.DateTime horaActual = System.DateTime.Now;
        totalSegundos = ConvertirHoraASegundos(horaActual);

        dia = horaActual.Day;
        mes = horaActual.Month;
        diaDelYear = horaActual.DayOfYear;

        TiempoPasado();


    }

    // Update is called once per frame
    void Update()
    {
        totalSegundos += Time.deltaTime * timeSpeed;
        ConvertirSegundosAFormatoHora();
        DetectarCicloDiaNoche();

        float normalizedTime = (float)totalSegundos / 86400f; // 86400 segundos en un día

        // Evalúa el color en el gradiente utilizando el valor normalizado
        Color lerpedColor = gradient.Evaluate(normalizedTime);
        Color lerpedColorPantalla = gradientPantalla.Evaluate(normalizedTime);

        // Aplica el color al objeto deseado
        sprite.color = lerpedColor;
        imagenPantalla.color = lerpedColorPantalla;

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            AcelerarTiempo();
        }

        timeNormalUI.SetActive(!tiempoAcelerado);
        timeFastUI.SetActive(tiempoAcelerado);
    }

    private void FixedUpdate()
    {

            PlayerPrefs.SetFloat("TOTALSEGUNDOS", (float)totalSegundos);
            PlayerPrefs.SetFloat("DIADELANO", diaDelYear);

    }

    public double segundosPasados;
    public void TiempoPasado()
    {
        float ultimosSegundos;
        float ultimosDia;
        ultimosSegundos = PlayerPrefs.GetFloat("TOTALSEGUNDOS", (float)totalSegundos);
        ultimosDia = PlayerPrefs.GetFloat("DIADELANO", diaDelYear);


        double totalsegundosMax = (diaDelYear * 8644) + totalSegundos;
        double totalsegundosMaxPasados = (ultimosDia * 8644) + ultimosSegundos;
        segundosPasados = totalsegundosMax - totalsegundosMaxPasados;

    }

    public void ConvertirSegundosAFormatoHora()
    {
        // Calcula las horas, minutos y segundos
        horas = Mathf.FloorToInt((float)(totalSegundos / 3600));
        minutos = Mathf.FloorToInt((float)((totalSegundos % 3600) / 60));
        segundos = Mathf.FloorToInt((float)(totalSegundos % 60));

        // Formatea la cadena en el formato de hora HH:MM:SS
        horaTXT.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);

        if (horas >= 24)
        {
            totalSegundos = 0;
        }
    }

    public bool esDeDia;
    public GameObject nocheIco;
    public GameObject diaIco;
    public void DetectarCicloDiaNoche()
    {
        if(horas >= 6 &&  horas <= 21)
        {
            esDeDia = true;
        }
        else
        {
            esDeDia = false;
        }

        diaIco.SetActive(esDeDia);
        nocheIco.SetActive(!esDeDia);
    }

    int ConvertirHoraASegundos(System.DateTime hora)
    {
        // Calcula los segundos totales
        int segundos = hora.Hour * 3600 + hora.Minute * 60 + hora.Second;

        return segundos;
    }

    [ContextMenu(itemName: "Delete_PlayerPrefs")]

    public void deletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All player prefs deleted");
    }

    public bool tiempoAcelerado;

    public GameObject timeNormalUI;
    public GameObject timeFastUI;
    public void AcelerarTiempo()
    {
        if(tiempoAcelerado)
        {
            Time.timeScale = 1;
            tiempoAcelerado = false;
        }
        else
        {
            Time.timeScale = 4;
            tiempoAcelerado = true;
        }
    }
}
