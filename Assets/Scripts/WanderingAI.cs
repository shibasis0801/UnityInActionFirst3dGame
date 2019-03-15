using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
    private bool _alive;
    
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        // ToDo Modify this logic to create demo of Graph algorithms. :D :D :D :D
        if (_alive)
        {
            // Since these are local coordinates, always move forward in local coordinates.
            transform.Translate(0, 0, speed * Time.deltaTime);

            // A ray pointing from Enemy to forward facing direction.
            // There is no camera here, even if there was we must search for obstacles 
            // where the enemy can go, not where the enemy can see.
            var ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            // Sphere cast checks for intersection of a spherical ray instead of the thin RayCast used with bullets
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                var gameObject = hit.transform.gameObject;

                if (gameObject.GetComponent<PlayerCharacter>() != null)
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;

                        /*
                         * The next statements spawn a fireball just ahead of the enemy
                         * transform belongs to the enemy here. So TransformPoint converts
                         * the given point (Vector3.forward is short for (0, 0, 1))
                         * into world coordinates from the perspective(local coordinates) of the enemy.
                         */
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        // Same rotation as enemy.
                        _fireball.transform.rotation = transform.rotation;
                    } 
                    
                }
                else if (hit.distance < obstacleRange)
                {
                    // Randomly rotate the enemy and check again for obstacle on the next update cycle.
                    var angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool state)
    {
        _alive = state;
    }

    public bool GetAlive()
    {
        return _alive;
    }
}
