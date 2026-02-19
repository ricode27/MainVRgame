using UnityEngine;

public class WaterRise : MonoBehaviour
{
    public float riseSpeed = 0.02f;
    public float maxHeight = 1.2f;

    void Update()
    {
        if (transform.position.y < maxHeight)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }
}
