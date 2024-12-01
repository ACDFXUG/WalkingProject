using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlay : MonoBehaviour
{
    Animator animator;
    PlayerController pc;
    bool hasSpeed,leftShift;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        pc=GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.movement.x!=0f||pc.movement.z!=0f){
            hasSpeed=true;
        }else{
            hasSpeed=false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            leftShift=true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            leftShift=false;
        }
        animator.SetBool("hasSpeed",hasSpeed);
        animator.SetBool("leftShift",leftShift);
    }
}
