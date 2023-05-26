using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpText : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform2;
    [SerializeField] private Transform objectGrabPointTransform2;
    [SerializeField] private LayerMask pickUpLayerMask2;
    public GameObject PickUpText2;
    float pickUpDistance = 2f;

    private ObjectGrabable objectGrabable;

    public void Awake()
    {
        PickUpText2.SetActive(false);
    }
    private void Update()
    {

        //raycast to see if looking at object and prompt pick up text
        if (objectGrabable == null)
        {
            // not  carrying an obj, try to grab

            if (Physics.Raycast(playerCameraTransform2.position, playerCameraTransform2.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask2))
            {
                if (raycastHit.transform.TryGetComponent(out objectGrabable))
                {
                    PickUpText2.SetActive(true);
                    if (Input.GetKey(KeyCode.E))
                    {
                        PickUpText2.SetActive(false);
                    }
                }
            }
            else //if raycast doesnt detect any grabable object, dont display pickup text
            {
                PickUpText2.SetActive(false);
            }
        }
        else
        {
            //Carrying object, drop
            //objectGrabable.Drop();
            objectGrabable = null;
            //PickUpText2.SetActive(false);
        }


    }
}
