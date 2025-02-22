using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public Image imagenActual;
    public float cantidad;

    public TextMeshProUGUI cantidadTXT;

    public bool negativo;
    public bool max;
    void Start()
    {
        Invoke(nameof(Destroy), 5f);
    }
    void Update()
    {
        if (!max)
        {
            if (negativo)
            {
                cantidadTXT.text = "-" + cantidad;
            }
            else
            {
                cantidadTXT.text = "+" + cantidad;
            }
        }
        else
        {
            cantidadTXT.text = "MAX";
        }

        
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
