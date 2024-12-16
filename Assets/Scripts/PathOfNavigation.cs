using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathOfNavigation : MonoBehaviour
{
    public NavMeshAgent player;
    private LineRenderer lr;
    public MouseToWorldPosition mtwp;
    public List<Vector3> path;
    private Vector3 lastPosition;
    private const float updateDistance=3f;
    private bool isShown=true;
    // Start is called before the first frame update
    void Start()
    {
        path=new List<Vector3>(25);
        lr=GetComponent<LineRenderer>();
        lastPosition=player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            isShown=!isShown;
            SwitchVisibility();
        }
        if(Vector3.Distance(lastPosition,player.transform.position)>updateDistance){
            CalculatePath(player.transform.position,mtwp.worldPosition);
            lastPosition=player.transform.position;
        }
    }
    void CalculatePath(Vector3 start,Vector3 end){
        path.Clear();
        lr.positionCount=0;
        NavMeshPath nmpath=new();
        
        NavMesh.SamplePosition(end, out NavMeshHit hit, 10f, NavMesh.AllAreas);
        if (player.CalculatePath(hit.position,nmpath)){
            foreach(var cn in nmpath.corners){
                path.Add(cn);
            }
            player.SetDestination(hit.position);
            if(isShown){
                lr.positionCount=path.Count;
                lr.SetPositions(nmpath.corners);
            }
            Debug.Log("Path found to "+hit.position);
        }else{
            Debug.Log("Path not found");
        }
    }
    void SwitchVisibility(){
        lr.enabled=isShown;
    }
}
