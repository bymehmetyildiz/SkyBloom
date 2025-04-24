using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] private Enemy boss;
    [SerializeField] private Transform target;
    [SerializeField] private float delta;

    private bool IsBossDead() => boss.stats.isDead;

    private void FixedUpdate()
    {
        if (IsBossDead())
            transform.position = Vector2.MoveTowards(transform.position, target.position, delta * Time.fixedDeltaTime);
    }
}
