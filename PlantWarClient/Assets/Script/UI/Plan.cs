using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan : MonoBehaviour
{
    float speed = 800;
    GameObject myPlan;
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            isMoving = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
        GoMove();
    }

    void GoMove() {
        if (isMoving)
        {
            Vector3 pos = Input.mousePosition;
            Vector3 disVal = pos - transform.position;
            if (disVal.magnitude >10)
            {
                float moveSpeed = disVal.magnitude / speed;
                disVal = disVal / moveSpeed;
                transform.position += (disVal * Time.deltaTime);
            }
        }
    }
}
