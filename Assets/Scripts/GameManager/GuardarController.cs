using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarController : MonoBehaviour
{
    public bool guardando;

    public float tiempoGuardar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!guardando)
        {
            tiempoGuardar += Time.deltaTime;
            if (tiempoGuardar > 240)
            {
                Guardar();
            }
        }

        if(Input.GetKeyDown(KeyCode.G)) 
        {
            Guardar();
        }
    }

    public void Guardar()
    {
        guardando = true;
        tiempoGuardar = 0;
        Invoke(nameof(DejarDeGuardar), 1f);
    }

    public void DejarDeGuardar()
    {
        guardando = false;
    }
}
