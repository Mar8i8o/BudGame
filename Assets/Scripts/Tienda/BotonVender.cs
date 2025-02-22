using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotonVender : MonoBehaviour
{

    InventarioController inventarioController;
    NotificationController notificationController;

    public string nombreItem;
    public string guiones;
    public float precio;
    public float cantidad;

    public TextMeshProUGUI txt;

    public bool isHojaBella;
    public bool isHojaIntel;
    public bool isHojaFuerza;

    public bool isBotellaBella;
    public bool isBotellaIntel;
    public bool isBotellaFuerza;
    public bool isBotellaRara;

    public Sprite monedaSprite;
    void Start()
    {
        inventarioController = GameObject.Find("GameManager").GetComponent<InventarioController>();
        notificationController = GameObject.Find("GameManager").GetComponent<NotificationController>();
    }

    void Update()
    {
        txt.text = nombreItem + " (" +  cantidad + ") " + guiones + precio + "c";
        
        if (isHojaBella)
        {
            cantidad = inventarioController.hojasBella;
        }
        else if (isHojaIntel) 
        {
            cantidad = inventarioController.hojasIntel;
        }
        else if (isHojaFuerza)
        {
            cantidad = inventarioController.hojasFuerza;
        }
        else if (isBotellaBella)
        {
            cantidad = inventarioController.pocionesBelleza;
        }
        else if (isBotellaFuerza)
        {
            cantidad = inventarioController.pocionesFuerza;
        }
        else if (isBotellaIntel)
        {
            cantidad = inventarioController.pocionesIntel;
        }
        else if (isBotellaRara)
        {
            cantidad = inventarioController.pocionesRara;
        }


        if (cantidad == 0)
        {
            txt.gameObject.SetActive(false);
            gameObject.transform.SetAsLastSibling();
        }
        else
        {
            txt.gameObject.SetActive(true);
        }
    }
    public void Vender()
    {

        if (cantidad > 0)
        {
            inventarioController.creditos += precio;

            if (isHojaBella)
            {
                inventarioController.hojasBella--;
            }
            else if (isHojaIntel)
            {
                inventarioController.hojasIntel--;
            }
            else if (isHojaFuerza)
            {
                inventarioController.hojasFuerza--;
            }
            else if (isBotellaBella)
            {
                inventarioController.pocionesBelleza--;
            }
            else if (isBotellaFuerza)
            {
                inventarioController.pocionesFuerza--;
            }
            else if (isBotellaIntel)
            {
                inventarioController.pocionesIntel--;
            }
            else if (isBotellaRara)
            {
                inventarioController.pocionesRara--;
            }
        }

        notificationController.MostrarNotificacion(precio, monedaSprite, false);
    }
}
