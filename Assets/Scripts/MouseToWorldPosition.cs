using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 该类用于将鼠标位置转换为世界坐标。
/// 作为 MonoBehaviour 附加到 GameObject 上。
/// </summary>
public class MouseToWorldPosition : MonoBehaviour
{
    /// <summary>
    /// 转换后的世界坐标。
    /// </summary>
    public Vector3 worldPosition;

    /// <summary>
    /// 地图相机。
    /// </summary>
    public Camera mapCamera;

    /// <summary>
    /// 常量 y 值，用于确定平面的高度。
    /// </summary>
    const float y0 = 22f;

    /// <summary>
    /// 在第一次帧更新之前调用。
    /// </summary>
    void Start()
    {
        // 目前没有初始化操作
    }

    /// <summary>
    /// 每帧调用一次。
    /// 处理鼠标点击事件并将鼠标位置转换为世界坐标。
    /// </summary>
    void Update()
    {
        if (mapCamera.enabled && Input.GetMouseButtonDown(0)) // 检查地图相机是否启用且鼠标左键被按下
        {
            var screenPosition = Input.mousePosition; // 获取鼠标在屏幕上的位置
            screenPosition.z = 0; // 设置 z 坐标为 0
            Ray ray = mapCamera.ScreenPointToRay(screenPosition); // 将屏幕坐标转换为射线
            worldPosition = GetWorldPosition(ray, y0); // 获取世界坐标
        }
    }

    /// <summary>
    /// 根据射线和指定的 y 值计算世界坐标。
    /// </summary>
    /// <param name="ray">射线。</param>
    /// <param name="y">平面的 y 值。</param>
    /// <returns>世界坐标。</returns>
    Vector3 GetWorldPosition(Ray ray, float y)
    {
        Plane tmpPlane = new Plane(Vector3.up, new Vector3(0, y, 0)); // 创建一个水平平面
        if (tmpPlane.Raycast(ray, out float dis)) // 检查射线是否与平面相交
        {
            return ray.GetPoint(dis); // 返回相交点的世界坐标
        }
        return Vector3.zero; // 如果没有相交，返回 (0, 0, 0)
    }
}