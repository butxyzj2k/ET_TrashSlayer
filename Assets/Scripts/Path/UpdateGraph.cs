using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Collections;

public class UpdateGraph : MonoBehaviour
{
    CapsuleCollider2D playerCollider;
    private List<GraphNode> obstacleNodes = new();

    private void Start()
    {
        playerCollider = PlayerController.instance.transform.GetComponent<CapsuleCollider2D>();
        AstarPath.active.data.GetNodes(node => 
        {
            if(!node.Walkable){
                obstacleNodes.Add(node);
            }
        });

        InvokeRepeating(nameof(UpdateGraphPerform), 0, 0.5f);
    }

    private List<GraphNode> GetNodesInBounds(Bounds bounds)
    {
        List<GraphNode> nodesInBounds = new();

        AstarPath.active.data.GetNodes(node =>
        {
            // Kiểm tra xem node có nằm trong bounds hay không
            if (bounds.Contains((Vector3)node.position))
            {
                nodesInBounds.Add(node);
            }
        });

        return nodesInBounds;
    }

    public void UpdateGraphPerform()
    {
    
        List<GraphNode> graphNodes = GetNodesInBounds(playerCollider.bounds);

        foreach (GraphNode node in graphNodes){
            node.Tag = 1;
        }

        StartCoroutine(ResetNode(graphNodes));

    }

    IEnumerator ResetNode(List<GraphNode> graphNodes){
        yield return new WaitForSeconds(3f);
        foreach(GraphNode node in graphNodes){
            if(!obstacleNodes.Contains(node)){
                node.Tag = 0;
            }
        }
    } 

}


















// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Pathfinding;

// public class UpdateGraph : MonoBehaviour
// {
//     Bounds playerBounds;
//     [SerializeField] Transform playerTransform;

//     [SerializeField] Vector3 offsetBound;
//     [SerializeField] CapsuleCollider2D playerCollider;
//     private void Start() {
//         InvokeRepeating(nameof(UpdateGraphPerform), 0, 1f);    
//         playerBounds = new Bounds(playerTransform.position, offsetBound);
//     }

//     public void UpdateGraphPerform(){
//         var graphToScan = AstarPath.active.data.gridGraph;
//         AstarPath.active.Scan(graphToScan);

//         var guo = new GraphUpdateObject(playerCollider.bounds);
//         guo.updatePhysics = true;
//         AstarPath.active.UpdateGraphs(guo);
//         StartCoroutine(ResetNode(guo.changedNodes));
//     }

//     IEnumerator ResetNode(List<GraphNode> graphNodes){
//         yield return new WaitForSecondsRealtime(1f);
//         foreach(GraphNode node in graphNodes){
//             node.Walkable = true;
//         }     
//     }
// }
