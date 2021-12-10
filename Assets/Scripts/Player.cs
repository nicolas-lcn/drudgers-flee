using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float BasicMoveSpeed;
    public float ClimbingSpeed;

    Rigidbody2D rb;
    CapsuleCollider2D col;

    //interact with Obstacle
    IObstacle focus = null;
    string focusType = "";

    PlayerObstacleHandler playerObstacleHandler;


    //smoothDamp
    Vector2 m_Velocity = Vector3.zero;
    private float m_movementSmoothing = .05f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    bool isAnObstacle(string tag)
    {
        bool res = false;
        switch (tag)
        {
            case "echelle":
                res = true;
                break;
        }
        return res;
    }

    public CapsuleCollider2D getCollider(){
        return col;
    }

    public Rigidbody2D getRigidBody(){
        return rb;
    }

    void automaticMovePlayer()
    {
        Vector2 targetVelocity = new Vector2(Vector2.right.x * BasicMoveSpeed * Time.deltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_movementSmoothing);
    }

    void movePlayer()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * BasicMoveSpeed * Time.deltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_movementSmoothing);
    }

    bool checkWallEncounter(){
        string hitTag ="";
        RaycastHit2D hit;

        if ((hit = Physics2D.Raycast(transform.position, new Vector2(col.size.x / 2, 0), col.size.x / 2))) //initialize hit + check is null
        {
            hitTag = hit.collider.tag;
        }
        return hitTag.Equals("wall");
    }


    bool checkObstacleEncounter(){
        RaycastHit2D hit;

        if ((hit = Physics2D.Raycast(transform.position, new Vector2(col.size.x / 2, 0), col.size.x / 2))) //initialize hit + check is null
        {
            string hitTag = hit.collider.tag;
       
            if (isAnObstacle(hitTag)) {
                focus = hit.collider.GetComponent<IObstacle>();
                focusType = hitTag;
                focus.setPlayer(this);
            }
        }
        return focus != null;
    }

    public void movePlayerVertically(){
        Vector2 targetVelocity = new Vector2(rb.velocity.x, Vector2.up.y * ClimbingSpeed * Time.deltaTime);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_movementSmoothing);
    }

    private void Update()
    {
        
    }

    public void hasPassedObstacle(){
        focus= null;
        nullizeVelocity();
    }

    public void nullizeVelocity(){
        rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref m_Velocity, m_movementSmoothing); 
    }

  
    void FixedUpdate()
    {
        if(checkWallEncounter()) GameManager.instance.gameOver();
        if(checkObstacleEncounter()){
            GameManager.instance.handlePlayerEncounting(focus);
        }
        else{
            automaticMovePlayer();
        }
    }
}
