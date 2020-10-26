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
        if (defender.transform.position.x < attacker.transform.position.x)
            ball.left = true;
        else
            ball.left = false;
        defender.addReward(-0.005f*Vector2.SqrMagnitude(ball.getPos()-defender.getPos()));
        attacker.addReward(1f);
        defender.Increase_curr();        
        attacker.Increase_curr();
        ball.ResetWithDelay();
    }
}
