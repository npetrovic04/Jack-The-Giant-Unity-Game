using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float speed = 0.7f;
    private float acceleration = 0.15f;
    private float maxSpeed = 1.5f;

    private float easySpeed = 1.5f;
    private float mediumSpeed = 2f;
    private float hardSpeed = 2.5f;

    //[HideInInspector]
    public bool moveCamera;

	
	void Start ()
    {
        if(GamePreferenceses.GetEasyDifficulty()== 1)
        {
            maxSpeed = easySpeed;
        }

        if (GamePreferenceses.GetMediumDifficulty() == 1)
        {
            maxSpeed = mediumSpeed;
        }

        if (GamePreferenceses.GetHardDifficulty() == 1)
        {
            maxSpeed = hardSpeed;
        }

        moveCamera = true;
	}
	
	void Update ()
    {
        if (moveCamera)
        {
            MoveCamera();
        }
	}

    void MoveCamera()
    {
        Vector3 temp = transform.position;

        float oldY = temp.y;

        float newY = temp.y - (speed * Time.deltaTime);

        temp.y = Mathf.Clamp(temp.y, oldY, newY);

        transform.position = temp;

        speed += acceleration * Time.deltaTime;

        if (speed > maxSpeed)
            speed = maxSpeed;
    }
}
