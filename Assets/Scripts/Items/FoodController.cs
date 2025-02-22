using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodController : MonoBehaviour
{
    GuardarController guardarController;
    public HabitacionesControler habitacionesControler;

    public int comidaId;

    
    //public GameObject[] iconosComida;
    //public GameObject[] iconosComidaUI;

    //public float cuantoAlimenta;
    //public float cuantaFelicidad;

    public bool usandoComida;

    public ItemFoodStats[] consumible;
    void Start()
    {
        //SoltarComida();
        guardarController = GameObject.Find("GameManager").GetComponent<GuardarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (usandoComida) 
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                SoltarComida();
            }
        }

        if(guardarController)
        {
            for (int i = 0; i < consumible.Length; i++)
            {
                consumible[i].Guardar();
            }
        }
    }

    public ItemFoodStats actualFood;
    public void CojerComida(ItemFoodStats food)
    {
        usandoComida = true;
        actualFood = food;

        actualFood.foodIcoPoint.SetActive(true);
        if(!actualFood.consumible)actualFood.foodIcoUi.SetActive(false);

        actualFood.cantidad--;

        /*

        if (id == 0) //TIERRA
        {
            cuantaFelicidad = 2;
            cuantoAlimenta = 10;
        }

        for (int i = 0; i < iconosComida.Length; i++)
        {
            if (i == id)
            {
                iconosComida[i].SetActive(true);
                iconosComidaUI[i].SetActive(true);
            }
            else
            {
                iconosComida[i].SetActive(false);
                iconosComidaUI[i].SetActive(false);
            }
        }

        */

        //habitacionesControler.actualPlant.plantStats.comida += actualFood.cuantoAlimenta;
        //habitacionesControler.actualPlant.plantStats.felicidad += actualFood.cuantaFelicidad;

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SoltarComida()
    {
        /*
        for (int i = 0; i < iconosComida.Length; i++)
        {
           iconosComida[i].SetActive(false);
        }
        */

        actualFood.foodIcoPoint.SetActive(false);
        if(!actualFood.consumible)actualFood.foodIcoUi.SetActive(true);

        usandoComida = false;

        EventSystem.current.SetSelectedGameObject(null);

        print("soltarComida");

        if ((habitacionesControler.actualPlant.plantStats.fasePlant >= 2) || actualFood.isMedicina)
        {
            if (habitacionesControler.actualPlant.plantStats.enfermo && !actualFood.isMedicina)
            {
                habitacionesControler.actualPlant.Negar();
                actualFood.cantidad++;
            }
            else if (habitacionesControler.actualPlant.bocaAbierta)
            {
                if (habitacionesControler.actualPlant.plantStats.comida + actualFood.cuantoAlimenta > 100)
                {
                    habitacionesControler.actualPlant.Negar();
                    actualFood.cantidad++;
                }
                else
                {
                    print("comer");
                    //actualFood.cantidad--;

                    if(actualFood.posibilidadesDeEnfermar > 0)
                    {
                        int aleatorio = Random.Range(1, 100);

                        if(aleatorio < actualFood.posibilidadesDeEnfermar)
                        {
                            habitacionesControler.actualPlant.plantStats.enfermo = true;
                        }

                    }

                    habitacionesControler.actualPlant.plantStats.comida += actualFood.cuantoAlimenta;
                    habitacionesControler.actualPlant.plantStats.felicidad += actualFood.cuantaFelicidad;

                    if (actualFood.isMedicina) habitacionesControler.actualPlant.plantStats.enfermo = false;


                    habitacionesControler.actualPlant.Comer();
                }

            }
            else
            {
                actualFood.cantidad++;
                habitacionesControler.actualPlant.CerrarBoca();
            }
        }
        else
        {
            actualFood.cantidad++;
            habitacionesControler.actualPlant.CerrarBoca();
        }
    }
}
