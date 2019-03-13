using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make component mandatory
[RequireComponent(typeof(CharacterController))]
// Add this to addComponent menu under Control Script option.
[AddComponentMenu("Control Script/FPSInput")]
public class FPSInput : MonoBehaviour
{
    private CharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }


    public float speed = 6.0f;

    public float gravity = -9.8f;
    // Update is called once per frame
    void Update()
    {
        /**
         * Since update is called every frame,
         * fps is dependent on processing speed,
         * hence, player movement will be dependent on processing speed.
         *
         * To scale this across PCs of different speed,
         * we multiply Time.deltaTime
         * at 30 fps it is 1/30,
         * at 60 fps it is 1/60
         */

        /* Older Code which skips collision detection due to directly changing transform
         
        var deltaX = Input.GetAxis("Horizontal") * speed;
        var deltaZ = Input.GetAxis("Vertical") * speed;

        var computerSpeedInvariantDeltaX = deltaX * Time.deltaTime;
        var computerSpeedInvariantDeltaZ = deltaZ * Time.deltaTime;

        transform.Translate(computerSpeedInvariantDeltaX, 0, computerSpeedInvariantDeltaZ);
      
        */

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        var movement = new Vector3(deltaX, 0, deltaZ);

        // Make |movement| <= speed
        movement = Vector3.ClampMagnitude(movement, speed);
        
        // Apply gravity to prevent player from Flying
        movement.y = gravity;
        
        // Make PC speed invariant, vector multiplication by scalar
        movement = movement * Time.deltaTime;
        
        // Convert local coordinates to global coordinates
        movement = transform.TransformDirection(movement);
        
        // Actually move the character
        _characterController.Move(movement);
    }
}
