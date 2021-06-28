using UnityEngine;
 
public class RipplePostProcessor : MonoBehaviour
{
    public const float LOWEST_AMOUNT_VALUE = 0.0001f;
    public Material RippleMaterial;
    public float MaxAmount = 5f;
 
    [Range(0, 1)]
    public float Friction = .9f;
 
    public float Amount = 0f;
    private bool _update = false;

    float nextTime = 3f;
 
    public void Ripple()
    {
        Amount = MaxAmount;
        Vector2 pos = new Vector2(Screen.width, Screen.height) / 2f;
        RippleMaterial.SetFloat("_CenterX", pos.x);
        RippleMaterial.SetFloat("_CenterY", pos.y);
        _update = true;
    }

    void Update()
    {
        if (Time.time >= nextTime)
        {
            Ripple();
            nextTime = Time.time + 2f;
        }
        if (_update)
        {
            RippleMaterial.SetFloat("_Amount", Amount);
            Amount *= Friction;
            if (Amount < LOWEST_AMOUNT_VALUE)
            {
                _update = false;
                Amount = 0;
                RippleMaterial.SetFloat("_Amount", Amount);
            }
        }
    }
 
    private void OnApplicationQuit()
    {
        RippleMaterial.SetFloat("_Amount", 0);
        RippleMaterial.SetFloat("_CenterX", 0);
        RippleMaterial.SetFloat("_CenterY", 0);
    }
}
