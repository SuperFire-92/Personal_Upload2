using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float xDirection, yDirection;
    public float speed;
    public GameObject background;
    public bool destroy = false;

    private void Start()
    {
        if (transform.position.x == 5)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90 + Random.Range(-20, 20));
        }
        else if (transform.position.x == -5)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90 + Random.Range(-20, 20));
        }
        else if (transform.position.y == 4)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180 + Random.Range(-20, 20));
        }
        else if (transform.position.y == -4)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0 + Random.Range(-20, 20));
        }

        background = GameObject.FindGameObjectWithTag("Background");
    }

    private void FixedUpdate()
    {
        xDirection = -Mathf.Sin(GetComponent<Transform>().eulerAngles.z * Mathf.Deg2Rad);
        yDirection = Mathf.Cos(GetComponent<Transform>().eulerAngles.z * Mathf.Deg2Rad);
        transform.position = new Vector3(transform.position.x + (xDirection * speed), transform.position.y + (yDirection * speed), transform.position.z);

        if (!GetComponent<Collider2D>().IsTouching(background.GetComponent<Collider2D>()))
        {
            destroy = true;
        }
        
    }
}
