using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public GameObject testObject;
    private float totalTime;
    private float v, t;

    // Start is called before the first frame update
    void Start()
    {

        totalTime = 0.0f;
        t = 1.0f;
        v = 0.0025f;

    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime * t;
        if (totalTime > 1.0f || totalTime < 0.0f)
        {
            t *= -1.0f;
            v *= -1.0f;
        }
        testObject.transform.Translate(v, 0, 0, Space.World);

    }
}
