using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDataController : MonoBehaviour
{
    public GameObject quePanelAbre;
    public GameObject[] paneles;

    void Start()
    {
        AbrirPanel(quePanelAbre);
    }

    void Update()
    {
        
    }

    public void AbrirPanel(GameObject quePanel)
    {
        quePanelAbre = quePanel;

        for (int i = 0; i < paneles.Length; i++)
        {
            if(quePanelAbre != paneles[i]) { paneles[i].SetActive(false); }
        }

        quePanelAbre.SetActive(true);
    }
}
