using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour
{
    public Material shockwaveMat;
    public Camera cam;

    public float speed = 0.1f;

    float maxMagnitude;

    void Start()
    {
        shockwaveMat.SetFloat("_Radius", -0.2f);

        maxMagnitude = shockwaveMat.GetFloat("_Magnitude");
    }

    public void Shockwave(Vector3 position)
    {
        Vector3 screenPosV3 = cam.WorldToScreenPoint(position);
        Vector2 screenPos = new Vector2(screenPosV3.x / cam.pixelWidth, screenPosV3.y / cam.pixelHeight);
        StopCoroutine("GrowShockwave");
        StartCoroutine(GrowShockwave(screenPos));
    }

    IEnumerator GrowShockwave(Vector2 screenPos)
    {
        shockwaveMat.SetVector("_FocalPoint", screenPos);

        float currentRadius = -0.2f;
        float currentMag = maxMagnitude;
        while (currentRadius < 2f)
        {
            currentRadius += speed * Time.deltaTime;
            currentMag = Mathf.Lerp(maxMagnitude, 0f, currentRadius);

            shockwaveMat.SetFloat("_Radius", currentRadius);
            shockwaveMat.SetFloat("_Magnitude", currentMag);

            yield return null;
        }
        shockwaveMat.SetFloat("_Magnitude", maxMagnitude);
        shockwaveMat.SetFloat("_Radius", -0.2f);
    }

    private void OnApplicationQuit()
    {
        shockwaveMat.SetFloat("_Radius", -0.2f);
        shockwaveMat.SetFloat("_Magnitude", maxMagnitude);
        shockwaveMat.SetVector("_FocalPoint", new Vector2(0.5f, 0.5f));
    }
}
