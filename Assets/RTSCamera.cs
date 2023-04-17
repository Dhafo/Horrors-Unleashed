using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class RTSCamera : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField]
    float panSpeed;
    [SerializeField]
    float panTime;
 
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
    }

    public Vector3 PanScreen() 
    {
        Vector3 direction = Vector3.zero;

        //
        

        Debug.Log(direction);
        return direction;
    }

    void HandleMovementInput() 
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        if (x != 0 || y != 0)
        {
            if (y >= Screen.height * .95f) { pos += (transform.forward * panSpeed); }
            else if (y <= Screen.height * .05f) { pos -= (transform.forward * panSpeed); }
            if (x >= Screen.width * .95f) { pos += (transform.right * panSpeed); }
            else if (x <= Screen.width * .05f) { pos -= (transform.right * panSpeed); }
        }
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            pos += (transform.forward * panSpeed);
        }
        else if(Input.GetKey(KeyCode.DownArrow)) 
        {
            pos -= (transform.forward * panSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            pos += (transform.right * panSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            pos -= (transform.right * panSpeed);
        }
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * panTime);
    }
}
