using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUp, IPlayerEvents
{
    [Range(1.0f, 4.0f)]
    public float speedMultiplier = 2.0f;

    protected override void PowerUpPayload()          // Checklist item 1
    {
        base.PowerUpPayload();
        mazeRunner.SetSpeedBoostOn(speedMultiplier);
    }

    protected override void PowerUpHasExpired()       // Checklist item 2
    {
        mazeRunner.SetSpeedBoostOff();
        base.PowerUpHasExpired();
    }

    void IPlayerEvents.OnPlayerHurt(int newHealth)
    {
        throw new System.NotImplementedException();
    }

    void IPlayerEvents.OnPlayerReachedExit(GameObject exit)
    {
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter2D()
    {
        transform.parent.SendMessage("OnSpeedPicked", SendMessageOptions.DontRequireReceiver);
        GameObject.Destroy(gameObject);
    }
}