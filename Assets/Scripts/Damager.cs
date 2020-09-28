using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    private Bar_controller defender;
    [SerializeField]
    private Bar_controller attacker;
    [SerializeField]
    private Ball_manager ball;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        defender.addReward(-1f);
        attacker.addReward(1f);
        ball.ResetWithDelay();
    }
}
