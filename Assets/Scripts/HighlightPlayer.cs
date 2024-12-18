using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 该类用于高亮显示玩家。
/// 作为 MonoBehaviour 附加到 GameObject 上。
/// </summary>
public class HighlightPlayer : MonoBehaviour
{   
    /// <summary>
    /// 高亮指针的 Image 组件。
    /// </summary>
    public Image pointer;

    /// <summary>
    /// 指针的大小。
    /// </summary>
    public float size = 4f;

    /// <summary>
    /// 是否显示高亮指针。
    /// </summary>
    private bool isShown = false;

    /// <summary>
    /// 获取屏幕位置的组件。
    /// </summary>
    private GetScreenPosition gsp;

    /// <summary>
    /// 在第一次帧更新之前调用。
    /// 初始化高亮指针的大小并隐藏指针。
    /// </summary>
    void Start()
    {
        gsp = GetComponent<GetScreenPosition>(); // 获取 GetScreenPosition 组件
        if (pointer != null)
        {
            pointer.rectTransform.sizeDelta = new Vector2(size, size); // 设置指针的大小
            Hide(); // 隐藏指针
        }
    }

    /// <summary>
    /// 每帧调用一次。
    /// 更新高亮指针的位置。
    /// </summary>
    void Update()
    {
        pointer.rectTransform.anchoredPosition = 
            new Vector2(gsp.screenPosition.x, gsp.screenPosition.y); // 更新指针的位置
    }

    /// <summary>
    /// 显示高亮指针。
    /// </summary>
    public void Show()
    {
        if (pointer != null && !isShown)
        {
            isShown = true;
            pointer.enabled = true; // 启用指针
        }
    }

    /// <summary>
    /// 隐藏高亮指针。
    /// </summary>
    public void Hide()
    {
        if (pointer != null && isShown)
        {
            isShown = false;
            pointer.enabled = false; // 禁用指针
        }
    }
}