using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplegableController : MonoBehaviour
{
    public bool desplegado;

    public GameObject puntoDesplegado;

    public GameObject uiContigua;

    public bool tieneUIContigua;

    public bool vertical;

    Vector3 posInicial;
    void Start()
    {
        posInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(desplegado) 
        {
            if (!vertical) { transform.position = Vector3.Lerp(transform.position, new Vector3(puntoDesplegado.transform.position.x, transform.position.y, puntoDesplegado.transform.position.z), 12 * Time.deltaTime); }
            else { transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, puntoDesplegado.transform.position.y, puntoDesplegado.transform.position.z), 12 * Time.deltaTime); }
        }    
        else
        {
            transform.position = Vector3.Lerp(transform.position, posInicial, 12 * Time.deltaTime);
        }
    }

    public void Desplegar()
    {
        transform.SetAsLastSibling();
        desplegado = true;

        if (tieneUIContigua)
        {
            uiContigua.SetActive(false);

            DesplegableController desplegableControllerContiguo = uiContigua.GetComponent<DesplegableController>();

            uiContigua.GetComponent<DesplegableController>().transform.position = desplegableControllerContiguo.posInicial;
        }
    }

    public void DesplegarFast()
    {
        if (uiContigua.GetComponent<DesplegableController>().desplegado == true)
        {
            DesplegableController desplegableControllerContiguo = uiContigua.GetComponent<DesplegableController>();
            transform.SetAsLastSibling();
            desplegado = true;
            transform.position = new Vector3(puntoDesplegado.transform.position.x, transform.position.y, puntoDesplegado.transform.position.z);

            if (tieneUIContigua)
            {
                uiContigua.GetComponent<DesplegableController>().desplegado = false;
                uiContigua.GetComponent<DesplegableController>().transform.position = desplegableControllerContiguo.posInicial;
                uiContigua.SetActive(false);
            }
        }
        else
        {
            Desplegar();
        }
    }

    public void Plegar()
    {
        desplegado = false;
        uiContigua.SetActive(true);
    }
}
