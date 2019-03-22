using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;


    void Start()
    {
        StartCoroutine(ChangeCam(0.1f));
        camera1.enabled = true;
        camera2.enabled = false;

    }

    IEnumerator ChangeCam(float timeDelay)
    {

        //before the time delay

        //this waits the amount that the method was called with
        yield return new WaitForSeconds(timeDelay);

        //after the time delay... let's say it was 5 seconds, so the stuff after the waitforseconds is called only after that 5 seconds, and then the stuff before isn't called again.
        //you can incorporate this some easier way, but I'm lazy so ill just make it change the cameras in here
        //so:

        camera1.enabled = false;
        camera2.enabled = true;

        //then we need to stop this coroutine so it doesn't just keep going and going and taking up time

        yield break;
        //this just stops the coroutine immediately

    }
}