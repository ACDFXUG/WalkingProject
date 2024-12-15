using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathOfNavigation : MonoBehaviour
{
    public NavMeshAgent player;
    private LineRenderer lr;
    public MouseToWorldPosition mtwp;
    private List<Vector3> path;
    private Vector3 lastPosition;
    private const float updateDistance=3f;
    private bool isShown=true;
    // Start is called before the first frame update
    void Start()
    {
        path=new List<Vector3>();
        lr=GetComponent<LineRenderer>();
        CalculatePath(player.transform.position,mtwp.worldPosition);
        lastPosition=player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)){
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
        
        if (NavMesh.SamplePosition(end, out NavMeshHit hit, 10f, NavMesh.AllAreas)){
            Debug.Log("Hit"+hit.position);
        }else{
            Debug.Log("Miss");
        }
        if (player.CalculatePath(hit.position,nmpath)){
            foreach(var cn in nmpath.corners){
                path.Add(cn);
            }
            player.SetDestination(hit.position);
            if(isShown){
                lr.positionCount=path.Count;
                lr.SetPositions(path.ToArray());
            }
        }else{
            Debug.LogWarning("Path not found");
        }
    }
    void SwitchVisibility(){
        if(isShown){
            lr.enabled=true;
        }else{
            lr.enabled=false;
        }
    }
}
