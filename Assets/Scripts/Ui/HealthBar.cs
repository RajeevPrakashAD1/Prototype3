using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.maxValue = 11300f;
        slider.value = 11300f;
    }

    public void SetHealth(int health)
    {
        //Debug.Log("setting health");
        slider.value = health;
    }
}
