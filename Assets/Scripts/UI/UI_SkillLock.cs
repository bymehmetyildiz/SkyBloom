using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillLock : MonoBehaviour
{
    private Animator anim;
   

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void UnlockSkill() => anim.SetBool("Open", true);

    public void DeactivateLock() => gameObject.SetActive(false);
    
}
