using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private float speed;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        angle = Random.Range(0f, Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(
            speed * Mathf.Cos(angle) * Time.deltaTime,
            speed * Mathf.Sin(angle) * Time.deltaTime,
            0f
        );
    }
}
