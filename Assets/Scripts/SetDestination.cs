using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 该类用于设置目标位置。
/// 作为 MonoBehaviour 附加到 GameObject 上。
/// </summary>
public class SetDestination : MonoBehaviour
{
    /// <summary>
    /// 目标位置的 UI 图像。
    /// </summary>
    public Image dest;

    /// <summary>
    /// 地图相机。
    /// </summary>
    public Camera mapCamera;

    /// <summary>
    /// 目标图像的大小。
    /// </summary>
    public float size = 4f;

    /// <summary>
    /// 是否已经设置了目标位置。
    /// </summary>
    bool isSet = false;

    /// <summary>
    /// MouseToWorldPosition 组件的引用。
    /// </summary>
    MouseToWorldPosition mtwp;

    /// <summary>
    /// 在第一次帧更新之前调用。
    /// 初始化目标图像的大小并隐藏它。
    /// </summary>
    void Start()
    {
        mtwp = GetComponent<MouseToWorldPosition>(); // 获取 MouseToWorldPosition 组件
        dest.rectTransform.sizeDelta = new Vector2(size, size); // 设置目标图像的大小
        Hide(); // 隐藏目标图像
    }

    /// <summary>
    /// 每帧调用一次。
    /// 更新目标图像的位置，并处理鼠标点击事件。
    /// </summary>
    void Update()
    {
        if (mapCamera.enabled) // 检查地图相机是否启用
        {
            Vector3 screenPos = mapCamera.WorldToScreenPoint(mtwp.worldPosition); // 将世界坐标转换为屏幕坐标
            dest.rectTransform.anchoredPosition = new Vector2(screenPos.x, screenPos.y); // 更新目标图像的位置

            if (Input.GetMouseButtonDown(0)) // 检查是否按下鼠标左键
            {
                ShowAtMousePosition(); // 在鼠标位置显示目标图像
            }

            if (Input.GetMouseButtonDown(1)) // 检查是否按下鼠标右键
            {
                Hide(); // 隐藏目标图像
            }
        }

        if (isSet && Input.GetKeyDown(KeyCode.M)) // 检查是否按下了 M 键
        {
            dest.enabled = !dest.enabled; // 切换目标图像的可见性
        }
    }

    /// <summary>
    /// 隐藏目标图像。
    /// </summary>
    void Hide()
    {
        dest.enabled = false; // 关闭目标图像
        isSet = false; // 重置目标位置状态
    }

    /// <summary>
    /// 在鼠标位置显示目标图像。
    /// </summary>
    void ShowAtMousePosition()
    {
        if (isSet) // 如果已经设置了目标位置
        {
            Hide(); // 隐藏目标图像
        }

        dest.enabled = true; // 显示目标图像
        isSet = true; // 设置目标位置状态
    }
}