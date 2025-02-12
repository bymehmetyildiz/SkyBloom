using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash Info")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashDur;
    private Material originalMat;
    private bool isFlashed;

    [Header("Ailment Colors")]
    [SerializeField] private Color[] ignitionColor;
    [SerializeField] private Color[] freezeColor;
    [SerializeField] private Color[] shockColor;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    //Damage Flash
    public IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;
        isFlashed = false;

        yield return new WaitForSeconds(flashDur);

        sr.color = currentColor;
        sr.material = originalMat;
        isFlashed = true;
    }

    //Stun
    public void StunFX()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    //Cancel Color Change
    public void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;

    }

    // Ignite
    public void IgnitionFxInvoke(float _seconds)
    {

        InvokeRepeating("IgnitionColorFX", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }

    private void IgnitionColorFX()
    {
        if (isFlashed)
        {
            if (sr.color != ignitionColor[0])
                sr.color = ignitionColor[0];
            else
                sr.color = ignitionColor[1];
        }
    }

    // Freeze
    public void FreezeFxInvoke(float _seconds)
    {
        InvokeRepeating("FreezeColorFX", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }

    private void FreezeColorFX()
    {
        if (isFlashed)
        {
            if (sr.color != freezeColor[0])
                sr.color = freezeColor[0];
            else
                sr.color = freezeColor[1];
        }
    }

    // Shock
    public void ShockFxInvoke(float _seconds)
    {
        InvokeRepeating("ShockColorFX", 0, 0.3f);
        Invoke("CancelColorChange", _seconds);
    }

    private void ShockColorFX()
    {
        if (isFlashed)
        {
            if (sr.color != shockColor[0])
                sr.color = shockColor[0];
            else
                sr.color = shockColor[1];
        }
    }

}
