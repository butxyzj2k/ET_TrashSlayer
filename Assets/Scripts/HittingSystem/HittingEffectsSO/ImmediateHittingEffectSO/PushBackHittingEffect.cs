using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/PushBackHittingEffect")]
public class PushBackHittingEffect : ImmediateHittingEffectSO
{
    [SerializeField] float force;
    
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        if(_receiver.GetComponent<IHaveHealth>().CurrentHealth <= 0 || !_receiver.GetComponent<ObjectMovement>()) return;
        Vector3 dirForce = (_receiver.transform.position - sender.transform.position).normalized;
        SceneGameManager.instance.StartCoroutine(AddForceToReceiver(sender, _receiver, dirForce));
    }

    IEnumerator AddForceToReceiver(GameObject sender, GameObject _receiver, Vector3 dirForce){
        float time = 0;
        while(time < _receiver.GetComponent<ObjectGetClipIn4>().hurtTime){
            time += Time.deltaTime;
            _receiver.GetComponent<Rigidbody2D>().MovePosition(_receiver.transform.position + force * Time.deltaTime * dirForce);
            yield return null;
        }
    }
}
