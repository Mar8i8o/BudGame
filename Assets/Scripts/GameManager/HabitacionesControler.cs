using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HabitacionesControler : MonoBehaviour
{
    public int indiceHabitacion;
    public GameObject camPosition;
    public GameObject[] habitaciones;
    PauseController pauseController;
    GameObject habitacionActual;
    public GameObject botonesDesplazamiento;
    public GameObject opcionesFocusPlant;
    DestileriaManager destileriaManager;
    public bool focusPlant;
    //public GameObject[] pointFocusPlant;
    //public PlantTrigger[] plantTrigger;
    public int plantsID;

    public PlantTrigger actualPlant;
    public TalkController talkController;

    public bool freezeCam;

    public float movementSpeed;

    private void Awake()
    {
        botonesDesplazamiento.SetActive(false);
    }

    void Start()
    {
        destileriaManager = GameObject.Find("ItemsController").GetComponent<DestileriaManager>();
        pauseController = gameObject.GetComponent<PauseController>();
        indiceHabitacion = 1;
        opcionesFocusPlant.SetActive(false);
        Invoke(nameof(ActivarBotonesDesplazamiento), 0.5f);
    }

    public void ActivarBotonesDesplazamiento()
    {
        botonesDesplazamiento.SetActive(true);
    }

    void Update()
    {
        if (!freezeCam)
        {
            if (!focusPlant)
            {
                habitacionActual = habitaciones[indiceHabitacion];
                camPosition.transform.position = Vector3.Lerp(camPosition.transform.position, habitacionActual.transform.position, movementSpeed * Time.deltaTime);
            }
            else
            {
                camPosition.transform.position = Vector3.Lerp(camPosition.transform.position, actualPlant.focusPoint.transform.position, movementSpeed * Time.deltaTime);
            }
        }

    }

    public void PasarHabitacionDerecha()
    {
        if (!focusPlant && !destileriaManager.usandoDestileria && !pauseController.pause)
        {
            if (indiceHabitacion < 2)
            {
                indiceHabitacion++;
            }
        }
    }
    public void PasarHabitacionIzquierda()
    {
        if (!focusPlant && !destileriaManager.usandoDestileria && !pauseController.pause)
        {
            if (indiceHabitacion > 0)
            {
                indiceHabitacion--;
            }
        }
    }

    public void FocusPlant()
    {
       
        if (talkController.dialogoActivo) return;
        actualPlant.focused = true;
        //plantsID = id;
        focusPlant = true;
        botonesDesplazamiento.SetActive(false);
        opcionesFocusPlant.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);

        /*
        if(actualPlant.plantStats.muerto && actualPlant.focused)
        {
            actualPlant.CortarPlanta();
        }
        else if(!actualPlant.plantStats.hayPlanta && actualPlant.focused)
        {
            actualPlant.plantStats.Plantar();
        }
        */


    }
    public void StopFocusPlant()
    {
        focusPlant = false;
        botonesDesplazamiento.SetActive(true);
        opcionesFocusPlant.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        actualPlant.focused = false;
    }
}
