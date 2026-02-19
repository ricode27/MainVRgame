using UnityEngine;

public class SimpleBuoyancy : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Water Settings")]
    public Transform waterObject; // CHANGE 1: Drag your WaterSurface here!
    public float waterLevel = 1.5f; // Fallback if no object is assigned
    
    public float buoyancyForce = 15f; 
    public float waterDrag = 2f;      
    public float waterAngularDrag = 1f; 

    private float airDrag;
    private float airAngularDrag;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        
        // Save original drag values
        // Note: In older Unity versions, use 'drag' instead of 'linearDamping'
        airDrag = rb.linearDamping;
        airAngularDrag = rb.angularDamping;
    }

    void FixedUpdate()
    {
        // CHANGE 2: Get the REAL height from the moving object
        float currentWaterLevel = waterLevel; 
        if (waterObject != null)
        {
            currentWaterLevel = waterObject.position.y;
        }

        // CHANGE 3: Use 'currentWaterLevel' instead of the old fixed variable
        float diff = transform.position.y - currentWaterLevel;

        if (diff < 0)
        {
            // Apply upward force
            rb.AddForceAtPosition(Vector3.up * buoyancyForce * Mathf.Abs(diff), transform.position, ForceMode.Acceleration);
            
            // Apply water resistance
            rb.linearDamping = waterDrag;
            rb.angularDamping = waterAngularDrag;
        }
        else
        {
            // Restore air resistance
            rb.linearDamping = airDrag;
            rb.angularDamping = airAngularDrag;
        }
    }
}