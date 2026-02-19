using UnityEngine;

public class ShortCircuitLight : MonoBehaviour
{
    private Light targetLight;
    
    [Header("Settings")]
    public float minIntensity = 0.2f;   // Dimmest point
    public float maxIntensity = 1.5f;   // Brightest spark
    public float flickerSpeed = 0.05f;  // How fast it "twitches"
    
    [Header("Buzzing Sound (Optional)")]
    public AudioSource buzzAudio;       // Drag a buzzing sound here

    private float nextActionTime = 0f;

    void Start()
    {
        targetLight = GetComponent<Light>();
    }

    void Update()
    {
        // Only run if it's time for the next "twitch"
        if (Time.time > nextActionTime)
        {
            // Pick a random brightness
            float newIntensity = Random.Range(minIntensity, maxIntensity);
            targetLight.intensity = newIntensity;

            // Sync sound volume to brightness (optional)
            if (buzzAudio != null)
            {
                buzzAudio.volume = newIntensity / maxIntensity;
            }

            // Set the time for the next random jump (unpredictable timing)
            nextActionTime = Time.time + Random.Range(0.01f, flickerSpeed);
        }
    }

    // Function to kill the light (call this from your Fuse Box later!)
    public void TurnOffPower()
    {
        this.enabled = false;
        targetLight.intensity = 0;
        if (buzzAudio != null) buzzAudio.Stop();
    }
}