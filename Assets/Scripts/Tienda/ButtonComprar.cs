using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonComprar : MonoBehaviour
{
    InventarioController inventarioController;
    NotificationController notificationController;

    public string nombreItem;
    public string guiones;
    public float precio;

    public TextMeshProUGUI txt;

    public Sprite monedaSprite;

    public ItemFoodStats queCompra;
    public HojaStats queCompraHoja;
    public SongController queDisco;

    public bool isHoja;
    public bool isDisco;

    private void Awake()
    {
        if (isDisco)
        {
            if (queDisco.laTienes)
            {
                gameObject.SetActive(false);
                gameObject.transform.SetAsLastSibling();
            }
        }
    }
    void Start()
    {
        inventarioController = GameObject.Find("GameManager").GetComponent<InventarioController>();
        notificationController = GameObject.Find("GameManager").GetComponent<NotificationController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isDisco) { txt.text = nombreItem + guiones + " " + precio + "c"; }
        else if(isHoja) { txt.text = nombreItem + " (" + queCompraHoja.cantidad + ") " + guiones + " " + precio + "c";  }
        else { txt.text = nombreItem + " (" + queCompra.cantidad + ") " + guiones + " " + precio + "c"; }

        if (isDisco)
        {
            if (queDisco.laTienes)
            {
                gameObject.SetActive(false);
                gameObject.transform.SetAsLastSibling();
            }
        }

    }

    public void Comprar()
    {
        if (inventarioController.creditos >= precio)
        {
            if (isDisco)
            {
               queDisco.laTienes = true;
               queDisco.gameObject.SetActive(true);
               queDisco.gameObject.transform.SetAsFirstSibling();
               inventarioController.creditos -= precio;
               gameObject.transform.SetAsLastSibling();

               notificationController.MostrarNotificacion(1, monedaSprite, false);
               gameObject.SetActive(false);

               if (queDisco.laTienes)
               {
                   gameObject.SetActive(false);
                   gameObject.transform.SetAsLastSibling();
               }

            }
            else if (isHoja)
            {
                inventarioController.creditos -= precio;
                queCompraHoja.gameObject.SetActive(true);
                queCompraHoja.AnyadirHoja(1);

                notificationController.MostrarNotificacion(1, monedaSprite, false);

            }
            else
            {
                if (queCompra.cantidad < 16)
                {
                    queCompra.gameObject.SetActive(true);
                    queCompra.cantidad++;
                    inventarioController.creditos -= precio;

                    notificationController.MostrarNotificacion(1, monedaSprite, false);


                    if (queCompra.cantidad == 1) { queCompra.gameObject.transform.SetAsFirstSibling(); }

                }
                else
                {
                    notificationController.MostrarNotificacionMax(monedaSprite);
                }
            }
        }
    }
}
