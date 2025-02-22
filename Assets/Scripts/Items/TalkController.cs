using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkController : MonoBehaviour
{
    public float textSpeed = 0.1f; // Velocidad de escritura del texto
    public string fullText; // El texto completo que deseas mostrar
    private string currentText = ""; // El texto que se está mostrando actualmente
    private int index = 0; // Índice que lleva la cuenta de cuántos caracteres se han mostrado

    public TextMeshProUGUI dialogueTXT;

    public TextMeshProUGUI opcion1;
    public TextMeshProUGUI opcion2;

    public GameObject panelDialogos;
    public GameObject panelOpciones;
    public GameObject opcionesFocusPlant;

    public HabitacionesControler habitacionesControler;

    public bool dialogoActivo;
    public bool hayOpciones;

    public DesplegableController[] desplegableControllers;

    void Start()
    {
        
    }

    private void Update()
    {
        if (dialogoActivo) 
        {
            if (!hayOpciones)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StopDialogue();
                }
            }

            if(habitacionesControler.actualPlant.plantStats.muerto)
            {
                StopDialogue();
            }

        }
    }

    public void StartDialogue()
    {
        if (habitacionesControler.actualPlant.plantStats.enfermo) /////ESTA ENFERMO
        {
            panelDialogos.SetActive(true);
            panelOpciones.SetActive(false);
            opcionesFocusPlant.SetActive(false);
            currentText = "";
            index = 0;
            fullText = "La planta esta demasiado debil para hablar";
            StartCoroutine(AnimateText());

            opcion1.text = "Charlar";
            opcion2.text = "Insultar";
            hayOpciones = true;
            dialogoActivo = true;

            Invoke(nameof(DesblockOpcions), 0.1f);
        }
        else if (habitacionesControler.actualPlant.plantStats.fasePlant < 3) ///ES PEQUEÑA
        {
            panelDialogos.SetActive(true);
            panelOpciones.SetActive(false);
            opcionesFocusPlant.SetActive(false);
            currentText = "";
            index = 0;
            fullText = "Esta planta aun no esta lista para hablar";
            StartCoroutine(AnimateText());

            opcion1.text = "Charlar";
            opcion2.text = "Insultar";
            hayOpciones = true;
            dialogoActivo = true;

            Invoke(nameof(DesblockOpcions), 0.1f);
        }
        else //////PUEDE HABLAR
        {
            panelDialogos.SetActive(true);
            panelOpciones.SetActive(true);
            opcionesFocusPlant.SetActive(false);
            currentText = "";
            index = 0;
            fullText = "La planta espera tu respuesta";
            StartCoroutine(AnimateText());

            opcion1.text = "Charlar";
            opcion2.text = "Insultar";
            hayOpciones = true;
            dialogoActivo = true;
        }
        
    }

    public void DesblockOpcions()
    {
        hayOpciones = false;    
    }

    public void StopDialogue()
    {
        panelDialogos.SetActive(false);
        opcionesFocusPlant.SetActive(true);
        dialogoActivo = false;

        /*
        for(int i = 0; i < desplegableControllers.Length; i++) 
        {
            if(desplegableControllers[i].desplegado) { desplegableControllers[i].Plegar(); }
        }
        */

        habitacionesControler.actualPlant.plantAnim.SetBool("Charlar", false);
        habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", false);
    }

    public string[] dialogosCaracol;
    public string[] dialogosHambre;
    public string[] dialogosSed;
    public string[] dialogosTriste;
    public string[] dialogosFeliz;
    public string[] dialogosDialogoDosEstadosNegativos;

    public string[] dialogoActual;

    public string[] dialogoInsulto;
    public string[] dialogoInsultoTriste;
    public string[] dialogoInsultoTristeHambre;
    public string[] dialogoInsultoTristeSed;
    public string[] dialogoInsulto2Estados;
    
    public void Charlar()
    {
        panelOpciones.SetActive(false);

        if(habitacionesControler.actualPlant.plantStats.esperandoCaracol)
        {
            dialogoActual = dialogosCaracol;
            habitacionesControler.actualPlant.plantAnim.SetBool("Charlar", true);
        }
        else if(habitacionesControler.actualPlant.plantStats.cuantosPensamientos >= 2)
        {
            dialogoActual = dialogosDialogoDosEstadosNegativos;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }
        else if (habitacionesControler.actualPlant.plantStats.pensamientoFelicidad.activeSelf)
        {
            dialogoActual = dialogosTriste;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }
        else if (habitacionesControler.actualPlant.plantStats.pensamientoComida.activeSelf)
        {
            dialogoActual = dialogosHambre;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }
        else if (habitacionesControler.actualPlant.plantStats.pensamientoAgua.activeSelf)
        {
            dialogoActual = dialogosSed;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }
        else
        {
            dialogoActual = dialogosFeliz;
            habitacionesControler.actualPlant.plantAnim.SetBool("Charlar", true);
        }
        
        int aleatorio = Random.Range(0, dialogoActual.Length);
        ContinuarDialogo(dialogoActual[aleatorio]);

        //habitacionesControler.actualPlant.plantAnim.SetBool("Charlar", true);
        habitacionesControler.actualPlant.plantStats.felicidad += 15;

        hayOpciones = false;
    }

    public void Insultar()
    {
        panelOpciones.SetActive(false);

        if (habitacionesControler.actualPlant.plantStats.cuantosPensamientos >= 2)
        {
            dialogoActual = dialogoInsulto2Estados;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }
        else if (habitacionesControler.actualPlant.plantStats.pensamientoFelicidad.activeSelf)
        {
            dialogoActual = dialogoInsultoTriste;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }
        else if (habitacionesControler.actualPlant.plantStats.pensamientoComida.activeSelf)
        {
            dialogoActual = dialogoInsultoTristeHambre;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }
        else if (habitacionesControler.actualPlant.plantStats.pensamientoAgua.activeSelf)
        {
            dialogoActual = dialogoInsultoTristeSed;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }
        else
        {
            dialogoActual = dialogoInsulto;
            habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        }


        int aleatorio = Random.Range(0, dialogoActual.Length);
        ContinuarDialogo(dialogoActual[aleatorio]);

        habitacionesControler.actualPlant.plantAnim.SetBool("Llorar", true);
        habitacionesControler.actualPlant.plantStats.felicidad -= 15;

        hayOpciones = false;
    }

    public void ContinuarDialogo(string texto)
    {
        index = 0;
        currentText = "";
        fullText = texto;
        StopAllCoroutines();
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        while (index < fullText.Length)
        {
            // Agrega el siguiente carácter al texto que se muestra actualmente
            currentText += fullText[index];
            dialogueTXT.text = currentText;

            // Incrementa el índice para avanzar al siguiente carácter
            index++;

            // Espera un tiempo antes de mostrar el siguiente carácter
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
