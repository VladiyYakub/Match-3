using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] dots;
<<<<<<< HEAD
    void Start()
=======
    private void Start()
    {
        Initialize();
    }
    private void Update()
>>>>>>> developer
    {
        Initialize();  
    }
<<<<<<< HEAD

    
    void Update()
=======
    void Initialize()
>>>>>>> developer
    {
        int dotTOUse = Random.Range(0, dots.Length);
        GameObject dot = Instantiate(dots[dotTOUse], transform.position, Quaternion.identity);
        dot.transform.parent = transform;
        dot.name = gameObject.name;
    }
    void Initialize()
    {
        int dotToUse = Random.Range(0, dots.Length);
        GameObject dot = Instantiate(dots[dotToUse], transform.position, Quaternion.identity);
        dot.transform.parent = transform;
        dot.name = gameObject.name;
    }
}

