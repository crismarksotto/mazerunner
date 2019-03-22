using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IMainGameEvents : IEventSystemHandler
{
    void OnGameWon();

    void OnGameLost();
}