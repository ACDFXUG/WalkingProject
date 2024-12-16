using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制玩家移动和摄像机视角的类
public class PlayerController : MonoBehaviour
{
    // 玩家的CharacterController组件，用于处理角色移动
    CharacterController player;
    // 摄像机的Transform组件，用于调整视角
    public new Transform camera;
    // 玩家的移动速度
    public float speed = 6f;
    // 玩家的跳跃高度
    public float jump = 2.5f;
    // 存储水平和垂直输入值的变量
    float x, y;
    // 重力常量
    const float g = 9.8f;
    // 玩家的移动向量
    public Vector3 movement;

    // 在游戏开始前调用一次
    void Start()
    {
        // 获取附加在玩家身上的CharacterController组件
        player = GetComponent<CharacterController>();
        // 锁定光标并使其不可见，以提供更沉浸的游戏体验
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 每帧更新时调用
    void Update()
    {
        // 获取水平和垂直输入用于移动
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        // 如果玩家站在地面上
        if (player.isGrounded)
        {
            // 设置玩家的移动向量
            movement = new Vector3(_horizontal, 0, _vertical);
            // 如果按下空格键，设置跳跃
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.y = jump;
            }
            // 如果按下左Shift键，增加移动速度
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = 9f;
            }
            // 如果释放左Shift键，恢复默认移动速度
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 6f;
            }
        }

        // 应用重力
        movement.y -= g * Time.deltaTime;
        // 移动玩家
        player.Move(
            player.transform.TransformDirection(
                speed * Time.deltaTime * movement
            )
        );

        // 根据鼠标输入旋转玩家
        x += Input.GetAxis("Mouse X");
        y -= Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(0, x, 0);

        // 限制摄像机的上下旋转角度
        y = Mathf.Clamp(y, -80f, 55f);
        camera.eulerAngles = new Vector3(y, x, 0);

        // 确保摄像机的z轴旋转角度为0
        if (camera.localEulerAngles.z != 0)
        {
            var rotX = camera.localEulerAngles.x;
            var rotY = camera.localEulerAngles.y;
            // camera.localEulerAngles = new Vector3(rotX, rotY, 0);
            camera.localEulerAngles.Set(rotX, rotY, 0);
        }
    }
}