using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBubble : MonoBehaviour
{
    private const float FlySpeed = 160;

    private const float LifeTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += FlySpeed * transform.right * -1 * Time.deltaTime;
    }
}