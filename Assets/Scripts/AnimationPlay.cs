using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlay : MonoBehaviour
{
    // Animator组件，用于控制角色动画
    Animator animator;
    // PlayerController组件，用于获取玩家控制信息
    PlayerController pc;
    // hasSpeed变量，用于判断角色是否在移动
    bool hasSpeed;
    // leftShift变量，用于判断玩家是否按下左Shift键
    bool leftShift;
    
    // Start is called before the first frame update
    void Start()
    {
        // 初始化Animator组件
        animator = GetComponent<Animator>();
        // 初始化PlayerController组件
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 判断角色是否在水平或垂直方向上移动，如果是，则hasSpeed为true，否则为false
        if (pc.movement.x != 0f || pc.movement.z != 0f)
        {
            hasSpeed = true;
        }
        else
        {
            hasSpeed = false;
        }
        
        // 当玩家按下左Shift键时，leftShift为true
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            leftShift = true;
        }
        
        // 当玩家释放左Shift键时，leftShift为false
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            leftShift = false;
        }
        
        // 将hasSpeed变量值传递给Animator控制器
        animator.SetBool("hasSpeed", hasSpeed);
        // 将leftShift变量值传递给Animator控制器
        animator.SetBool("leftShift", leftShift);
    }
}