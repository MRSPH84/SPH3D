using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    public float rotationSpeed = 2f;
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Quaternion targetRotation = Quaternion.Euler(0,90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Quaternion targetRotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }



    }
}