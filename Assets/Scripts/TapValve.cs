using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Required for Haptics

public class TapValve : MonoBehaviour
{
    [Header("Wiring")]
    public FloodRising floodScript; 
    public Renderer valveRenderer;  
    public AudioSource tapAudio; 

    [Header("Settings")]
    public Color offColor = Color.green;
    public Color onColor = Color.red;
    public float cooldownTime = 1.0f; 

    private bool isFlowing = true;
    private float lastTouchTime = 0f; 

    void Start()
    {
        if (valveRenderer != null) valveRenderer.material.color = onColor;
        if (tapAudio != null && isFlowing) tapAudio.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. COOLDOWN CHECK
        if (Time.time - lastTouchTime < cooldownTime) return;

        // 2. TAG CHECK
        if (other.CompareTag("Player") || other.CompareTag("GameController")) 
        {
            // TOGGLE LOGIC
            ToggleWater();
            
            // HAPTICS LOGIC
            TriggerHapticFeedback(other);

            lastTouchTime = Time.time;
        }
    }

    void ToggleWater()
    {
        isFlowing = !isFlowing;

        if (isFlowing)
        {
            if (floodScript != null) floodScript.isRising = true;
            if (valveRenderer != null) valveRenderer.material.color = onColor;
            if (tapAudio != null) tapAudio.Play();
        }
        else
        {
            if (floodScript != null) floodScript.isRising = false;
            if (valveRenderer != null) valveRenderer.material.color = offColor;
            if (tapAudio != null) tapAudio.Stop();
        }
    }

    // --- ROBUST HAPTICS FINDER ---
    void TriggerHapticFeedback(Collider other)
    {
        // 1. Try to find the controller on the object itself
        ActionBasedController controller = other.GetComponent<ActionBasedController>();

        // 2. If not found, look in the PARENTS (up the hierarchy)
        if (controller == null)
        {
            controller = other.GetComponentInParent<ActionBasedController>();
        }

        // 3. If found, vibrate!
        if (controller != null)
        {
            // Intensity (0.0 to 1.0), Duration (seconds)
            controller.SendHapticImpulse(0.7f, 0.2f); 
            Debug.Log("Vibrating Controller: " + controller.name);
        }
        else
        {
            Debug.LogWarning("Touched by 'Player', but no ActionBasedController found on: " + other.name);
        }
    }
}