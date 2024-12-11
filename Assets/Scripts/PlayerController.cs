using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController player;
    public new Transform camera;
    public float speed=5f;
    public float jump=2.5f;
    float x,y;
    const float g=9.8f;
    public Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        player=GetComponent<CharacterController>();
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        

        float _horizontal=Input.GetAxis("Horizontal");
        float _vertical=Input.GetAxis("Vertical");
        if(player.isGrounded){
            movement=new Vector3(_horizontal,0,_vertical);
            if(Input.GetKeyDown(KeyCode.Space)){
                movement.y=jump;
            }
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                speed=9f;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift)){
                speed=5f;
            }
        }
        movement.y-=g*Time.deltaTime;
        player.Move(
            player.transform.TransformDirection(
                speed*Time.deltaTime*movement
            )
        );
        x+=Input.GetAxis("Mouse X");
        y-=Input.GetAxis("Mouse Y");
        transform.eulerAngles=new Vector3(0,x,0);
        y=Mathf.Clamp(y,-80f,55f);
        camera.eulerAngles=new Vector3(y,x,0);
        if(camera.localEulerAngles.z!=0){
            var rotX=camera.localEulerAngles.x;
            var rotY=camera.localEulerAngles.y;
            // camera.localEulerAngles=new Vector3(rotX,rotY,0);
            camera.localEulerAngles.Set(rotX,rotY,0);
        }
    }
}
