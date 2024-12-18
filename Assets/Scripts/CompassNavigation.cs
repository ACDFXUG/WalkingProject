using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassNavigation : MonoBehaviour
{
    public Transform player;
    public MouseToWorldPosition mtwp;
    public Image arrow;
    // private Vector3 start,end;
    private float angle;
    private Vector3 start,end,fwd;
    // Start is called before the first frame update

    void Start(){}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            arrow.enabled=!arrow.enabled;
        }

        start=player.position;
        end=mtwp.worldPosition;
        
        // 玩家面向的方向（2D情况下可以考虑使用 player.right）
        fwd=player.forward;

        // 目标方向
        Vector3 tgtDirection = (end-start).normalized;

        // 计算相对于玩家面向方向的角度
        angle=Vector3.Angle(fwd,tgtDirection);

        // 确定旋转方向
        float sign=Mathf.Sign(Vector3.Dot(Vector3.up,Vector3.Cross(fwd,tgtDirection)));

        // 将角度转换为适合UI旋转的形式
        // 以确保箭头正确地指向前方
        angle=-sign*angle;

        // 设置箭头的旋转
        arrow.rectTransform.rotation=Quaternion.Euler(0,0,angle);
    }
}
