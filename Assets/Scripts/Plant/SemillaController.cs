using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemillaController : MonoBehaviour
{
    public PlantStats plantStats;
    public GameObject objetivo;

    public float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(transform.position, objetivo.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, objetivo.transform.position, 6 * Time.deltaTime);

        if(distance <= 0)
        {
            plantStats.FinalizarPlantar();
            Destroy(gameObject);
        }
    }
}
