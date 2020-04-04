using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkspeed = 2;//when gameobject move how much speed you want to move :)
    public float runspeed = 5; //when gameobject run how much speed you want to run :)
    Animator animator;//when we move the gameobject (walk animation or run animation) to work if you don't have anim that's look wired 
    public float turnSmoothTime = 0.3f;
    float turnSmoothVelocity;//
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    
    Transform cameraT;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();//when move gameobject with animation you will need this 
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//which direction move?//Vector2 means "how a GameObject can be moved to new positions" 
        Vector2 inputDir = input.normalized;//normalized that's for calculations only takes the direction of the vectors

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);//eulerAngles means three dimensional rotation (X,Y,Z)
        }
        bool running = Input.GetKey(KeyCode.LeftShift);//if we click the LeftShift button running anim will work
        float targetspeed = ((running) ? runspeed : walkspeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetspeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World); // moving a gameobject in the direction and distance of translation.

        float animationspeedPercent = ((running) ? 1 : 0.5f) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationspeedPercent, speedSmoothTime, Time.deltaTime);
    }
}
