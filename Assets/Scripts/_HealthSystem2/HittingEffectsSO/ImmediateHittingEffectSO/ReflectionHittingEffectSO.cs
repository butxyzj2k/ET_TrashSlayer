using System.Collections;
using System.Collections.Generic;
using System.Linq;

// using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/ReflectionHittingEffectSO")]
public class ReflectionHittingEffectSO : ImmediateHittingEffectSO
{
    [SerializeField] private float radiusToCheckReflect;

    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        if(_receiver.CompareTag("Enemy")){
            ReflectEnemy(sender, _receiver);
        }
        else{
            ReflectNormal(sender, _receiver);
        }
    }

    void ReflectEnemy(GameObject sender, GameObject _receiver){

        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesList = new();

        for(int i = 0; i < enemiesArray.Length; i++){
            if(enemiesArray[i] != _receiver && enemiesArray[i].GetComponent<DynamicObjectHitting>()) 
            {
                GetSideCollison.Side side1 = GetSideCollison.GetSide(_receiver.transform.position, enemiesArray[i]);
                GetSideCollison.Side side2 = GetSideCollison.GetSide(_receiver.transform.position, sender);
                if(Vector3.Distance(enemiesArray[i].transform.position, _receiver.transform.position) <= radiusToCheckReflect && side1 == side2){
                    enemiesList.Add(enemiesArray[i]);
                }
            }
        }
        
        if(enemiesList.Count > 0){
            int randomEnemy = UnityEngine.Random.Range(0, enemiesList.Count);
            Vector3 newDir = (enemiesList[randomEnemy].transform.position - sender.transform.position).normalized;
            sender.transform.up = newDir;
        }

        else{
            ReflectNormal(sender, _receiver);
        }
    }

    void ReflectNormal(GameObject objectReflect, GameObject surfaceReflect){
        Vector3 newDir = GetNewDirOfReflection(objectReflect, surfaceReflect);
        objectReflect.transform.up = newDir;
    }

    Vector3 GetNewDirOfReflection(GameObject sender, GameObject _receiver){
        Vector3 a = sender.transform.up.normalized;
        Vector3 n = GetNormalizeVectorOfSurface(sender, _receiver);
        return a - 2 * Vector3.Dot(a, n) * n ;
    }

    Vector3 GetNormalizeVectorOfSurface(GameObject sender, GameObject _receiver){
        GetSideCollison.Side side = GetSideCollison.Side.none;
        if(_receiver.GetComponent<Collider2D>()) side = GetSideCollison.GetSide(_receiver.GetComponent<Collider2D>().ClosestPoint(sender.transform.position), sender);
        else if(_receiver.GetComponentInChildren<Collider2D>())  side = GetSideCollison.GetSide(_receiver.GetComponentInChildren<Collider2D>().ClosestPoint(sender.transform.position), sender);

        return side switch
        {
            GetSideCollison.Side.up => new Vector3(0, 1, 0),
            GetSideCollison.Side.down => new Vector3(0, -1, 0),
            GetSideCollison.Side.left => new Vector3(-1, 0, 0),
            GetSideCollison.Side.right => new Vector3(1, 0, 0),
            _ => new Vector3(0, 0, 0),
        };
    }
}
