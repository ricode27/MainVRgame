using UnityEngine;



public class RowingLocomotion : MonoBehaviour

{

    [Header("Assign These")]

    public Transform paddleTip;

    public Transform waterObject;



    [Header("Height Settings")]

    public float rideHeight = 0.5f;



    [Header("Stabilization Settings")]

    public float activationDepth = -0.15f;

    public float minMoveDistance = 0.05f;



    [Header("Water Settings")]

    public float bobbingHeight = 0.05f;

    public float bobbingSpeed = 0.8f;  



    [Header("Rowing Power")]

    public float rowingPower = 3f;      

    public float friction = 2.0f;    



    private Vector3 previousPosition;

    private Vector3 velocity;

    private bool wasInWater = false;

    private CharacterController characterController;



    void Start()

    {

        if (paddleTip != null) previousPosition = paddleTip.position;

        characterController = GetComponent<CharacterController>();

    }



    void Update()

    {

        if (paddleTip == null || characterController == null || waterObject == null) return;



        float currentWaterLevel = waterObject.position.y;



        // 1. STRICT WATER CHECK

        bool currentlyInWater = paddleTip.position.y < (currentWaterLevel + activationDepth);



        // Debug Lines

        if (currentlyInWater)

        {

            Debug.DrawLine(paddleTip.position, paddleTip.position + Vector3.up, Color.blue);

        }

        else

        {

            Debug.DrawLine(paddleTip.position, paddleTip.position + Vector3.up, Color.red);

        }



        // 2. ROWING LOGIC (Fixed)

        if (currentlyInWater)

        {

            // If we JUST entered the water, anchor the start position

            if (!wasInWater)

            {

                previousPosition = paddleTip.position;

            }



            Vector3 paddleMovement = paddleTip.position - previousPosition;

           

            // Only push if we've moved past the deadzone INSIDE the water

            if (paddleMovement.magnitude > minMoveDistance)

            {

                Vector3 pushDirection = new Vector3(-paddleMovement.x, 0, -paddleMovement.z);

                velocity += pushDirection * rowingPower * Time.deltaTime * 60f;

               

                // Update the anchor ONLY after a successful water stroke

                previousPosition = paddleTip.position;

            }

        }

        else

        {

            // IF IN THE AIR: Keep resetting the anchor so air-swings are completely ignored

            previousPosition = paddleTip.position;

        }



        // 3. FRICTION & MOVEMENT

        velocity = Vector3.Lerp(velocity, Vector3.zero, friction * Time.deltaTime);



        float yWave = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;

        float targetY = currentWaterLevel + rideHeight + yWave;



        Vector3 finalMove = velocity * Time.deltaTime;

        float currentY = transform.position.y;

        finalMove.y = targetY - currentY;



        characterController.Move(finalMove);



        wasInWater = currentlyInWater;

    }

}