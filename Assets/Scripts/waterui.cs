using UnityEngine;
using UnityEngine.UI;

public class WaterLevelUI : MonoBehaviour
{
    public Transform waterSurface;   // the rising plane
    public float minY = 0f;          // start water height
    public float maxY = 4f;          // roof height (same as FloodRising)

    Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        float level = Mathf.InverseLerp(minY, maxY, waterSurface.position.y);
        img.fillAmount = level;
    }
}