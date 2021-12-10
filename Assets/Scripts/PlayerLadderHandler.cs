using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderHandler : MonoBehaviour
{
    public GameObject ladder;
    private SpriteRenderer[] steps;
    private int numberOfSteps;
    private int index;

    void Start(){
        numberOfSteps = ladder.transform.childCount;
        steps = new SpriteRenderer[numberOfSteps];
        for(int i =0; i<numberOfSteps; i++){
            steps[i] = ladder.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
        }
        index = 0;
    }

    public void hideAll(){
        for(int i =1; i<numberOfSteps; i++){
            steps[i].enabled = false;
        }
    }

    public void showNext(){
        steps[index].enabled = true;
        index++;
    }

    public void hideCurrent(){
        steps[index].enabled = false;
        index--;
    }


    
}
