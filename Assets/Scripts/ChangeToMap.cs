using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToMap : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera first,map;
    private Camera current;
    private HighlightPlayer hp;
    void Start()
    {
        hp=GameObject.Find("Person").GetComponent<HighlightPlayer>();
        current=first;
        if(map!=null){
            map.enabled=false;
        }
        hp.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            SwitchCamera();
        }
    }
    /// <summary>
    /// Switches between the main camera and the map camera.
    /// This function is called when the 'M' key is pressed.
    /// </summary>
    void SwitchCamera(){
        if(current==first){
            current.enabled=false;
            map.enabled=true;
            current=map;
            Cursor.lockState=CursorLockMode.None;
            Cursor.visible=true;
            hp.Show();
        }else{
            current.enabled=false;
            first.enabled=true;
            current=first;
            Cursor.lockState=CursorLockMode.Locked;
            Cursor.visible=false;
            hp.Hide();
        }
    }
}
