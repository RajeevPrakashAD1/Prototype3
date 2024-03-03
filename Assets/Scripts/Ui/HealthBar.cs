using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.maxValue = 9900f;
        slider.value = 9900f;
    }

    public void SetHealth(int health)
    {
        //Debug.Log("setting health"+health);
        slider.value = health;
    }
}
