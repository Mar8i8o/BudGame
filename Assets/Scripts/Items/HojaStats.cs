using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HojaStats : MonoBehaviour
{
    public GameObject hojaPoint;
    public GameObject hojaUi;
    public InventarioController inventario;

    public HojaItemController hojaItemController;
    //public DestileriaController destileriaController;

    [HideInInspector]public NotificationController notificationController;
    public Sprite ico;

    public bool isPlant;
    public bool isHojaBella;
    public bool isHojaFuerza;
    public bool isHojaIntel;

    public bool isCatalizador;

    public float cantidad;
    public TextMeshProUGUI cantidadTXT;

    private void Awake()
    {
        ActualizarCantidades();
    }
    void Start()
    {
        inventario = GameObject.Find("GameManager").GetComponent<InventarioController>();
        notificationController = GameObject.Find("GameManager").GetComponent<NotificationController>();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarCantidades();
    }

    public void ActualizarCantidades()
    {
        if (isHojaBella) { cantidad = inventario.hojasBella; }
        else if (isHojaFuerza) { cantidad = inventario.hojasFuerza; }
        else if (isHojaIntel) { cantidad = inventario.hojasIntel; }
        else if (isCatalizador) { cantidad = inventario.catalizadores; }

        cantidadTXT.text = "" + cantidad;
    }

    public void CojerHoja()
    {
        if (cantidad > 0)
        {
            hojaItemController.CojerHoja(gameObject.GetComponent<HojaStats>());

            if (isHojaBella) { inventario.hojasBella--; }
            else if (isHojaFuerza) { inventario.hojasFuerza--; }
            else if (isHojaIntel) { inventario.hojasIntel--; }
            else if (isCatalizador) { inventario.catalizadores--; }
        }
    }

    public void AnyadirHoja(int cuantas)
    {
        if (isHojaBella) { inventario.hojasBella += cuantas; }
        else if (isHojaFuerza) { inventario.hojasFuerza += cuantas; }
        else if (isHojaIntel) { inventario.hojasIntel+= cuantas; }
        else if (isCatalizador) { inventario.catalizadores+= cuantas; }

        ActualizarCantidades();

    }

}
