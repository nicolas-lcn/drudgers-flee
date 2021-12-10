using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    private bool eventSuccess = false;
    public float init_timeCounter;
    private float timeCounter;

    public  float init_beforeStartTime;
    private float beforeStartTime;

    private string type;

    public void Start(){
        timeCounter = init_timeCounter;
        type = "q";
    }


    public void reinit_timedQTE(){
        timeCounter = init_timeCounter;
        beforeStartTime = init_beforeStartTime;
    }

    public void timedQTE(){
        if(canStart()){
            eventSuccess = false;
        }
        if(! eventSuccess){
            timeCounter -= Time.deltaTime;
        }    
    }

    public bool canStart(){
        if(beforeStartTime - Time.deltaTime >0){
            beforeStartTime -= Time.deltaTime;
            return false;
        }
        else{
            return true;
        }
    }

    public float getTimeCounter(){
        return timeCounter;
    }

    public float getBeforeStartTime(){
        return beforeStartTime;
    }

    public void hasSucceed(string _type){
        eventSuccess = true;
        if(_type.Equals("q")){
                type = "d";
        }
        if(_type.Equals("d")){
                type = "q";
        }
    }

    public void hasFailed(string _type){
        eventSuccess = false;
        if(_type.Equals("q")){
                type = "d";
        }
        if(_type.Equals("d")){
                type = "q";
        }
    }


    public bool isSucceeded(){
        return eventSuccess;
    }

    public void setType(string _type){
        type = _type;
    }

    public bool isWantedKey(string key){
        return key.Equals(type);
    }

    public bool hasTimerStarted(){
        return timeCounter < init_timeCounter;
    }

    public string getType(){
        return type;
    }

}
