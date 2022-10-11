using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMover : MonoBehaviour
{
    public bool isActive = true;
    private new Rigidbody rigidbody = null;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void MoveTo(Vector3 newPosition)
    {
        if(isActive)
        {
            rigidbody.MovePosition(newPosition);
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
