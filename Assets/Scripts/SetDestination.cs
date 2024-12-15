using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDestination : MonoBehaviour
{
    public Image dest;
    public Camera mapCamera;
    public float size=4f;
    bool isSet=false;
    MouseToWorldPosition mtwp;
    // Start is called before the first frame update
    void Start()
    {
        mtwp=GetComponent<MouseToWorldPosition>();
        dest.rectTransform.sizeDelta=new(size,size);
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(mapCamera.enabled){
            Vector3 screenPos=mapCamera.WorldToScreenPoint(mtwp.worldPosition);
            dest.rectTransform.anchoredPosition=new(screenPos.x,screenPos.y);
            if(Input.GetMouseButtonDown(0)){
                ShowAtMousePosition();
            }
            if(Input.GetMouseButtonDown(1)){
                Hide();
            }
        }
        if(isSet&&Input.GetKeyDown(KeyCode.M)){
            dest.enabled=!dest.enabled;
        }
    }
    void Hide(){
        dest.enabled=false;
        isSet=false;
    }
    void ShowAtMousePosition(){
        if(isSet){
            Hide();
        }
        dest.enabled=true;
        isSet=true;
    }
}
