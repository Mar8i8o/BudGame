using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestileriaManager : MonoBehaviour
{
    public DestileriaController destileriaActual;

    public bool usandoDestileria;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SalirFocus()
    {
        destileriaActual.SalirFocus();
    }
}
