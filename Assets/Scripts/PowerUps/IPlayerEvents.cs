using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPlayerEvents : IEventSystemHandler
{
    void OnPlayerHurt(int newHealth);

    void OnPlayerReachedExit(GameObject exit);
}