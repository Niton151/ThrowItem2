using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
    private Transform craftPos;
    //This script is responsible for what happens when the pullable objects reach the core
    //by default, the game objects are simply turned off
    //as this is much more performant than destroying the objects
    void OnTriggerStay (Collider other) {
        if(other.transform.root.gameObject.GetComponent<SingularityPullable>()){
            other.transform.root.gameObject.transform.position = craftPos.position + new Vector3(0, 1f, 0);
        }
    }

    void Awake(){
        craftPos = transform.root.GetComponent<TeleportGrenade>().GetCraftPos();
        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
}
