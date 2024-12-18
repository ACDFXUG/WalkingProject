using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 该类用于处理导航路径的计算和显示。
/// 作为 MonoBehaviour 附加到 GameObject 上。
/// </summary>
public class PathOfNavigation : MonoBehaviour
{
    /// <summary>
    /// 玩家的 NavMeshAgent 组件。
    /// </summary>
    public NavMeshAgent player;

    /// <summary>
    /// 用于绘制路径的 LineRenderer 组件。
    /// </summary>
    private LineRenderer lr;

    /// <summary>
    /// 用于将鼠标位置转换为世界坐标的组件。
    /// </summary>
    public MouseToWorldPosition mtwp;

    /// <summary>
    /// 存储路径的列表。
    /// </summary>
    public List<Vector3> path;

    /// <summary>
    /// 记录上次更新位置的时间。
    /// </summary>
    private Vector3 lastPosition;

    /// <summary>
    /// 更新路径的距离阈值。
    /// </summary>
    private const float updateDistance = 2.5f;

    /// <summary>
    /// 是否显示路径。
    /// </summary>
    private bool isShown = true;

    /// <summary>
    /// 在第一次帧更新之前调用。
    /// 初始化路径列表和 LineRenderer。
    /// </summary>
    void Start()
    {
        path = new List<Vector3>(25); // 初始化路径列表，预分配 25 个元素
        lr = GetComponent<LineRenderer>(); // 获取 LineRenderer 组件
        lastPosition = player.transform.position; // 记录玩家初始位置
    }

    /// <summary>
    /// 每帧调用一次。
    /// 处理路径的更新和显示切换。
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 检查是否按下数字键 1
        {
            isShown = !isShown; // 切换路径显示状态
            SwitchVisibility(); // 更新路径可见性
        }

        if (Vector3.Distance(lastPosition, player.transform.position) > updateDistance) // 检查玩家移动距离是否超过阈值
        {
            CalculatePath(player.transform.position, mtwp.worldPosition); // 计算新路径
            lastPosition = player.transform.position; // 更新上次记录的位置
        }
    }

    /// <summary>
    /// 计算从起点到终点的路径。
    /// </summary>
    /// <param name="start">起点。</param>
    /// <param name="end">终点。</param>
    void CalculatePath(Vector3 start, Vector3 end)
    {
        path.Clear(); // 清空路径列表
        lr.positionCount = 0; // 重置 LineRenderer 的位置计数

        NavMeshPath nmpath = new(); // 创建一个新的 NavMeshPath 对象

        NavMesh.SamplePosition(end, out NavMeshHit hit, 10f, NavMesh.AllAreas); // 获取最近的可行走位置
        if (player.CalculatePath(hit.position, nmpath)) // 计算路径
        {
            foreach (var cn in nmpath.corners) // 遍历路径的拐点
            {
                path.Add(cn); // 将拐点添加到路径列表
            }

            player.SetDestination(hit.position); // 设置玩家的目标位置
            if (isShown) // 如果路径显示开启
            {
                lr.positionCount = path.Count; // 更新 LineRenderer 的位置计数
                lr.SetPositions(nmpath.corners); // 设置 LineRenderer 的位置
            }

            Debug.Log("Path found to " + hit.position); // 打印路径找到的信息
        }
        else
        {
            Debug.Log("Path not found"); // 打印路径未找到的信息
        }
    }

    /// <summary>
    /// 切换路径的可见性。
    /// </summary>
    void SwitchVisibility()
    {
        lr.enabled = isShown; // 更新 LineRenderer 的可见性
    }
}