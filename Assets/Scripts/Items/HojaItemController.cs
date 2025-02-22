using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HojaItemController : MonoBehaviour
{
    public DestileriaController actualDestileria;
    public InventarioController inventario;

    public HojaStats hojaStatsActual;
    public bool usandoHoja;

    public bool puedeRecuperarHoja;

    DestileriaManager destileriaManager;

    void Start()
    {
        inventario = GameObject.Find("GameManager").GetComponent<InventarioController>();
        destileriaManager = GameObject.Find("ItemsController").GetComponent<DestileriaManager>();
    }

    void Update()
    {
        if(usandoHoja) 
        {

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
               if (puedeRecuperarHoja) 
               {
                   SoltarHoja();
                   hojaStatsActual.AnyadirHoja(1);
               }
               else
                {
                    //Invoke(nameof(SoltarHoja), 1);
                }

            }


        }
    }


    public void CojerHoja(HojaStats hojaStats)
    {
        hojaStatsActual = hojaStats;
        usandoHoja = true;
        //actualDestileria = hojaStats.destileriaController;
        actualDestileria = destileriaManager.destileriaActual;

        //hojaStatsActual.hojaUi.SetActive(false);
        hojaStatsActual.hojaPoint.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SoltarHoja()
    {
        usandoHoja = false;
        hojaStatsActual.hojaPoint.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
