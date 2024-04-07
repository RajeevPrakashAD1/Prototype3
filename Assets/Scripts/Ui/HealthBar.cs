using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.maxValue = 9999f;
        slider.value = 9999f;
    }

    public void SetHealth(int health)
    {
        //Debug.Log("setting health"+health);
        slider.value = health;
    }
}
