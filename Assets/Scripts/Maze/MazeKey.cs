using UnityEngine;
using System.Collections;

public class MazeKey : MonoBehaviour
{

    void OnTriggerEnter2D()
    {
        transform.parent.SendMessage("OnKeyFound", SendMessageOptions.DontRequireReceiver);
        GameObject.Destroy(gameObject);
    }

}
