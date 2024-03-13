using UnityEngine;

public class Rotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Rotate the object on X, Y, and Z axes by specified amounts, adjusted for frame rate.
        transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);
    }

}