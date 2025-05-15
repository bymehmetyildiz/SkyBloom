using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource fireSFX;
    public string id;
    public bool isActivated;

    void Awake()
    {
        anim = GetComponent<Animator>();     
    }

    private void Update()
    {
        float distance = Vector2.Distance(PlayerManager.instance.player.transform.position, transform.position);

        if (distance <= fireSFX.maxDistance)
        {
            if (isActivated && !fireSFX.isPlaying)
            {
                fireSFX.Play();
            }
        }
        else
        {
            if (fireSFX.isPlaying)
            {
                fireSFX.Stop();
            }
        }
    }

    [ContextMenu("Generate Checkpoint Id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            ActivateCheckPoint();
        }
    }

    public void ActivateCheckPoint()
    {
        isActivated = true;
        anim.SetBool("Lit", isActivated);
    }
}
