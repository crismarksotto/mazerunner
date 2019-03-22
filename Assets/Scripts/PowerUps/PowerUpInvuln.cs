using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvuln : PowerUp
{
    public float invulnDurationSeconds = 5f;
    public GameObject invulnParticles;
    private GameObject invulnParticlesInstance;

    protected override void PowerUpPayload()     // Checklist item 1
    {
        base.PowerUpPayload();
        mazeRunner.SetInvulnerability(true);
        if (invulnParticles != null)
        {
            invulnParticlesInstance = Instantiate(invulnParticles, mazeRunner.gameObject.transform.position, mazeRunner.gameObject.transform.rotation, transform);
        }
    }

    protected override void PowerUpHasExpired()     // Checklist item 2
    {
        if (powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        mazeRunner.SetInvulnerability(false);
        if (invulnParticlesInstance != null)
        {
            Destroy(invulnParticlesInstance);
        }
        base.PowerUpHasExpired();
    }

    private void Update()                            // Checklist item 3
    {
        if (powerUpState == PowerUpState.IsCollected)
        {
            invulnDurationSeconds -= Time.deltaTime;
            if (invulnDurationSeconds < 0)
            {
                PowerUpHasExpired();
            }
        }
    }
    void OnTriggerEnter2D()
    {
        transform.parent.SendMessage("OnInvulnPicked", SendMessageOptions.DontRequireReceiver);
        GameObject.Destroy(gameObject);
    }
}