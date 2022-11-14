using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosistipn;
    public int swipeAngle = 0;

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnMouseDown()
    {
        firstTouchPosition = Input.mousePosition;
        Debug.Log(firstTouchPosition);
    }
}
