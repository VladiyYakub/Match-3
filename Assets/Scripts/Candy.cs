using UnityEngine;

public class Candy : MonoBehaviour

{
    public int column;
    public int row;
    public int targetX;
    public int targetY;
    private Board board;
    private GameObject otherCandy;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;

    private Camera _cam;

    public void Init(Camera cam, Board board)
    {
        _cam = cam;

        this.board = board;
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
    }

    void Update()
    {
        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //MOve Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allCandies[column, row] = this.gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //MOve Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allCandies[column, row] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(firstTouchPosition);
    }

    private void OnMouseUp()
    {
        finalTouchPosition = _cam.ScreenToWorldPoint(Input.mousePosition); ;
        CalculateAngle();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        Debug.Log(swipeAngle);
        MovePieces();

    }

    void MovePieces()
    {

        if(swipeAngle > -45 && swipeAngle <= 45 && column < board.x_widht)
        {
            //Right Swipe
            otherCandy = board.allCandies[column + 1, row];
            otherCandy.GetComponent<Candy>().column -= 1;
            column += 1;

        } 
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.y_height)
        {
            //Up Swipe
            otherCandy = board.allCandies[column, row + 1];
            otherCandy.GetComponent<Candy>().row -= 1;
            row += 1;

        }
        else if (swipeAngle > 135 ||  swipeAngle <= -135 && column > 0)
        {
            //Left Swipe
            otherCandy = board.allCandies[column - 1, row];
            otherCandy.GetComponent<Candy>().column +=1;
            column -= 1;

        } 
        else if (swipeAngle < -45 && swipeAngle >= -135 && row >0)
        {
            //Down Swipe
            otherCandy = board.allCandies[column, row - 1];
            otherCandy.GetComponent<Candy>().column +=1;
            column -= 1;

        } 
    }
}
