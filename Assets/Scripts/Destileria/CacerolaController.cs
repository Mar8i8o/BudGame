using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacerolaController : MonoBehaviour
{
    public bool seleccionado;
    public DestileriaController destileriaController;

    public HojaItemController hojaItemController;


    void Start()
    {
        hojaItemController = GameObject.Find("ItemsController").GetComponent<HojaItemController>();
    }

    void Update()
    {
        if(hojaItemController.usandoHoja)
        {
            print("UsandoHoja");
            if (seleccionado)
            {
                print("Seleccionado");
                if (Input.GetKeyUp(KeyCode.Mouse0)) /////METER HOJA
                {
                    print("LevantarRaton");
                    //hojaItemController.hojaStatsActual.AnyadirHoja(-1);
                    if (destileriaController.focus)
                    { 
                        if (destileriaController.tapaAbierta)
                        {

                            if (hojaItemController.hojaStatsActual.isCatalizador && !destileriaController.hasCatalizador)
                            {
                                MeterHoja();
                                destileriaController.destileriaAnim.SetTrigger("CaeAlgo");
                                print("caeAlgo");
                                hojaItemController.SoltarHoja();
                            }
                            else if (hojaItemController.hojaStatsActual.isPlant && !destileriaController.hasPlant)
                            {
                                MeterHoja();
                                destileriaController.destileriaAnim.SetTrigger("CaeAlgo");
                                print("caeAlgo");
                                hojaItemController.SoltarHoja();
                            }
                            else
                            {
                                hojaItemController.hojaStatsActual.AnyadirHoja(1);
                                hojaItemController.SoltarHoja();
                            }
                        }
                        else
                        {
                            hojaItemController.hojaStatsActual.AnyadirHoja(1);
                            hojaItemController.SoltarHoja();
                        }
                    }
                }
            }
        }
    }

    public void MeterHoja()
    {
        hojaItemController.hojaStatsActual.notificationController.MostrarNotificacion(1, hojaItemController.hojaStatsActual.ico, true);

        if (hojaItemController.hojaStatsActual.isCatalizador)
        {
            destileriaController.hasCatalizador = true;
        }
        else
        {
            destileriaController.hasPlant = true;

            if (hojaItemController.hojaStatsActual.isHojaBella)
            {
                destileriaController.hasBella = true;
                destileriaController.hasIntel = false;
                destileriaController.hasFuerza = false;
            }
            else if (hojaItemController.hojaStatsActual.isHojaFuerza)
            {
                destileriaController.hasBella = false;
                destileriaController.hasIntel = false;
                destileriaController.hasFuerza = true;
            }
            else if (hojaItemController.hojaStatsActual.isHojaIntel)
            {
                destileriaController.hasBella = false;
                destileriaController.hasIntel = true;
                destileriaController.hasFuerza = false;
            }

        }

    }
    public void Seleccionar()
    {
        seleccionado = true;

        if(!destileriaController.hasPlant)hojaItemController.puedeRecuperarHoja = false;
    }

    public void Deseleccionar()
    {
        seleccionado = false;

        if (!destileriaController.hasPlant) hojaItemController.puedeRecuperarHoja = true;
    }
}
