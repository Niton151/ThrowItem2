using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
    private Transform craftPos;
    //This script is responsible for what happens when the pullable objects reach the core
    //by default, the game objects are simply turned off
    //as this is much more performant than destroying the objects
    void OnTriggerStay (Collider other) {
        if(other.GetComponent<SingularityPullable>()){
            other.gameObject.transform.position = craftPos.position;
        }
    }

    void Awake(){
        craftPos = transform.root.GetComponent<TeleportGrenade>().GetCraftPos();
        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
}
