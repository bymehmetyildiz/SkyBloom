using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using Cinemachine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Player player;

    [Header("Flash Info")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashDur;
    private Material originalMat;
    private bool isFlashed;

    [Header("Ailment Colors")]
    [SerializeField] private Color[] ignitionColor;
    [SerializeField] private Color[] freezeColor;
    [SerializeField] private Color[] shockColor;

    [Header("Hit Effect")]
    [SerializeField] private GameObject hitFX;

    [Header("Screen Shake")]
    private CinemachineImpulseSource screenShake;
    [SerializeField] private float shakeMultiplier;
    [SerializeField] private Vector3 shakePower;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = PlayerManager.instance.player;
        screenShake = GetComponent<CinemachineImpulseSource>();
        originalMat = sr.material;
    }

    //ScreenShake
    public void ScreenShake()
    {
        screenShake.m_DefaultVelocity = new Vector3(shakePower.x * player.facingDir, shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
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

    //Hit Effect
    public void CreateHitFX(Transform _target)
    {
        GameObject newHitFX = Instantiate(hitFX, _target.position, Quaternion.identity);
        newHitFX.transform.localScale = new Vector3(newHitFX.transform.localScale.x * _target.GetComponent<Entity>().knockBackDir, newHitFX.transform.localScale.y);
        

        Destroy(newHitFX, 0.5f);
    }


}
