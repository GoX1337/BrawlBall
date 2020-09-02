using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDPSend;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Vector3 lastPosition;
    public Quaternion lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(Vector3.zero);
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        FocusMouseCursor();
        SendLastPlayerPosition();
    }

    void MoveCharacter()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void FocusMouseCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = transform.position;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Vector3 hitpoint = hit.point;
            transform.LookAt(hitpoint);
            Vector3 targetPostition = new Vector3(hitpoint.x, this.transform.position.y, (hitpoint.z));
            this.transform.LookAt(targetPostition);

        }
    }

    void SendLastPlayerPosition()
    {
        if (!transform.position.Equals(lastPosition) || !transform.rotation.Equals(lastRotation))
        {
            UDPSender.getInstance().sendString(transform.position + " " + transform.rotation);
            Debug.Log("Send UDP msg");
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }
}
