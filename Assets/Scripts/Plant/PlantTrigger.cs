using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrigger : MonoBehaviour
{
    HabitacionesControler habitacionesControler;

    public GameObject focusPoint;
    public bool selected;
    public RegaderaController regaderaController;
    public TijerasController tjerasController;
    public FoodController foodController;
    public PlantStats plantStats;
    public bool focused;
    public bool bocaAbierta;

    public Animator plantAnim;

    public RuntimeAnimatorController controllerAdult;
    public RuntimeAnimatorController controllerFase0;
    public RuntimeAnimatorController controllerFase1;
    public RuntimeAnimatorController controllerFase2;

    public MeshRenderer plantMeshRenderer;
    void Start()
    {
        habitacionesControler = GameObject.Find("GameManager").GetComponent<HabitacionesControler>();
        regaderaController = GameObject.Find("ItemsController").GetComponent<RegaderaController>();
        tjerasController = GameObject.Find("ItemsController").GetComponent<TijerasController>();
        foodController = GameObject.Find("ItemsController").GetComponent<FoodController>();

        regaderaController.particulasRegadera.Stop();
    }

    void Update()
    {
        //plantMeshRenderer.gameObject.SetActive(hayPlanta);
        plantMeshRenderer.enabled = plantStats.hayPlanta;

        RegaderaController();
        ControlFaseVisual();

        plantAnim.SetBool("Triste", plantStats.triste);

        if (plantStats.enfermo) { plantAnim.SetBool("Enfermo", true); }
        else { plantAnim.SetBool("Enfermo", false); }       

    }

    public void ControlFaseVisual()
    {
        if (plantStats.hayPlanta)
        {
            if (plantStats.fasePlant == 0) //SEMILLA
            {
                plantAnim.runtimeAnimatorController = controllerFase0;
            }
            else if (plantStats.fasePlant == 1) //BROTE
            {
                plantAnim.runtimeAnimatorController = controllerFase1;
            }
            else if (plantStats.fasePlant == 2) // CAPULLO
            {
                plantAnim.runtimeAnimatorController = controllerFase2;
            }
            else if (plantStats.fasePlant == 3) // ADULTO
            {
                plantAnim.runtimeAnimatorController = controllerAdult;
            }
        }
        else
        {
            plantAnim.runtimeAnimatorController = controllerFase0;
        }
    }

    public void FocusPlant()
    {

        if (focused && plantStats.muerto) { CortarPlanta(); }
        else if (focused && !plantStats.hayPlanta) { plantStats.Plantar(); }

        if (habitacionesControler.actualPlant != null) habitacionesControler.StopFocusPlant();
        habitacionesControler.actualPlant = gameObject.GetComponent<PlantTrigger>();
        habitacionesControler.FocusPlant();
    }
    public void RegaderaController()
    {
        if (focused)
        {
            if (regaderaController.regando)
            {
                plantStats.agua += Time.deltaTime * 10;
                plantAnim.SetBool("Ducha", true);
            }
            else
            {
                plantAnim.SetBool("Ducha", false);
            }
        }
        else
        {
            plantAnim.SetBool("Ducha", false);
        }
    }

    public void CortarPlanta()
    {
        plantStats.hayPlanta = false;
        plantStats.muerto = false;
        plantStats.pensamientoMuerto.SetActive(false);
        plantStats.particulasCortar.Play();
     
    }

    public void Select()
    {
        selected = true;

        if (focused)
        {
            if (regaderaController.usandoRegadera)
            {
                regaderaController.regando = true;
                Invoke(nameof(StartParticles), 0.1f);
            }
            else if (foodController.usandoComida)
            {
                print("abrirBoca");
                plantAnim.SetBool("AbrirBoca", true);
                bocaAbierta = true;
            }
        }

    }    
    public void Deselect()
    {
        selected = false;

        if (regaderaController.usandoRegadera)
        {
            regaderaController.regando = false;
            CancelInvoke(nameof(StartParticles));
            regaderaController.particulasRegadera.Stop();
        }
        else if (foodController.usandoComida)
        {
            if (!negando)
            {
                plantAnim.SetBool("AbrirBoca", false);
                bocaAbierta = false;
            }
        }
    }

    public void CerrarBoca()
    {
        bocaAbierta = false;
        print("bocaCerrada");
        plantAnim.SetBool("AbrirBoca", false);
    }

    public void Comer()
    {
        bocaAbierta = false;
        print("bocaCerrada");
        plantAnim.SetTrigger("Comer");

        Invoke(nameof(CerrarBoca),0.1f);
    }

    public bool negando;
    public void Negar()
    {
        bocaAbierta = false;
        negando = true;
        plantAnim.SetTrigger("Negar");

        Invoke(nameof(CerrarBoca), 0.1f);
    }

    public void StartParticles()
    {
        regaderaController.particulasRegadera.Play();
    }

}
