using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MazeDirectives : MonoBehaviour
{

    public int keysToFind;
    public int powerUpSpeedAvailable;
    public int powerUpInvulnAvailable;
    public int powerUpStarAvailable;

    public Text keysValueText;

    public MazeGoal mazeGoalPrefab;
    public MazeKey mazeKeyPrefab;

    //BrainThingie
    public PowerUpStar powerUpStarPrefab;
    public PowerUpSpeed powerUpSpeedPrefab;
    public PowerUpInvuln powerUpInvulnPrefab;

    MazeGoal mazeGoal;

    int foundKeys;

    List<Vector3> mazeKeyPositions;
    List<Vector3> powerUpStarPositions;
    List<Vector3> powerUpSpeedPositions;
    List<Vector3> powerUpInvulnPositions;

    void Awake()
    {
        MazeGenerator.OnMazeReady += StartDirectives;
    }

    void Start()
    {
        SetKeyValueText();
    }

    void StartDirectives()
    {
        mazeGoal = Instantiate(mazeGoalPrefab, MazeGenerator.instance.mazeGoalPosition, Quaternion.identity) as MazeGoal;
        mazeGoal.transform.SetParent(transform);

        mazeKeyPositions = MazeGenerator.instance.GetRandomFloorPositions(keysToFind);

        powerUpStarPositions = MazeGenerator.instance.GetRandomFloorPositions(powerUpStarAvailable);
        powerUpSpeedPositions = MazeGenerator.instance.GetRandomFloorPositions(powerUpSpeedAvailable);
        powerUpInvulnPositions = MazeGenerator.instance.GetRandomFloorPositions(powerUpInvulnAvailable);

        for (int i = 0; i < mazeKeyPositions.Count; i++)
        {
            MazeKey mazeKey = Instantiate(mazeKeyPrefab, mazeKeyPositions[i], Quaternion.identity) as MazeKey;
            mazeKey.transform.SetParent(transform);

            PowerUpStar powerUpStar = Instantiate(powerUpStarPrefab, powerUpStarPositions[i], Quaternion.identity) as PowerUpStar;
            powerUpStar.transform.SetParent(transform);

            PowerUpSpeed powerUpSpeed = Instantiate(powerUpSpeedPrefab, powerUpSpeedPositions[i], Quaternion.identity) as PowerUpSpeed;
            powerUpSpeed.transform.SetParent(transform);

            PowerUpInvuln powerUpInvuln = Instantiate(powerUpInvulnPrefab, powerUpInvulnPositions[i], Quaternion.identity) as PowerUpInvuln;
            powerUpInvuln.transform.SetParent(transform);
        }

        for (int i = 0; i < powerUpStarPositions.Count; i++)
        {
            PowerUpStar powerUpStar = Instantiate(powerUpStarPrefab, powerUpStarPositions[i], Quaternion.identity) as PowerUpStar;
            powerUpStar.transform.SetParent(transform);
        }
        for (int i = 0; i < powerUpSpeedPositions.Count; i++)
        {
            PowerUpSpeed powerUpSpeed = Instantiate(powerUpSpeedPrefab, powerUpSpeedPositions[i], Quaternion.identity) as PowerUpSpeed;
            powerUpSpeed.transform.SetParent(transform);
        }
        for (int i = 0; i < powerUpInvulnPositions.Count; i++)
        {
            PowerUpInvuln powerUpInvuln = Instantiate(powerUpInvulnPrefab, powerUpInvulnPositions[i], Quaternion.identity) as PowerUpInvuln;
            powerUpInvuln.transform.SetParent(transform);
        }

    }

    public void OnGoalReached()
    {
        Debug.Log("Goal Reached");
        if (foundKeys == keysToFind)
        {
            Debug.Log("Escape the maze");
        }
    }

    public void OnKeyFound()
    {
        foundKeys++;

        SetKeyValueText();

        if (foundKeys == keysToFind)
        {
            GetComponentInChildren<MazeGoal>().OpenGoal();
        }
    }

    void SetKeyValueText()
    {
        keysValueText.text = foundKeys.ToString() + " of " + keysToFind.ToString();
    }

    //BrainThingie

    public void OnInvulnPicked()
    {

        Debug.Log("Invuln Picked");

    }

    public void OnSpeedPicked()
    {

        Debug.Log("Speed Picked");

    }

    public void OnStarPicked()
    {

        Debug.Log("Star Picked");

    }
}