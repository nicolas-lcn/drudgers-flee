using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    
    public SpriteRenderer q_key;
    public SpriteRenderer d_key;

    public void showQKey(){
        q_key.enabled = true;
    }

    public void hideQKey(){
        q_key.enabled = false;
    }

    public void showDKey(){
        d_key.enabled = true;
    }

    public void hideDKey(){
        d_key.enabled = false;
    }
}
