using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance;
    public Player player;
    public IObstacle[] obstacles;
    private PlayerLadderHandler obstacleHandler;
    public QTEManager qteManager; 
    public UIController uiController;
    public Wall wall;

    private bool isInQTE;

    public void handlePlayerEncounting(IObstacle obstacle){
        launchQTE();
        if(obstacle.isPassed()){
            player.hasPassedObstacle();
            stopQTE();
        }else{
            if(qteManager.isSucceeded()){
                playerClimbing();
            }
            else player.nullizeVelocity();
        }
    }

    public void playerClimbing(){
        player.movePlayerVertically();
    }

    private void Awake(){
        instance = this;
        isInQTE = false;
    }

    public void gameOver(){
        stopQTE();
        hideKeys();
        player.getRigidBody().simulated = false;
        wall.moveSpeed = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void launchQTE(){
        isInQTE = true;
    }

    private void stopQTE(){
        isInQTE = false;
        qteManager.reinit_timedQTE();
    }

    private void hasSucceedQTE(string _type){
        qteManager.hasSucceed(_type);
        qteManager.reinit_timedQTE();
    }

    private void hasFailedQTE(string _type){
        qteManager.hasFailed(_type);
        qteManager.reinit_timedQTE();
    }

    private void showKeys(){
        if(! qteManager.canStart()) return;

        if(qteManager.isWantedKey("q")) uiController.showQKey();
        else uiController.hideQKey();
        if(qteManager.isWantedKey("d")) uiController.showDKey();
        else uiController.hideDKey();
    }

    private void hideKeys(){
        uiController.hideQKey();
        uiController.hideDKey();
    }

    public void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        if(isInQTE){
            showKeys();
            qteManager.timedQTE();

            if(Input.GetKeyDown("q") && !qteManager.isSucceeded() && qteManager.getTimeCounter()>0 && qteManager.hasTimerStarted() && qteManager.isWantedKey("q")){
                Debug.Log("Hurray Q");
                hasSucceedQTE("q");
            }
            else if(Input.GetKeyDown("d") && !qteManager.isSucceeded() && qteManager.getTimeCounter()>0 && qteManager.hasTimerStarted() && qteManager.isWantedKey("d")){
                Debug.Log("Hurray D");
                hasSucceedQTE("d");
            }

            else if (Input.GetKeyDown("d") && !qteManager.isSucceeded() && qteManager.isWantedKey("q") && qteManager.hasTimerStarted()){
                Debug.Log("Oh poor q");
                hasFailedQTE("q");
            }

            else if (Input.GetKeyDown("q") && qteManager.isWantedKey("d") && !qteManager.isSucceeded() && qteManager.hasTimerStarted()){
                Debug.Log("Oh poor d");
                hasFailedQTE("d");
            }

            else if (qteManager.getTimeCounter() <=0){
                Debug.Log("Time Finished");
                hasFailedQTE(qteManager.getType());
            }
        }
        else hideKeys();
    }
}
