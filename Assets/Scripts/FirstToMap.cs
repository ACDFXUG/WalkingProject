using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类用于在游戏中切换主相机和地图相机。
/// 作为 MonoBehaviour 附加到 GameObject 上。
/// </summary>
public class FirstToMap : MonoBehaviour
{
    /// <summary>
    /// 主相机和地图相机。
    /// </summary>
    public Camera first, map;

    /// <summary>
    /// 当前使用的相机。
    /// </summary>
    private Camera current;

    /// <summary>
    /// 高亮玩家组件。
    /// </summary>
    private HighlightPlayer hp;

    /// <summary>
    /// 在第一次帧更新之前调用。
    /// 初始化相机和高亮玩家组件。
    /// </summary>
    void Start()
    {
        hp = GetComponent<HighlightPlayer>();
        current = first;
        if (map != null)
        {
            map.enabled = false; // 关闭地图相机
        }
        hp.Hide(); // 隐藏高亮玩家
    }

    /// <summary>
    /// 每帧调用一次。
    /// 监听按下 'M' 键以切换相机。
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SwitchCamera(); // 切换相机
        }
    }

    /// <summary>
    /// 切换当前相机和地图相机。
    /// 当按下 'M' 键时调用此方法。
    /// </summary>
    void SwitchCamera()
    {
        if (current == first)
        {
            current.enabled = false; // 关闭当前相机
            map.enabled = true; // 启用地图相机
            current = map; // 更新当前相机
            Cursor.lockState = CursorLockMode.None; // 解锁鼠标
            Cursor.visible = true; // 显示鼠标
            hp.Show(); // 显示高亮玩家
        }
        else
        {
            current.enabled = false; // 关闭当前相机
            first.enabled = true; // 启用主相机
            current = first; // 更新当前相机
            Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标
            Cursor.visible = false; // 隐藏鼠标
            hp.Hide(); // 隐藏高亮玩家
        }
    }
}