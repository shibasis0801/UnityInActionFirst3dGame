using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        // Try to create this functionality in Android EventApp with modules and inline functions
        _camera = GetComponent<Camera>();

        // Now we will be having a target reticule, so the windows Mouse Pointer must be hidden.
        // There is a cursor inside the game and the Windows cursor.
        // lockstate disables movement from Windows cursor.
        Cursor.lockState = CursorLockMode.Locked;
        // Hides the windows cursor.
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // This is the point through which the ray will be cast after starting from the camera
            var point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
    
            // This helper function creates a ray starting from cameraPosition to the point we gave
            Ray ray = _camera.ScreenPointToRay(point);
            
            // This will store the object it hits, along with the point it hits the object. ( where this ray intersects a polygon of the object )
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                
                // The reactive target is a script that will have enemy behaviour
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    /*
                     * This function will create a sphere where the bullet hits.
                     * To understand this, have to study coroutines well.
                     * Notice that there is a function call here, instead of passing function name
                     * and parameters separately.
                     * The StartCoroutine function gets a IEnumerator instance to do something with.
                     * Study JonSkeet and Microsoft C# books to understand.
                     */
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    /*
     * This method is called after Rendering is done.
     * Rendering is the process of calculating the visible 2D portion of the 3D gameobjects
     * This function creates views that act like an overlay(on top of).
     */
    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    private IEnumerator SphereIndicator(Vector3 point)
    {
        // A sphere is created dynamically at the point the ray hits.
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = point;
        
        // This yield statement pauses this function for one second.
        yield return new WaitForSeconds(1);
        
        // On resuming, the sphere is destroyed.
        Destroy(sphere);
        
        // Have to study coroutines to understand this. May work like an event loop. Maybe.
    }
}
