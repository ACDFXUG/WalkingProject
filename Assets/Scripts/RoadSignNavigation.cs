using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class RoadSignNavigation : MonoBehaviour
{
    // 路标预制件
    public GameObject roadSignPrefab;

    // 路径点列表
    private List<Vector3> paths;

    // 导航路径组件
    private PathOfNavigation pon;

    // 存储当前的路标
    private GameObject currentRoadSign;

    void Start()
    {   
        // 获取 PathOfNavigation 组件
        pon = GetComponent<PathOfNavigation>();

        // 初始化路径点列表
        paths = new(pon.path);

        // 更新路标
        UpdateRoadSign();
    }

    void Update()
    {
        // 按下数字键2时切换路标的激活状态
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentRoadSign != null)
            {
                currentRoadSign.SetActive(!currentRoadSign.activeSelf);
            }
        }

        // 检查路径是否更新
        if (pon.path != null && !PathsEqual(pon.path, paths))
        {
            // 更新路径点列表
            paths = new(pon.path);

            // 更新路标
            UpdateRoadSign();
        }

        // 每帧更新路标的坐标和方向
        UpdateRoadSignPositionAndDirection();
    }

    private void UpdateRoadSign()
    {
        // 如果路径为空或只有一个点，销毁现有路标并退出
        if (paths == null || paths.Count <= 1)
        {
            if (currentRoadSign != null)
            {
                Destroy(currentRoadSign);
                currentRoadSign = null;
            }
            return;
        }

        // 如果还没有创建路标，则创建一个
        if (currentRoadSign == null)
        {
            currentRoadSign = Instantiate(roadSignPrefab);
            // 可选：将路标作为此物体的子对象
            // currentRoadSign.transform.SetParent(transform);
        }

        // 更新路标的坐标和方向
        UpdateRoadSignPositionAndDirection();
    }

    private void UpdateRoadSignPositionAndDirection()
    {
        // 确保路径至少有一个点
        if (paths == null || paths.Count < 1 || currentRoadSign == null)
        {
            return;
        }

        // 获取路径的第一个和最后一个点
        Vector3 firstPoint = paths[0];
        Vector3 lastPoint = paths[^1];

        // 根据路径点的数量决定路标的放置位置和方向
        if (paths.Count >= 3)
        {
            // 路径有三个或更多点
            Vector3 secondPoint = paths[1];
            Vector3 directionToNextPoint = paths[2] - secondPoint;
            Vector3 reversedDirection = -directionToNextPoint;

            // 设置路标的坐标为第二个点的位置
            currentRoadSign.transform.position = secondPoint;

            // 设置路标的旋转以指向反转后的方向
            currentRoadSign.transform.rotation = Quaternion.LookRotation(reversedDirection);

            Debug.Log($"Updated sign at: {secondPoint} pointing to: {reversedDirection}");
        }
        else if (paths.Count == 2)
        {
            // 路径只有两个点
            Vector3 directionToFirstPoint = firstPoint - lastPoint;

            // 设置路标的坐标为最后一个点的位置
            currentRoadSign.transform.position = lastPoint;

            // 设置路标的旋转以指向反转后的方向
            currentRoadSign.transform.rotation = Quaternion.LookRotation(directionToFirstPoint);

            Debug.Log($"Updated sign at: {lastPoint} pointing to: {directionToFirstPoint}");
        }
        else if (paths.Count == 1)
        {
            // 路径只有一个点，不显示路标
            if (currentRoadSign != null)
            {
                Destroy(currentRoadSign);
                currentRoadSign = null;
            }
        }
    }

    private bool PathsEqual(List<Vector3> path1, List<Vector3> path2)
    {
        // 如果任一路径为空，返回 false
        if (path1 == null || path2 == null) return false;

        // 如果路径长度不同，返回 false
        if (path1.Count != path2.Count) return false;

        // 比较每个路径点是否相等
        for (int i = 0; i < path1.Count; i++)
        {
            if (!path1[i].Equals(path2[i])) return false;
        }

        // 路径点完全相同，返回 true
        return true;
    }
}