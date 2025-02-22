using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RegaderaController : MonoBehaviour
{
    public RectTransform canvasRectTransform; // Referencia al RectTransform del Canvas
    public RectTransform pointRectTransform; // Referencia al RectTransform del objeto que seguirá al ratón

    public bool usandoRegadera;

    public bool regando;

    public GameObject regadera;
    public GameObject butonRegadera;
    public Animator regaderaAnim;
    public ParticleSystem particulasRegadera;

    private void Start()
    {
        regadera.SetActive(usandoRegadera);
    }
    void Update()
    {
        // Obtener la posición del ratón en la pantalla
        Vector2 posicionRaton = Input.mousePosition;

        // Convertir la posición del ratón en la pantalla a una posición en el RectTransform del Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, posicionRaton, null, out Vector2 posicionEnCanvas);

        // Asignar la posición en el RectTransform del Canvas al objeto que seguirá al ratón
        pointRectTransform.localPosition = posicionEnCanvas;

        butonRegadera.SetActive(!usandoRegadera);

        if (usandoRegadera)
        {
            if(Input.GetKeyUp(KeyCode.Mouse0)) 
            {
                SoltarRegadera();
            }

            if (regando) 
            {
                regaderaAnim.SetBool("regando", true);
            }
            else
            {
                regaderaAnim.SetBool("regando", false);
            }
        }
        else
        {
            //regaderaAnim.SetBool("regando", false);
            if (particulasRegadera.isPlaying) 
            {
                regando = false;
                regaderaAnim.SetBool("regando", false);
                particulasRegadera.Stop();  
            }
        }

        //print(particulasRegadera.isPlaying);

    }

    public void CojerRegadera()
    {
        usandoRegadera = true;
        regadera.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SoltarRegadera()
    {
        usandoRegadera = false;
        regadera.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
