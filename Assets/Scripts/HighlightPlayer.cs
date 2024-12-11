using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightPlayer : MonoBehaviour
{   
    public Image pointer;
    public float size=8f;
    private bool isShown=false;
    private GetScreenPosition gsp;
    // Start is called before the first frame update
    void Start()
    {
        gsp=GetComponent<GetScreenPosition>();
        if(pointer!=null){
            pointer.rectTransform.sizeDelta=new Vector2(size,size);
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        pointer.rectTransform.anchoredPosition=
        new(gsp.screenPosition.x,gsp.screenPosition.y);
    }
    public void Show(){
        if(pointer!=null&&!isShown){
            isShown=true;
            pointer.enabled=true;
        }
    }
    public void Hide(){
        if(pointer!=null&&isShown){
            isShown=false;
            pointer.enabled=false;
        }
    }
}
