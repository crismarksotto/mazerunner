using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPowerUpEvents : IEventSystemHandler
{
    void OnPowerUpCollected(PowerUp powerUp, MazeRunner player);

    void OnPowerUpExpired(PowerUp powerUp, MazeRunner player);
}