using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HojasController : MonoBehaviour
{
    GuardarController guardarController;
    InventarioController inventarioController;

    public GameObject hojaActual;
    public TijerasController tijerasController;
    public PlantTrigger plantTrigger;
    public GameObject plantParent;

    public float tempFase;
    public int fase;

    public string type; //INTELIGENTE, BELLA, FUERTE

    public bool selected;

    public Animator hojasAnim;

    public Sprite spriteHojaInteligente;
    public Sprite spriteHojaBella;
    public Sprite spriteHojaFuerte;
    public Sprite spriteActual;

    public float speed;

    public bool hojasMarchitas;

    int random;
  
    void Start()
    {
        inventarioController = GameObject.Find("GameManager").GetComponent<InventarioController>();
        tijerasController = GameObject.Find("ItemsController").GetComponent<TijerasController>();

        guardarController = GameObject.Find("GameManager").GetComponent<GuardarController>();

        fase = PlayerPrefs.GetInt("Fase" + gameObject.name + plantParent.name, fase);
        type = PlayerPrefs.GetString("Type" + gameObject.name + plantParent.name, type);

        random = Random.Range(60, 80);

        if (fase == 3)
        {
            if (type == "")
            {
                int aleatorio = Random.Range(0, 101);

                if (aleatorio < 60) { type = "FUERZA"; }
                else if (aleatorio >= 60 && aleatorio < 70) { type = "BELLEZA"; }
                else if (aleatorio >= 70 && aleatorio <= 100) { type = "INTELIGENCIA"; }
            }
        }

        if (plantTrigger.plantStats.muerto || !plantTrigger.plantStats.hayPlanta) { fase = 0; tempFase = 0; }

    }

    bool marchitando;
    void Update()
    {

        if (guardarController.guardando)
        {
            PlayerPrefs.SetInt("Fase" + gameObject.name + plantParent.name, fase);
            PlayerPrefs.SetString("Type" + gameObject.name + plantParent.name, type);
        }

        if (fase == 0) { hojaActual.SetActive(false); }
        else { hojaActual.SetActive(true); }

        if (plantTrigger.plantStats.enfermo && !hojasMarchitas) { MarchitarHojas(); marchitando = true; }
        if (plantTrigger.plantStats.muerto && !hojasMarchitas) { MarchitarHojas(); marchitando = true; }

        if (!plantTrigger.plantStats.enfermo && hojasMarchitas && !marchitando) { hojasMarchitas = false; hojasAnim.SetBool("Muerte", false); }
        if (!plantTrigger.plantStats.muerto && hojasMarchitas && !marchitando) { hojasMarchitas = false; hojasAnim.SetBool("Muerte", false); }

        SetType();
        CortarHojasController();

        if(plantTrigger.plantStats.fasePlant < 3) { fase = 0; tempFase = 0; }

    }

    public void SetType()
    {

        if(plantTrigger.plantStats.muerto || !plantTrigger.plantStats.hayPlanta) { fase = 0; tempFase = 0; }

        if (plantTrigger.plantStats.fasePlant >= 3)
        {

            if (fase < 3 && !hojasMarchitas && plantTrigger.plantStats.hayPlanta && !plantTrigger.plantStats.muerto)
            {
                tempFase += Time.deltaTime * speed;

                if (tempFase > random)
                {
                    fase++;
                    tempFase = 0;

                    if (fase == 3)
                    {
                        int aleatorio = Random.Range(0, 100);

                        if (aleatorio < 60) { type = "FUERZA"; }
                        else if (aleatorio >= 60 && aleatorio < 70) { type = "BELLEZA"; }
                        else if (aleatorio >= 70 && aleatorio <= 100) { type = "INTELIGENCIA"; }
                    }
                }
            }

            ////////

            if (fase == 3)
            {
                if (type == "INTELIGENCIA")
                {
                    hojasAnim.SetBool("Intel", true);
                    hojasAnim.SetBool("Belleza", false);
                    hojasAnim.SetBool("Fuerza", false);

                    spriteActual = spriteHojaInteligente;
                }
                else if (type == "FUERZA")
                {
                    hojasAnim.SetBool("Intel", false);
                    hojasAnim.SetBool("Belleza", false);
                    hojasAnim.SetBool("Fuerza", true);

                    spriteActual = spriteHojaFuerte;
                }
                else if (type == "BELLEZA")
                {
                    hojasAnim.SetBool("Intel", false);
                    hojasAnim.SetBool("Belleza", true);
                    hojasAnim.SetBool("Fuerza", false);

                    spriteActual = spriteHojaBella;
                }
                hojasAnim.SetBool("Adult", true);
            }
            else
            {
                hojasAnim.SetBool("Intel", false);
                hojasAnim.SetBool("Belleza", false);
                hojasAnim.SetBool("Fuerza", false);

                hojasAnim.SetBool("Adult", false);
            }

        }
        else
        {
            fase = 0;
            tempFase = 0;
        }
    }

    public void MarchitarHojas()
    {
        hojasMarchitas = true;
        hojasAnim.SetBool("Muerte", true);
        tempFase = 0;
        //fase = 0;
        Invoke(nameof(DestruirHojas),1);
    }

    public void DestruirHojas()
    {
        tempFase = 0;
        fase = 0;
        marchitando = false;
    }


    public void CortarHojasController()
    {
        if (plantTrigger.focused)
        {
            if (selected && tijerasController.usandoTijeras)
            {
                if (fase == 3)
                {
                    tijerasController.hojaTrigger = true;
                    tijerasController.AbrirTijeras();
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {

                        if((type == "INTELIGENCIA" && inventarioController.hojasIntel >= 16) || (type == "FUERZA" && inventarioController.hojasFuerza >= 16) || (type == "BELLEZA" && inventarioController.hojasBella >= 16))
                        {
                            tijerasController.notificationController.MostrarNotificacionMax(spriteActual);
                            print("MaxHojas");
                            tijerasController.SoltarTijeras();
                            selected = false;
                        }
                        else
                        {
                            CortarHoja();

                            if (type == "INTELIGENCIA") { inventarioController.hojasIntel++; }
                            else if (type == "FUERZA") { inventarioController.hojasFuerza++; }
                            else if (type == "BELLEZA") { inventarioController.hojasBella++; }

                            tijerasController.notificationController.MostrarNotificacion(1, spriteActual, false);
                            tijerasController.SoltarTijeras();
                            selected = false;
                        }
                        
                    }
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        tijerasController.SoltarTijeras();
                        selected = false;
                    }
                }
            }

        }
    }

    public void Select()
    {
        if(plantTrigger.focused)tijerasController.hojaTrigger = true;
        selected = true;
    }

    public void Deselect()
    {
        tijerasController.hojaTrigger = false;
        selected = false;

        tijerasController.CerrarTijeras();

    }

    public void CortarHoja()
    {
        fase = 0;
        tempFase = 0;
        random = Random.Range(120, 200);
    }
}
