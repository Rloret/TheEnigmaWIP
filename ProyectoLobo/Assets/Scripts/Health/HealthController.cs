using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
{

    public Image Bar; //imagen de la barra de salud
    public float MaxHealth = 100f;
    public float CurrentHealth;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;

        InvokeRepeating ("decreaseHealth", 0f, 0.5f); //va restando vida en el tiempo, para comprobar
    }

    void decreaseHealth()
    {
        CurrentHealth -= 2f; //He puesto un magic number aquí para probar
        float calculateHealth = CurrentHealth / MaxHealth;
        setHealth(calculateHealth);
    }

    void setHealth(float myHealth)
    {
        Bar.fillAmount = myHealth;
    }
}