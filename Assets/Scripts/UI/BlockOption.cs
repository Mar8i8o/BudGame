using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockOption : MonoBehaviour
{
    public bool block;

    public bool bloqueaCantidad;
    HojaStats hojaStats;

    Button button;
    Image image;

    [HideInInspector]public HabitacionesControler habitacionesControler;
    void Start()
    {
        habitacionesControler = GameObject.Find("GameManager").GetComponent<HabitacionesControler>();
        button = gameObject.GetComponent<Button>();
        image = gameObject.GetComponent<Image>();

        if (bloqueaCantidad) { hojaStats = gameObject.GetComponent<HojaStats>(); }
    }

    // Update is called once per frame
    void Update()
    {

        if (!bloqueaCantidad)
        {

            if (habitacionesControler.focusPlant)
            {
                if (habitacionesControler.actualPlant.plantStats.muerto || !habitacionesControler.actualPlant.plantStats.hayPlanta)
                {
                    block = true;
                }
                else
                {
                    block = false;
                }
            }

        }
        else
        {
            if(hojaStats.cantidad == 0)
            {
                block = true;
            }
            else
            {
                block= false;
            }
        }

        if (block) 
        {
            button.interactable = false;
            image.color = Color.black;
        }
        else
        {
            button.interactable = true;
            image.color = Color.white;
        }
    }
}
