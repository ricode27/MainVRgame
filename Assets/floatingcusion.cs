using UnityEngine;

public class FloatOnWater : MonoBehaviour
{
    public Transform water;
    public float floatOffset = 0.05f;
    public float floatSpeed = 2f;

    void Update()
    {
        float targetY = water.position.y + floatOffset;
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * floatSpeed);
        transform.position = pos;
    }
}
