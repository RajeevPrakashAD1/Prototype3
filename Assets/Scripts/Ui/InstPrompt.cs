using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstPrompt : MonoBehaviour
{
    private void Start()
    {
        // Start the coroutine to disable the GameObject after 5 seconds
        StartCoroutine(DisableGameObjectAfterDelay(2f));
    }

    private IEnumerator DisableGameObjectAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the GameObject
        gameObject.SetActive(false);
    }
}
