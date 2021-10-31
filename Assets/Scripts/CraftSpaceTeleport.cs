using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSpaceTeleport : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform craftPos;

    [SerializeField]
    private Transform basePos;

    [SerializeField]
    private bool BaseToCraft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(/*OVRInput.GetDown(OVRInput.RawButton.A) && */other.gameObject.CompareTag("Hand"))
        {
            if (BaseToCraft)
            {
                Tutorial.IntoIsCraftTell(true);
                player.transform.position = craftPos.position;
            }
            else
            {
                Tutorial.IntoIsReturnBase(true);
                player.transform.position = basePos.position;
            }
        }
    }
}
