using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemFoodStats : MonoBehaviour
{
    GuardarController guardarController;

    public GameObject foodIcoUi;
    public GameObject foodIcoPoint;

    public float cuantoAlimenta;
    public float cuantaFelicidad;
    public int posibilidadesDeEnfermar;

    public bool isMedicina;

    public bool consumible;

    public int cantidad;
    public TextMeshProUGUI cantidadTXT;

    FoodController foodController;

    void Start()
    {
        foodIcoPoint.gameObject.SetActive(false);
        foodController = GameObject.Find("ItemsController").GetComponent<FoodController>();
        guardarController = GameObject.Find("GameManager").GetComponent<GuardarController>();

        cantidad = PlayerPrefs.GetInt("Cantidad" + gameObject.name, cantidad);

    }

    private void Update()
    {
        if(consumible) 
        {
            if (cantidad > 0)
            {
                cantidadTXT.text = "" + cantidad;
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.SetAsLastSibling();
                gameObject.SetActive(false);
            }
        }

    }

    public void Guardar()
    {
        PlayerPrefs.SetInt("Cantidad" + gameObject.name, cantidad);
    }

    public void CojerComida()
    {
        foodController.CojerComida(gameObject.GetComponent<ItemFoodStats>());
    }
    
}
