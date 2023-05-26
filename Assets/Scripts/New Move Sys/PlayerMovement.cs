using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private float speed;
    public float runSpeed = 20f;
    public float walkSpeed = 12f;
    public float crouchSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //Crouching Vignette
    //public PostProcessVolume postProcessVolume;

    public Volume Volume;
    public float crouchIntensity = 0.5f;
    public float lerpSpeed = 5f;
    //vigpublic Vignette vignette;
    private float targetIntensity;

    //VFX for sprint
    public VisualEffect sprintVisualEffect;

    //Ground check
    public Transform groundCheck;
    public float groundDistance = 2f;
    public LayerMask groundMask;    

    Vector3 velocity;
    bool isGrounded;


    void Start()
    {
        //GlobalVolume = GetComponent<Volume>();
        //GlobalVolume.profile.TryGet(out vignette);
        Volume volume = gameObject.GetComponent<Volume>();
        targetIntensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !(Input.GetKey(KeyCode.LeftControl)))
        { speed = runSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            //vignetterColor.value = new Color(0f, 255f, 255f);
            //vignette.color.Override(Color.red);
            targetIntensity = 0f;
            if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.S)))
            {
                sprintVisualEffect.enabled = true;
            }
            else
            {
                sprintVisualEffect.enabled = false;
            }
            
        }
        else if (Input.GetKey(KeyCode.LeftControl)) 
        { speed = crouchSpeed;
          transform.localScale = new Vector3(1, 0.5f, 1);
            //vignetterColor.value = new Color(0f, 0f, 0f);
            //vignette.color.Override(Color.black);
            targetIntensity = crouchIntensity;
            sprintVisualEffect.enabled = false;
        }
        else
        { speed = walkSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            targetIntensity = 0f;
            sprintVisualEffect.enabled = false;
        }

        //vignette.intensity.Override(Mathf.Lerp(vignette.intensity.value, targetIntensity, Time.deltaTime * lerpSpeed));

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed *Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
