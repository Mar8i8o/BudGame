using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DestileriaController : MonoBehaviour
{
    public GameObject posCam;
    public GameObject camara;


    DestileriaManager destileriaManager;
    InventarioController inventarioController;
    NotificationController notificationController;
    public CamaraControll camaraControll;
    public HabitacionesControler habitacionesControler;

    public GameObject buttonDestileria;

    public GameObject focusDestileriaUI;

    public bool hasPlant;
    public bool hasCatalizador;

    public bool hasBella;
    public bool hasIntel;
    public bool hasFuerza;

    public bool focus;

    public float temperatura;
    public float presion;
    public float tiempo;

    public TextMeshProUGUI temperaturaIndicator;
    public Slider temperaturaSlider;
    public TextMeshProUGUI presionIndicator;
    public Slider presionSlider;
    public TextMeshProUGUI tiempoIndicator;
    public Slider tiempoSlider;
    public GameObject panelComputer;

    public float tiempoDestileria;

    public Sprite notBella;
    public Sprite notFuerza;
    public Sprite notIntel;
    public Sprite notRara;

    public GameObject butonStart;
    public GameObject barraTiempoParent;
    public Image barraTiempo;

    public Animator destileriaAnim;

    public GameObject notificacionRecoger;

    public bool tapaAbierta;

    void Start()
    {
        focusDestileriaUI.SetActive(false);
        destileriaManager = GameObject.Find("ItemsController").GetComponent<DestileriaManager>();
        inventarioController = GameObject.Find("GameManager").GetComponent<InventarioController>();
        notificationController = GameObject.Find("GameManager").GetComponent<NotificationController>();
        camaraControll = GameObject.Find("Main Camera").GetComponent<CamaraControll>();

        tapaAbierta = true;
        hasCatalizador = false;
        hasPlant = false;
        hasBella = false;
        hasFuerza = false;
        hasIntel = false;
        esperandoRecoger = false;

        panelComputer.SetActive(false);

    }

    void Update()
    {

        if(!hasPlant || !hasCatalizador) { tapaAbierta = true; }
        else { tapaAbierta = false; }

        if (focus)
        {
            camara.transform.position = Vector3.Lerp(camara.transform.position, posCam.transform.position, 7 * Time.deltaTime);
        }

        temperatura = temperaturaSlider.value;
        temperaturaIndicator.text = (int)temperatura + " C";

        presion = presionSlider.value;
        presionIndicator.text = (int)presion + " ATM";

        tiempo = tiempoSlider.value;
        tiempoIndicator.text = (int)tiempo + " S";

        notificacionRecoger.SetActive(esperandoRecoger);

        if(usandoDestileria)
        {
            tiempoDestileria += Time.deltaTime;

            barraTiempo.fillAmount = tiempoDestileria / tiempo;

            if(tiempoDestileria > tiempo) StopDestileria();
        }

        destileriaAnim.SetBool("Cooking", usandoDestileria);
        destileriaAnim.SetBool("EsperandoRecoger", esperandoRecoger);
        destileriaAnim.SetBool("IsIntel", hasIntel);
        destileriaAnim.SetBool("IsFuerza", hasFuerza);
        destileriaAnim.SetBool("IsBella", hasBella);
        destileriaAnim.SetBool("IsRara", !reaccionaBien);

        if(tapaAbierta)
        {
            destileriaAnim.SetBool("CerrarTapa", false);
        }
        else
        {
            destileriaAnim.SetBool("CerrarTapa", true);
        }
    }

    
    public void FocusDestileria()
    {
         GameObject[] destilerias = GameObject.FindGameObjectsWithTag("Destileria");

        for(int i = 0; i < destilerias.Length; i++)
        {
            destilerias[i].GetComponent<DestileriaController>().SalirFocus();
        }

         destileriaManager.usandoDestileria = true;
         habitacionesControler.freezeCam = true;
         focus = true;

         buttonDestileria.SetActive(false);
         focusDestileriaUI.SetActive(true);
         destileriaManager.destileriaActual = gameObject.GetComponent<DestileriaController>();
        camaraControll.freezeCam = true;


    }

    public void SalirFocus()
    {
        destileriaManager.usandoDestileria = false;
        buttonDestileria.SetActive(true);
        focusDestileriaUI.SetActive(false);

        camaraControll.freezeCam = false;
        habitacionesControler.freezeCam = false;
        focus = false;
    }

    public void InteractuarComputer()
    {
        panelComputer.SetActive(!panelComputer.activeSelf);
    }

    public bool reaccionaBien;
    public bool usandoDestileria;

    [HideInInspector]public int tiempoRequerido;
    [HideInInspector]public int presionRequerida;
    [HideInInspector] public int temperaturaRequerida;

    int aciertos;
    public void StartDestileria()
    {

        if (!tapaAbierta)
        {
            print("start destileria");
            usandoDestileria = true;
            tiempoDestileria = 0;

            presionSlider.interactable = false;
            temperaturaSlider.interactable = false;
            tiempoSlider.interactable = false;

            butonStart.SetActive(false);
            barraTiempoParent.SetActive(true);

            if (hasBella)
            {
                tiempoRequerido = 60;
                presionRequerida = 3;
                temperaturaRequerida = 90;
            }
            else if(hasIntel)
            {
                tiempoRequerido = 30;
                presionRequerida = 13;
                temperaturaRequerida = 180;
            }
            else if(hasFuerza)
            {
                tiempoRequerido = 10;
                presionRequerida = 20;
                temperaturaRequerida = 300;
            }

            aciertos = 0;

            if (VerificarTemperatura(temperatura, temperaturaRequerida)) aciertos++;
            if (VerificarTemperatura(tiempo, tiempoRequerido)) aciertos++;
            if (VerificarTemperatura(presion, presionRequerida)) aciertos++;

            if (aciertos == 3) { reaccionaBien = true; }
            else { reaccionaBien = false; }

        }
    }

    public bool esperandoRecoger;
    public void StopDestileria()
    {
        print("stop destileria");
        usandoDestileria = false;
        esperandoRecoger = true;
        tiempoDestileria = 0;
    }

    public void RecogerDestileria()
    {
        if (esperandoRecoger)
        {
            esperandoRecoger = false;
            presionSlider.interactable = true;
            temperaturaSlider.interactable = true;
            tiempoSlider.interactable = true;

            tapaAbierta = false;

            butonStart.SetActive(true);
            barraTiempoParent.SetActive(false);

            if (reaccionaBien)
            {
                if (hasBella)
                {
                    inventarioController.pocionesBelleza++;
                    notificationController.MostrarNotificacion(1, notBella, false);
                }
                else if (hasIntel)
                {
                    inventarioController.hojasIntel++;
                    notificationController.MostrarNotificacion(1, notIntel, false);
                }
                else if (hasFuerza)
                {
                    inventarioController.hojasFuerza++;
                    notificationController.MostrarNotificacion(1, notFuerza, false);
                }
            }
            else
            {
                inventarioController.pocionesRara++;
                notificationController.MostrarNotificacion(1, notRara, false);
            }

            hasPlant = false;
            hasCatalizador = false;
            hasBella = false;
            hasIntel = false;
            hasFuerza = false;
        }
    }


    public bool VerificarTemperatura(float temperaturaActual, float temperaturaDeseada)
    {
        // Calculamos la diferencia entre la temperatura actual y la deseada
        float diferencia = Mathf.Abs(temperaturaActual - temperaturaDeseada);

        print("diferencia" + diferencia);

        // Verificamos si la diferencia está dentro de la tolerancia aceptable
        if ((int)diferencia <= 5)
        {
            return true; // La reacción sale bien
        }
        else
        {
            return false; // La reacción sale mal
        }
    }
}
