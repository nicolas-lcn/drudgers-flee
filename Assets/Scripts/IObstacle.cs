using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle
{
    void setPlayer(Player player);
    bool tryProgress();

    bool isPassed();
    public PlayerLadderHandler getController();
}
