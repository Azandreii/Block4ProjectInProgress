using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    float pickUpDistance = 2f;

    private ObjectGrabable objectGrabable;


    private void Update()
    {

        //check if pressed "E" while lookin at grabable obj to pick up
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabable == null) { 
                // not  carrying an obj, try to grab
                
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabable)) {
                        objectGrabable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabable);
                    }
                }
            } else
            {
                //Carrying object, drop
                objectGrabable.Drop();
                objectGrabable = null;
            }
        }
            
    }
}
