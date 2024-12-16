using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARInformation : MonoBehaviour
{
    public TMP_Text info;
    public GameObject player;
    public MouseToWorldPosition mtwp;
    private Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        lastPos=player.transform.position;
        DisplayText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)){
            info.enabled=!info.enabled;
        }
        DisplayText();
        lastPos=player.transform.position;
    }
    void DisplayText(){
        var speed=GetSpeed();
        var curLocation=player.transform.position;
        var dest=mtwp.worldPosition;
        var time=Vector3.Distance(curLocation,dest)/speed;
        info.text="Speed "+speed+"\nCurrent Location "+curLocation+"\nDestination "+dest+"\nTime "+time;
    }
    float GetSpeed(){
        return ((player.transform.position-lastPos)/Time.deltaTime).magnitude;
    }
}