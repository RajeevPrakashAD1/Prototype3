using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.maxValue = 100f;
        slider.value = 100f;
    }

    public void SetHealth(int health)
    {
        //Debug.Log("setting health");
        slider.value = health;
    }
}
