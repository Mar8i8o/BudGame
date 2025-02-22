using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventarioController : MonoBehaviour
{

    GuardarController guardarController;

    public float creditos;

    public int hojasBella;
    public int hojasFuerza;
    public int hojasIntel;

    public int pocionesFuerza;
    public int pocionesIntel;
    public int pocionesBelleza;

    public int pocionesRara;

    public int catalizadores;

    public TextMeshProUGUI creditsTXT;
    public TextMeshProUGUI pocionesFuerzaTXT;
    public TextMeshProUGUI pocionesIntelTXT;
    public TextMeshProUGUI pocionesBellezaTXT;
    public TextMeshProUGUI pocionesRaraTXT;



    private void Awake()
    {
        guardarController = GameObject.Find("GameManager").GetComponent<GuardarController>();
        hojasBella = PlayerPrefs.GetInt("HOJASBELLA", hojasBella);
        hojasFuerza = PlayerPrefs.GetInt("HOJASFUERZA", hojasFuerza);
        hojasIntel = PlayerPrefs.GetInt("HOJASINTEL", hojasIntel);
        pocionesBelleza = PlayerPrefs.GetInt("POCIONESBELLA", pocionesBelleza);
        pocionesFuerza = PlayerPrefs.GetInt("POCIONESFUERZA", pocionesFuerza);
        pocionesIntel = PlayerPrefs.GetInt("POCIONESINTEL", pocionesIntel);
        pocionesRara = PlayerPrefs.GetInt("POCIONESRARA", pocionesRara);
        catalizadores = PlayerPrefs.GetInt("CATALIZADORES", catalizadores);
        creditos = PlayerPrefs.GetFloat("CREDITOS", creditos);
    }
    void Start()
    {
        
    }

    void Update()
    {
        creditsTXT.text = "CREDITS: " + creditos +"c";

        pocionesFuerzaTXT.text = "x" + pocionesFuerza;
        pocionesBellezaTXT.text = "x" + pocionesBelleza;
        pocionesIntelTXT.text = "x" + pocionesIntel;

        pocionesRaraTXT.text = "x" + pocionesRara;


        if(Input.GetKeyDown(KeyCode.H)) 
        {
            hojasBella++;
            hojasFuerza++;
            hojasIntel++;
            creditos += 10;
        }

    }

    private void FixedUpdate()
    {

        if (guardarController.guardando)
        {

            PlayerPrefs.SetInt("HOJASBELLA", hojasBella);
            PlayerPrefs.SetInt("HOJASFUERZA", hojasFuerza);
            PlayerPrefs.SetInt("HOJASINTEL", hojasIntel);
            PlayerPrefs.SetInt("POCIONESBELLA", pocionesBelleza);
            PlayerPrefs.SetInt("POCIONESFUERZA", pocionesFuerza);
            PlayerPrefs.SetInt("POCIONESINTEL", pocionesIntel);
            PlayerPrefs.SetInt("POCIONESRARA", pocionesRara);
            PlayerPrefs.SetInt("CATALIZADORES", catalizadores);
            PlayerPrefs.SetFloat("CREDITOS", creditos);
        }
    }

}
