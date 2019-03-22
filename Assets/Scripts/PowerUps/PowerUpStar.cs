using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStar : PowerUp
{
    public int healthBonus = 20;

    protected override void PowerUpPayload()  // Checklist item 1
    {
        base.PowerUpPayload();

        // Payload is to give some health bonus
        mazeRunner.SetHealthAdjustment(healthBonus);      
    }

    void OnTriggerEnter2D()
    {
        transform.parent.SendMessage("OnStarPicked", SendMessageOptions.DontRequireReceiver);
        GameObject.Destroy(gameObject);
    }
}