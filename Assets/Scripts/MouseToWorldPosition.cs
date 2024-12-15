using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseToWorldPosition : MonoBehaviour
{
    public Vector3 worldPosition;
    public Camera mapCamera;
    const float y0=22f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mapCamera.enabled&&Input.GetMouseButtonDown(0)){
            var screenPosition=Input.mousePosition;
            screenPosition.z=0;
            Ray ray=mapCamera.ScreenPointToRay(screenPosition);
            worldPosition=GetWorldPosition(ray,y0);
        }
    }
    Vector3 GetWorldPosition(Ray ray,float y){
        Plane tmpPlane=new(Vector3.up,new Vector3(0,y,0));
        if(tmpPlane.Raycast(ray, out float dis)){
            return ray.GetPoint(dis);
        }
        return Vector3.zero;
    }
}
