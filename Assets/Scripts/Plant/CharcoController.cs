using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcoController : MonoBehaviour
{
    public Material material;
    RegaderaController regaderaController;

    public PlantTrigger plantTrigger;
    public float opacidad;


    void Start()
    {
        regaderaController = GameObject.Find("ItemsController").GetComponent<RegaderaController>();
    }

    // Update is called once per frame
    void Update()
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, opacidad);

        if (plantTrigger.focused)
        {
            if (regaderaController.regando)
            {
                if (opacidad < 0.8)
                {
                    opacidad += Time.deltaTime * 0.1f;
                }
            }
            else
            {
                if (opacidad > 0)
                {
                    opacidad -= Time.deltaTime * 0.1f;
                }
            }
        }
        else
        {
            if (opacidad > 0)
            {
                opacidad -= Time.deltaTime * 0.1f;
            }
        }

    }
}
