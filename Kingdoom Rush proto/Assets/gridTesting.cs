using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridTesting : MonoBehaviour
{
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(4,8, 2f, new Vector3(-10, -10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
