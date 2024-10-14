using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MultiDirAttackExtensionCode{

    public struct NewTransform
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    public static List<NewTransform> CreateMultiDir(Transform startPoint, Vector3 target, float numDir, float angleBetweenDir, float offsetDis, AttackPatternSO attackPatternSO)
    {
        Vector3 defaultPos = startPoint.position;
        Quaternion defaultRota = attackPatternSO.GetDir(startPoint, target);
        List<NewTransform> newTransforms = new();

        for (int i = 0; i <= numDir / 2; i++)
        {
            // Tạo và thêm transform đối xứng
            void AddTransform(float angle)
            {
                NewTransform newTransform = new NewTransform
                {
                    rotation = defaultRota * Quaternion.Euler(0, 0, angle),
                    position = defaultPos + (defaultRota * Quaternion.Euler(0, 0, angle)) * new Vector3(0, offsetDis, 0)
                };
                newTransforms.Add(newTransform);
            }

            // Xử lý trường hợp góc 0 hoặc là góc cuối cùng
            if (i == 0 || i == numDir / 2)
            {
                AddTransform(i * angleBetweenDir);
            }
            else
            {
                AddTransform(i * angleBetweenDir);
                AddTransform(-i * angleBetweenDir);
            }
        }

        return newTransforms;
    }

    public static IEnumerator RotateNewBarrels(Transform startPoint, float angleSpin, float distOffset, float rotateRate ,List<NewTransform> newBarrels){
        if(startPoint.GetComponentInParent<ObjectAttack>()){
            while(startPoint.GetComponentInParent<ObjectAttack>().IsAttack){
                for(int i = 0; i < newBarrels.Count; i++){
                    NewTransform newBarrel = newBarrels[i];
                    newBarrel.rotation *= Quaternion.Euler(0, 0, angleSpin);
                    newBarrel.position =  startPoint.position +  newBarrel.rotation * new Vector3(0, distOffset, 0);
                    newBarrels[i] = newBarrel;
                    yield return new WaitForSeconds(rotateRate);
                    if(!startPoint.transform.parent.gameObject.activeInHierarchy) yield break;
                }
            }
        }
        else{
             while(startPoint.gameObject.activeInHierarchy){
                for(int i = 0; i < newBarrels.Count; i++){
                    NewTransform newBarrel = newBarrels[i];
                    newBarrel.rotation *= Quaternion.Euler(0, 0, angleSpin);
                    newBarrel.position =  startPoint.position +  newBarrel.rotation * new Vector3(0, distOffset, 0);
                    newBarrels[i] = newBarrel;
                    yield return new WaitForSeconds(rotateRate);
                }
            }
        }
    }
}