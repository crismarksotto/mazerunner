using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class MazeRunner : MonoBehaviour, IMainGameEvents
{

    public float walkSpeed;
    public float rotationSpeed;

    //BrainThingie
    public int damageFromEnemyContact = 11;
    public AudioClip soundEffectEnemyContact;
    public GameObject particleContactPrefab;

    internal void SetHealthAdjustment(int healthBonus)
    {
        throw new NotImplementedException();
    }

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private GameObject particleContactInstance;
    private ParticleSystem particleSystemContactInstance;
    // particle system from above gameobject
    private int playerHitPoints;
    private float speedOriginal;
    private bool isPlayerInvulnerable;
    Vector2 direction = Vector2.zero;

    int targetX = 1;
    int targetY = 1;

    int currentX = 1;
    int currentY = 1;

    float currentAngle;
    float lastAngle;

    void Update()
    {

        bool targetReached = transform.position.x == targetX && transform.position.y == targetY;

        currentX = Mathf.FloorToInt(transform.position.x);
        currentY = Mathf.FloorToInt(transform.position.y);

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        float angle = 0;

        if (direction.x > 0)
        {
            angle = 270;

            if (MazeGenerator.instance.GetMazeGridCell(currentX + 1, currentY) && targetReached)
            {

                targetX = currentX + 1;
                targetY = currentY;
            }
        }
        else if (direction.x < 0)
        {
            angle = 90;

            if (MazeGenerator.instance.GetMazeGridCell(currentX - 1, currentY) && targetReached)
            {
                targetX = currentX - 1;
                targetY = currentY;
            }
        }
        else if (direction.y > 0)
        {
            angle = 0;

            if (MazeGenerator.instance.GetMazeGridCell(currentX, currentY + 1) && targetReached)
            {
                targetX = currentX;
                targetY = currentY + 1;
            }
        }
        else if (direction.y < 0)
        {
            angle = 180;

            if (MazeGenerator.instance.GetMazeGridCell(currentX, currentY - 1) && targetReached)
            {

                targetX = currentX;
                targetY = currentY - 1;
            }
        }
        else
        {
            angle = lastAngle;
        }



        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY), walkSpeed * Time.deltaTime);

        lastAngle = angle;
    }
    //MazeRunnerBrainThingie//
    void IMainGameEvents.OnGameWon()
    {
        // Remove from physics (no collisions, no movement) if game over
        rigidBody.simulated = false;
    }

    void IMainGameEvents.OnGameLost()
    {
        // Remove from physics (no collisions, no movement) if game over
        rigidBody.simulated = false;
        // We lose our yellow color
        spriteRenderer.color = Color.grey;
    }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        playerHitPoints = 100;
    }

    /// <summary>
    /// Send message to listenrs that player has lost some health
    /// </summary>
    private void SendPlayerHurtMessages()
    {
        // Send message to any listeners
        foreach (GameObject go in EventSystemListeners.main.listeners)
        {
            ExecuteEvents.Execute<IPlayerEvents>(go, null, (x, y) => x.OnPlayerHurt(playerHitPoints));
        }
    }
    public void SetSpeedBoostOn(float speedMultiplier)
    {
        speedOriginal = walkSpeed;
        walkSpeed *= speedMultiplier;
    }

    public void SetSpeedBoostOff()
    {
        walkSpeed = speedOriginal;
    }

    public void SetInvulnerability(bool isInvulnerabilityOn)
    {
        isPlayerInvulnerable = isInvulnerabilityOn;
    }
    private void SpawnCollisionParticles (Vector3 pos, Quaternion rot)
    {
        // Just one system that we keep re-using (if it is in use we don't spawn any particles)
        if (particleContactPrefab != null)
        {
            if (particleContactInstance == null)
            {
                // First time usage
                particleContactInstance = Instantiate (particleContactPrefab, pos, rot, transform);
                particleSystemContactInstance = particleContactInstance.GetComponent<ParticleSystem> ();
            } else
            {
                if (!particleSystemContactInstance.IsAlive ())
                {
                    // Reuse existing particle system
                    particleContactInstance.transform.SetPositionAndRotation (pos, rot);
                    particleSystemContactInstance.Play ();
                }
            }
        }
    }
}