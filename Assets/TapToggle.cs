using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TapToggle : MonoBehaviour
{
    public ParticleSystem water;
    private bool isOn = true;

    public void ToggleTap()
    {
        if (isOn)
            water.Stop();
        else
            water.Play();

        isOn = !isOn;
    }
}
