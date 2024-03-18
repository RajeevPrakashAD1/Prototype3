using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InstPrompt : MonoBehaviour
{
    public TextMeshProUGUI Tmp;
    private void Start()
    {
        // Start the coroutine to disable the GameObject after 5 seconds
       // StartCoroutine(DisableGameObjectAfterDelay(2f));
    }

    public void Invoke(string _text)
    {
        Tmp.text = _text;
        StartCoroutine(DisableGameObjectAfterDelay(0.2f));
    }
    private IEnumerator DisableGameObjectAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the GameObject
        gameObject.SetActive(false);
    }
}
