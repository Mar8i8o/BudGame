using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracolController : MonoBehaviour
{

    public GameObject caracol;
    public GameObject plantaObjetivo;
    public GameObject caracolSpawnPoint;
    public PlantStats plantStats;
    public bool caracolLlegando;

    void Update()
    {
        if (caracolLlegando)
        {
            caracol.SetActive(true);
            caracol.transform.position = Vector3.MoveTowards(caracol.transform.position, new Vector3(plantaObjetivo.transform.position.x, caracol.transform.position.y, plantaObjetivo.transform.position.z), 0.0002f);
        }
        else
        {
            caracol.SetActive(false);
        }
    }

    public void LlamarCaracol(PlantStats stats)
    {
        plantStats = stats;
        plantaObjetivo = plantStats.caracolPos;
        caracol.transform.position = caracolSpawnPoint.transform.position;
        caracolLlegando = true;
    }

    public void CaracolSeVa()
    {
        caracolLlegando = false;
        caracol.transform.position = caracolSpawnPoint.transform.position;
        //caracol.transform.position = caracolSpawnPoint.transform.position;
    }

}
