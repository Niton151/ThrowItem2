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

    [SerializeField]
    private Material sunnySky;

    [SerializeField]
    private Material spaceSky;
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
        if(OVRInput.GetDown(OVRInput.RawButton.B) && other.gameObject.CompareTag("Hand"))
        {
            if (BaseToCraft)
            {
                Tutorial.IntoIsCraftTell(true);
                player.transform.position = craftPos.position + new Vector3(0, 0.5f, 0);
                RenderSettings.skybox = spaceSky;
                
            }
            else
            {
                RenderSettings.skybox = sunnySky;
                Tutorial.IntoIsReturnBase(true);
                player.transform.position = basePos.position + new Vector3(0, 0.5f, 0);
            }
        }
    }
}
