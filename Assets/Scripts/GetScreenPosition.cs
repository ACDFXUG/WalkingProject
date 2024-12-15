using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScreenPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera map;
    public Vector3 screenPosition;
    public Transform playerTrans;
    void Start()
    {
        screenPosition=map.WorldToScreenPoint(playerTrans.position);
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition=map.WorldToScreenPoint(playerTrans.position);
    }
}
