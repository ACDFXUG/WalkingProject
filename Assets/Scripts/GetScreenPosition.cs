using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类用于获取玩家在屏幕上的位置。
/// 作为 MonoBehaviour 附加到 GameObject 上。
/// </summary>
public class GetScreenPosition : MonoBehaviour
{
    /// <summary>
    /// 地图相机。
    /// </summary>
    public Camera map;

    /// <summary>
    /// 玩家在屏幕上的位置。
    /// </summary>
    public Vector3 screenPosition;

    /// <summary>
    /// 玩家的 Transform 组件。
    /// </summary>
    public Transform playerTrans;

    /// <summary>
    /// 在第一次帧更新之前调用。
    /// 初始化玩家在屏幕上的位置。
    /// </summary>
    void Start()
    {
        screenPosition = map.WorldToScreenPoint(playerTrans.position); // 将玩家的世界坐标转换为屏幕坐标
    }

    /// <summary>
    /// 每帧调用一次。
    /// 更新玩家在屏幕上的位置。
    /// </summary>
    void Update()
    {
        screenPosition = map.WorldToScreenPoint(playerTrans.position); // 将玩家的世界坐标转换为屏幕坐标
    }
}