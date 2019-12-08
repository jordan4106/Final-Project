using UnityEngine;

public class LightDemo : MonoBehaviour
{
    // Interpolate light color between two colors back and forth
    public float lightIntensity = 1.5f;
    private bool lightsOn;
    float duration = 1.0f;
    Color color0 = Color.red;
    Color color1 = Color.blue;

    Light lt;

    void Start()
    {
        lt = GetComponent<Light>();
        lightsOn = false;
    }

    void Update()
    {
        // set light color

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lightsOn = true;

        }

        if (lightsOn)
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            lt.color = Color.Lerp(color0, color1, t);
            lt.intensity = lightIntensity;
        }

    }
}