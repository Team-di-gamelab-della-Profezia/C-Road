using UnityEngine;

public class Rotation : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            transform.Rotate(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {

            transform.Rotate(-0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            transform.Rotate(-0, 90, 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            transform.Rotate(-0, 270, 0);
        }
    }
}
