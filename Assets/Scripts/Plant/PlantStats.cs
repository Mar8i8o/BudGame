using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantStats : MonoBehaviour
{
    GuardarController guardarController;
    TimeController timeController;
    CaracolController caracolController;

    public float comida;
    public float agua;
    public float felicidad;
    public float salud;

    [HideInInspector]public double setComida;
    [HideInInspector]public double setAgua;
    [HideInInspector]public double setFelicidad;

    public bool enfermo;
    public bool triste;

    public GameObject pensamientoAgua;
    public GameObject pensamientoFelicidad;
    public GameObject pensamientoComida;
    public GameObject pensamientoEnfermo;
    public GameObject pensamientoMuerto;
    public GameObject pensamientoPlantar;
    public Animator plantAnim;

    public GameObject pensamientos;
    public GameObject[] posicionesPensamientos;

    public Image barraComida;
    public Image barraAgua;
    public Image barraFelicidad;

    public GameObject sanoStat;
    public GameObject enfermoStat;

    public GameObject caracolPos;
    GameObject caracol;
    public float distanciaCaracol;
    public bool esperandoCaracol;
    public bool muerto;
    public bool hayPlanta;
    public float tiempoVida;

    public int fasePlant;
    public float timeFase;

    public TMP_InputField inputField;
    public string nombrePlanta;

    public GameObject statComida;
    public GameObject statAgua;
    public GameObject statFelicidad;

    public ParticleSystem particulasCortar;
    public RadioController radioController;

    private void Awake()
    {
        timeController = GameObject.Find("GameManager").GetComponent<TimeController>();
        caracolController = GameObject.Find("GameManager").GetComponent<CaracolController>();
        caracol = GameObject.Find("Caracol");

        guardarController = GameObject.Find("GameManager").GetComponent<GuardarController>();
        radioController = GameObject.Find("Radio").GetComponent<RadioController>();
        //comida = 100;
        //agua = 100;
        //felicidad = 100;

        GetDatos();
    }
    void Start()
    {
        inputField.text = nombrePlanta;
        pensamientos.transform.position = posicionesPensamientos[fasePlant].transform.position;
        /*
        comida = comida - timeController.segundosPasados * 0.1f;
        agua = agua - timeController.segundosPasados * 0.1f;
        felicidad = agua - timeController.segundosPasados * 0.1f;
        salud = agua - timeController.segundosPasados * 0.1f;
        */

        setAgua = agua;
        setFelicidad = felicidad;
        setComida = comida;

        if(muerto) { Invoke(nameof(Morir),0.1f); }
        if(!hayPlanta) 
        {
            pensamientoEnfermo.SetActive(false);
            pensamientoAgua.SetActive(false);
            pensamientoComida.SetActive(false);
            pensamientoFelicidad.SetActive(false);
        }

        if (fasePlant == 0) { maxTimeFase = Random.Range(120, 130); }
        else if (fasePlant == 1) { maxTimeFase = Random.Range(180, 190); }
        else if (fasePlant == 2) { maxTimeFase = Random.Range(300, 310); ; }

    }

    public float offsetPensamiento;
    void Update()
    {

        pensamientoMuerto.SetActive(muerto);
        pensamientoPlantar.SetActive(!hayPlanta);

        if(guardarController.guardando) GuardarDatos();
        MoscasController();
        AjustarPensamientoMuerto();

        if (esperandoCaracol && muerto) { caracolController.CaracolSeVa(); esperandoCaracol = false; }

        if (muerto || !hayPlanta) { return; } 

        sanoStat.SetActive(!enfermo);
        enfermoStat.SetActive(enfermo);

        if (!muerto)
        {
            pensamientoEnfermo.SetActive(enfermo);
            pensamientoAgua.SetActive(agua < 20);
            pensamientoComida.SetActive(comida < 20);
            pensamientoFelicidad.SetActive(felicidad < 20);
        }

        if(pensamientoAgua.activeSelf || pensamientoComida.activeSelf || pensamientoFelicidad.activeSelf)
        {
            triste = true;
        }
        else
        {
            triste= false;
        }

        nombrePlanta = inputField.text;

        RandomEnfermar();
        ActualizarStats();
        ActualizarBarras();
        if(fasePlant == 3) TiempoVida();
        ControlarFase();

    }

    public void AjustarPensamientoMuerto()
    {

        if(fasePlant == 0) { offsetPensamiento = 0.21f; }
        if(fasePlant == 1) { offsetPensamiento = 0.18f; }
        if(fasePlant == 2) { offsetPensamiento = 0.21f; }
        if(fasePlant == 3) { offsetPensamiento = 0.40f; }

        pensamientoMuerto.transform.position = posicionesPensamientos[fasePlant].transform.position + new Vector3(0, offsetPensamiento, 0);

        //pensamientoMuerto.transform.position = Vector3.MoveTowards(pensamientoMuerto.transform.position, posicionesPensamientos[fasePlant].transform.position + new Vector3(0, offsetPensamiento, 0), 0.1f);
    }

    public bool evolucionando;

    public int maxTimeFase;

    float multipleFase;
    public void ControlarFase()
    {
        if (fasePlant < 3 && !evolucionando)
        {

            if(radioController.playingMusic || (radioController.playingRadio && radioController.radioMusic)) { multipleFase = 2.5f; }
            else { multipleFase = 1; }

            timeFase += Time.deltaTime * multipleFase;

            if (timeFase > maxTimeFase)
            {
                timeFase = 0;
                evolucionando = true;
                plantAnim.SetBool("Evolucionando", true);
                Invoke(nameof(FinishEvolucion), 2.30f);
            }
        }

        //pensamientos.transform.position = posicionesPensamientos[fasePlant].transform.position;

        pensamientos.transform.position = Vector3.MoveTowards(pensamientos.transform.position, posicionesPensamientos[fasePlant].transform.position, 4 * Time.deltaTime);

    }

    public void FinishEvolucion()
    {
        plantAnim.SetBool("Evolucionando", false);
        evolucionando = false;
        fasePlant++;
        timeFase = 0;

        if (fasePlant == 0) { maxTimeFase = Random.Range(120, 130); }
        else if (fasePlant == 1) { maxTimeFase = Random.Range(180, 190); }
        else if (fasePlant == 2) { maxTimeFase = Random.Range(300, 310); ; }
    }

    public GameObject semilla;
    public void Plantar()
    {
        GameObject instancia = Instantiate(semilla, posicionesPensamientos[0].transform.position + new Vector3(0,2,0), posicionesPensamientos[0].transform.rotation);

        SemillaController semillaController = instancia.GetComponent<SemillaController>();

        semillaController.plantStats = gameObject.GetComponent<PlantStats>();
        semillaController.objetivo = posicionesPensamientos[0];

    }

    public void FinalizarPlantar()
    {
        hayPlanta = true;
        muerto = false;
        comida = 50;
        setComida = 50;
        agua = 50;
        setAgua = 50;
        felicidad = 50;
        setFelicidad = 50;
        enfermo = false;
        tiempoVida = 0;
        fasePlant = 0;
        timeFase = 0;
    }

    public ParticleSystem particulasMoscas;
    public int cuantosPensamientos;
    public void MoscasController()
    {
        if (!muerto && hayPlanta)
        {

            cuantosPensamientos = 0;

            if (pensamientoAgua.activeSelf) { cuantosPensamientos++; }
            if (pensamientoComida.activeSelf) { cuantosPensamientos++; }
            if (pensamientoEnfermo.activeSelf) { cuantosPensamientos++; }
            if (pensamientoFelicidad.activeSelf) { cuantosPensamientos++; }

            //print("pensamientos: " + cuantosPensamientos);
            //print(particulasMoscas.isEmitting);

            if (cuantosPensamientos >= 2)
            {
                if (!particulasMoscas.isEmitting) particulasMoscas.Play();
            }
            else { particulasMoscas.Stop(); }

        }
        else if(muerto)
        {
            if (!particulasMoscas.isEmitting) particulasMoscas.Play();
        }
        else if(!hayPlanta)
        {
            particulasMoscas.Stop();
        }
    }
    public float tiempoEscuchandoMusica;
    public float tiempoSinEcucharMusica;
    public bool disfrutaDeLaMusica;
    public void ActualizarStats()
    {

        int multipleAgua;

        if (enfermo) { multipleAgua = 6; }
        else { multipleAgua = 1; }

        float speed = 10;

        if (setFelicidad < felicidad) { setFelicidad += Time.deltaTime * speed; }
        else if (setFelicidad > felicidad) { setFelicidad -= Time.deltaTime * speed; }

        if (setComida < comida) { setComida += Time.deltaTime * speed; }
        else if (setComida > comida) { setComida -= Time.deltaTime * speed; }

        if (setAgua < agua) { setAgua += Time.deltaTime * speed; }
        else if (setAgua > agua) { setAgua -= Time.deltaTime * speed; }

        /////

        if (fasePlant >= 2)
        {
            if (comida > 0) { comida -= Time.deltaTime * 0.2f * timeController.timeSpeed; }
            else { Morir(); }
        }

        if (agua > 0) { agua -= Time.deltaTime * 0.2f * multipleAgua * timeController.timeSpeed; }
        else { Morir(); }


        if (fasePlant == 3)
        {
            if (felicidad > 0) { felicidad -= Time.deltaTime * 0.2f * timeController.timeSpeed; }
            else { Morir(); };
        }

        if (agua > 100) agua = 100;
        if (felicidad > 100) felicidad = 100;
        if (comida > 100) comida = 100;

        if (radioController.playingMusic ||(radioController.playingRadio && radioController.radioMusic)) 
        {

            if (disfrutaDeLaMusica)
            {
                tiempoEscuchandoMusica += Time.deltaTime;

                if (tiempoEscuchandoMusica > 300)
                {
                    disfrutaDeLaMusica = false;
                    tiempoEscuchandoMusica = 0;
                    tiempoSinEcucharMusica = 0;
                }
            }
        }

        if(!disfrutaDeLaMusica) 
        {
            tiempoSinEcucharMusica += Time.deltaTime;

            if(tiempoSinEcucharMusica > 300)
            {
                disfrutaDeLaMusica = true;
                tiempoEscuchandoMusica = 0;
                tiempoSinEcucharMusica = 0;
            }
                
        }

        if (disfrutaDeLaMusica)
        {
            if (radioController.playingMusic)
            {
                if (felicidad < 100) felicidad += 0.2f * Time.deltaTime * radioController.musicMultiplicador;
            }
            else if (radioController.playingRadio)
            {
                if (felicidad < 100) felicidad += 0.2f * Time.deltaTime * 0.5f;
            }
        }

        statFelicidad.SetActive(fasePlant == 3);
        statComida.SetActive(fasePlant >= 2);

    }

    public void GetDatos()
    {
        comida = PlayerPrefs.GetFloat("Comida" + gameObject.name, (float)comida);
        agua = PlayerPrefs.GetFloat("Agua" + gameObject.name, (float)agua);
        felicidad = PlayerPrefs.GetFloat("Felicidad" + gameObject.name, (float)felicidad);
        salud = PlayerPrefs.GetFloat("Salud" + gameObject.name, (float)salud);
        salud = PlayerPrefs.GetFloat("TiempoVida" + gameObject.name, tiempoVida);
        nombrePlanta = PlayerPrefs.GetString("Nombre" + gameObject.name, inputField.text);

        fasePlant = PlayerPrefs.GetInt("Fase" + gameObject.name, fasePlant);
        timeFase = PlayerPrefs.GetFloat("TimeFase" + gameObject.name, timeFase);

        if (PlayerPrefs.GetInt("Enfermo" + gameObject.name, System.Convert.ToInt32(enfermo)) == 0) { enfermo = false; }
        else { enfermo = true; }

        if (PlayerPrefs.GetInt("HayPlanta" + gameObject.name, System.Convert.ToInt32(hayPlanta)) == 0) { hayPlanta = false; }
        else { hayPlanta = true; }

        if (PlayerPrefs.GetInt("PlantaMuerta" + gameObject.name, System.Convert.ToInt32(muerto)) == 0) { muerto = false; }
        else { muerto = true; }

    }
    public void GuardarDatos()
    {
        PlayerPrefs.SetFloat("Comida" + gameObject.name, (float)comida);
        PlayerPrefs.SetFloat("Agua" + gameObject.name, (float)agua);
        PlayerPrefs.SetFloat("Felicidad" + gameObject.name, (float)felicidad);
        PlayerPrefs.SetFloat("Salud" + gameObject.name, (float)salud);
        PlayerPrefs.SetInt("Enfermo" + gameObject.name, System.Convert.ToInt32(enfermo));
        PlayerPrefs.SetInt("HayPlanta" + gameObject.name, System.Convert.ToInt32(hayPlanta));
        PlayerPrefs.SetInt("PlantaMuerta" + gameObject.name, System.Convert.ToInt32(muerto));
        PlayerPrefs.SetFloat("TiempoVida" + gameObject.name, tiempoVida);

        PlayerPrefs.SetInt("Fase" + gameObject.name, fasePlant);
        PlayerPrefs.SetFloat("TimeFase" + gameObject.name, timeFase);

        PlayerPrefs.SetString("Nombre" + gameObject.name, nombrePlanta);

    }
    public void RandomEnfermar()
    {
        if (Time.frameCount % 1200 == 0)
        {

            int margenAleatorio = Random.Range(10, 100 + (int)agua + (int)comida + (int)felicidad);

            int aleatorio = Random.Range(1, margenAleatorio);

            //print("aleatorio: " + aleatorio + "margen: " + margenAleatorio);

            if (aleatorio == margenAleatorio - 1)
            {
                enfermo = true;
            }

        }
    }

    public void ActualizarBarras()
    {
        barraComida.fillAmount = (float)setComida / 100;
        barraAgua.fillAmount = (float)setAgua / 100;
        barraFelicidad.fillAmount = (float)setFelicidad / 100;
    }

    public void TiempoVida()
    {
        if (!esperandoCaracol)
        {
            tiempoVida += Time.deltaTime;

            if (tiempoVida > 1200 && !caracolController.caracolLlegando)
            {
                caracolController.LlamarCaracol(gameObject.GetComponent<PlantStats>());
                esperandoCaracol = true;
            }
        }
        else
        {
            distanciaCaracol = Vector3.Distance(caracol.transform.position, caracolPos.transform.position);

            if(distanciaCaracol <= 0.1f)
            {
                caracolController.CaracolSeVa();
                Morir();
            }

        }
    }

    public void Morir()
    {

        print("MORIR");

        muerto = true;
        plantAnim.SetTrigger("Dead");
        esperandoCaracol = false;
        caracolController.CaracolSeVa();

        pensamientoEnfermo.SetActive(false);
        pensamientoAgua.SetActive(false);
        pensamientoComida.SetActive(false);
        pensamientoFelicidad.SetActive(false);

        plantAnim.SetBool("Evolucionando", false);
        evolucionando = false;

    }

    [ContextMenu(itemName: "Delete_PlayerPrefs")]

    public void deletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All player prefs deleted");
    }
}
