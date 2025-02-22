using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    public GameObject posCam;
    public GameObject camara;

    public CamaraControll camaraControll;
    public HabitacionesControler habitacionesControler;

    public GameObject buttonOrdenador;

    public bool focus;
    void Start()
    {
        
    }

    void Update()
    {
        if(focus) 
        {
            camara.transform.position = Vector3.Lerp(camara.transform.position, posCam.transform.position, 7 * Time.deltaTime);
        }
    }

    public void FocusComputer()
    {
        camaraControll.freezeCam = true;
        habitacionesControler.freezeCam = true;
        focus = true;

        buttonOrdenador.SetActive(false);
        
    }

    public void SalirFocus()
    {
        buttonOrdenador.SetActive(true);

        camaraControll.freezeCam = false;
        habitacionesControler.freezeCam = false;
        focus = false;
    }
}
