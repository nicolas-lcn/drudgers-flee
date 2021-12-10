using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echelle : MonoBehaviour, IObstacle
{
    BoxCollider2D col;
    Vector3 colSize;
    //bool _isPassed = false;
    Player m_player = null;
    public PlayerLadderHandler controller;

    public void setPlayer(Player pPlayer)
    {
        m_player = pPlayer;
    }

    public bool isPassed()
    {
        return (m_player.getCollider().bounds.min.y >= col.bounds.max.y - 0.5f && m_player.getCollider().bounds.min.y <= col.bounds.max.y + 0.5f);
    }

    public bool tryProgress()
    {
        return isPassed();
    }

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        colSize = col.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlayerLadderHandler getController(){
        return controller;
    }
}
