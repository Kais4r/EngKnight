using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainMenu_lightcontrol : MonoBehaviour
{
    Light2D spotLight;
    public float duration = 5f; // The duration of each intensity change
    public float maxIntensity = 1f; // The maximum intensity value

    // Start is called before the first frame update
    void Start()
    {
        spotLight = transform.GetChild(0).GetComponent<Light2D>();
        StartCoroutine(ChangeLightIntensity());
    }

    private IEnumerator ChangeLightIntensity()
    {
        while (true)
        {
            float time = 0f;
            float initialIntensity = spotLight.intensity;
            float targetIntensity = maxIntensity;

            // Increase light intensity
            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;
                spotLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, t);
                yield return null;
            }

            // Wait for a moment at full intensity
            yield return new WaitForSeconds(0.3f);

            // Decrease light intensity
            time = 0f;
            targetIntensity = initialIntensity;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;
                spotLight.intensity = Mathf.Lerp(maxIntensity, targetIntensity, t);
                yield return null;
            }

            // Wait for a moment before repeating the loop
            yield return new WaitForSeconds(0.3f);
        }
    }
}