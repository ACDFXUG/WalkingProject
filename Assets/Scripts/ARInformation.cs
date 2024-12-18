using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARInformation : MonoBehaviour
{
    // 显示信息的文本组件引用
    public TMP_Text info;
    // 玩家的变换组件引用
    public Transform player;
    // 将鼠标位置转换为世界位置的组件引用
    public MouseToWorldPosition mtwp;
    // 存储玩家上次的位置，用于计算速度
    private Vector3 lastPos;

    // 在第一次帧更新之前调用
    void Start()
    {
        // 初始化玩家的上次位置
        lastPos = player.position;
        // 调用 DisplayText 方法显示初始信息
        DisplayText();
    }

    // 每帧调用一次
    void Update()
    {
        // 当按下 'V' 键时切换信息显示
        if (Input.GetKeyDown(KeyCode.V))
        {
            info.enabled = !info.enabled;
        }
        // 更新显示的信息
        DisplayText();
        // 更新玩家的上次位置
        lastPos = player.position;
    }

    // 更新显示的信息
    void DisplayText()
    {
        // 计算玩家当前的速度
        var speed = GetSpeed();
        // 获取玩家当前位置
        var curLocation = player.position;
        // 获取目标位置
        var dest = mtwp.worldPosition;
        // 计算到达目标所需的时间
        var time = Vector3.Distance(curLocation, dest) / speed;
        // 更新显示的文本
        info.text = "Speed " + speed + "\nCurrent Location " + curLocation + "\nDestination " + dest + "\nTime " + time;
    }

    // 计算玩家的速度
    float GetSpeed()
    {
        // 通过位置变化除以时间变化来计算速度
        return ((player.position - lastPos) / Time.deltaTime).magnitude;
    }
}