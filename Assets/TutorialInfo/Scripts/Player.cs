using UnityEngine;

public class Player : MonoBehaviour
{
    public int Speed;
    public int jump;
    Rigidbody myRig;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       myRig = GetComponent <Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0, 0, Speed * Time.deltaTime));

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(0, 0, Speed * Time.deltaTime));

        }
        if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 0, Speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S))
            {
            transform.Translate(new Vector3(0, 0, Speed * Time.deltaTime));
        }
            if(Input.GetKeyDown(KeyCode.Space))
        {
            myRig.angularVelocity = new Vector3(myRig.angularVelocity.x, jump, myRig.angularVelocity.z);
        }

    }
}
