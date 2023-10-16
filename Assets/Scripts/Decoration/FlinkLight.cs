using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlameEffect : MonoBehaviour
{
    public float speed = 1.5f;
    public float minIntensity = 0.1f;
    public float maxIntensity = 5.0f;
    public float scaleSpeed = 1.0f;
    public float scaleAmount = 0.1f;

    private Vector3 initialScale;
    private Light2D light2D;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // Simula o movimento da chama usando Perlin Noise
        float noise = Mathf.PerlinNoise(Time.time * speed, 0.0f);
        float flicker = Mathf.Lerp(minIntensity, maxIntensity, noise);
        light2D.intensity = flicker;

        // Anima o tamanho da chama para um movimento irregular
        float scale = Mathf.PingPong(Time.time * scaleSpeed, scaleAmount) + 1.0f;
        transform.localScale = initialScale * scale;
    }
}
