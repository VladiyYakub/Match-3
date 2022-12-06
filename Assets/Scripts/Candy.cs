using System.Collections;
using UnityEngine;

public class Candy : MonoBehaviour
{
    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;

    private Board board;
    private GameObject otherCandy;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;
    public float swipeResist = 1f;
    private Camera _cam;    

    public void Init(Camera cam, Board board)
    {
        _cam = cam;
        this.board = board;
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousRow = row;
        //previousColumn = column;
    }

    void Update()
    {
        FindMetches();
        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
        }

        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //MOve Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allCandies[column,row] != this.gameObject)
            {
                board.allCandies[column, row] = this.gameObject;
            }
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
            //Move Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allCandies[column, row] != this.gameObject)
            {
                board.allCandies[column, row] = this.gameObject;
            }
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;           
        }
    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if(otherCandy != null)
        {
            if(!isMatched && !otherCandy.GetComponent<Candy>().isMatched)
            {
                otherCandy.GetComponent<Candy>().row = row;
                otherCandy.GetComponent<Candy>().column = column;
                row = previousRow;
                column = previousColumn;
            }
            else
            {
                board.DestroyMatches();
            }
            otherCandy = null;
        }       
    }

    private void OnMouseDown()
    {
        firstTouchPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(firstTouchPosition);
    }

    private void OnMouseUp()
    {
        finalTouchPosition = _cam.ScreenToWorldPoint(Input.mousePosition); ;
        CalculateAngle();
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
        }        
    }

    void MovePieces()
    {
        if(swipeAngle > -45 && swipeAngle <= 45 && column < board.x_widht-1)
        {
            //Right Swipe
            otherCandy = board.allCandies[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherCandy.GetComponent<Candy>().column -= 1;
            column += 1;
        } 
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.y_height-1)
        {
            //Up Swipe
            otherCandy = board.allCandies[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherCandy.GetComponent<Candy>().row -= 1;
            row += 1;
        }
        else if (swipeAngle > 135 ||  swipeAngle <= -135 && column > 0)
        {
            //Left Swipe
            otherCandy = board.allCandies[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherCandy.GetComponent<Candy>().column +=1;
            column -= 1;
        } 
        else if (swipeAngle < -45 && swipeAngle >= -135 && row >0)
        {
            //Down Swipe
            otherCandy = board.allCandies[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherCandy.GetComponent<Candy>().row +=1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    void FindMetches()
    {
        if (column > 0 && column < board.x_widht - 1)
        {
            GameObject leftCandy1 = board.allCandies[column - 1, row];
            GameObject rightCandy1 = board.allCandies[column + 1, row];
            if(leftCandy1 !=null & rightCandy1 !=null ) 
            {
                if (leftCandy1.tag == this.gameObject.tag && rightCandy1.tag == this.gameObject.tag)
                {
                    leftCandy1.GetComponent<Candy>().isMatched = true;
                    rightCandy1.GetComponent<Candy>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.y_height - 1)
        {
            GameObject upCandy1 = board.allCandies[column, row +1];
            GameObject downCandy1 = board.allCandies[column, row -1];
            if(upCandy1 !=null & downCandy1 != null)
            {
                if (upCandy1.tag == this.gameObject.tag && downCandy1.tag == this.gameObject.tag)
                {
                    upCandy1.GetComponent<Candy>().isMatched = true;
                    downCandy1.GetComponent<Candy>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }
}
