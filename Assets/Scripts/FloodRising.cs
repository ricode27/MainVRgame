using UnityEngine;

public class FloodRising : MonoBehaviour
{
    [Header("Settings")]
    public float riseSpeed = 0.1f;    // How fast the water rises
    public float maxHeight = 4.0f;    // The height of your roof (Y value)
    public bool isRising = false;     // Check this box in Inspector to start the flood

    void Update()
    {
        // Only rise if the checkbox is ON and we haven't hit the roof yet
        if (isRising && transform.position.y < maxHeight)
        {
            // Move the water object UP
            transform.Translate(Vector3.up * riseSpeed * Time.deltaTime, Space.World);
        }
    }
}