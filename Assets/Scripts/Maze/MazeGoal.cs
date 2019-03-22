using UnityEngine;
using System.Collections;

public class MazeGoal : MonoBehaviour
{

    public Sprite closedGoalSprite;
    public Sprite openedGoalSprite;

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = closedGoalSprite;
    }

    public void OpenGoal()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = openedGoalSprite;
    }

    void OnTriggerEnter2D()
    {
        transform.parent.SendMessage("OnGoalReached", SendMessageOptions.DontRequireReceiver);
    }
}
