using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerStats : MonoBehaviour
{
    public GameObject panelStats;

    public bool statsActivos;

    public PlantStats plantStats;
    void Start()
    {
        statsActivos = false;
        panelStats.SetActive(false);
    }

    void Update()
    {
        
    }

    public void InteractuarStats()
    {
        if (plantStats.hayPlanta)
        {
            if (!statsActivos)
            {
                statsActivos = true;
                panelStats.SetActive(true);
            }
            else
            {
                statsActivos = false;
                panelStats.SetActive(false);
            }
        }
    }
}
