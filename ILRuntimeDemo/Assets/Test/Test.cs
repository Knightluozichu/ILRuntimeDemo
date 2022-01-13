using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject cube;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                var instance = Instantiate(cube,this.transform);
                //Destroy(instance);
            }

            stopwatch.Stop();
            print($"Milliseconds: {stopwatch.ElapsedMilliseconds}");
        }
    }
}
