using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.maxValue = 12900f;
        slider.value = 12900f;
    }

    public void SetHealth(int health)
    {
        //Debug.Log("setting health");
        slider.value = health;
    }
}
