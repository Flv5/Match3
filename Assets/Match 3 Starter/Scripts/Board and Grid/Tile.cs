//https://www.raywenderlich.com/673-how-to-make-a-match-3-game-in-unity
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private static Tile previousSelected = null;

    private SpriteRenderer render;
    private bool isSelected = false;

    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    bool isSwaping;
    Vector3 currentStartingPos;
    Vector3 tileToMoveWithStartingPos;
    GameObject tileToMoveWith;
    public float speed;
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        isSwaping = false;
    }

    private void Select()
    {
        isSelected = true;
        render.color = selectedColor;
        previousSelected = gameObject.GetComponent<Tile>();
    }

    private void Deselect()
    {
        isSelected = false;
        render.color = Color.white;
        previousSelected.isSelected = false;
        previousSelected.render.color = Color.white;
        previousSelected = null;
    }

    private void OnMouseDown() //If Clicked
    {
        if (render.sprite == null || isSwaping)
            return;
        if (isSelected)
            Deselect();
        else
        {
            if (previousSelected == null)
                Select();
            else
            {
                SwapSprite(previousSelected);
            }
        }
    }

    private void Update()
    {
        if (isSwaping)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, tileToMoveWithStartingPos, step);
            tileToMoveWith.transform.position = Vector3.MoveTowards(tileToMoveWith.transform.position, currentStartingPos, step);
            //Have tile arrive to their destination
            if (transform.position.x == tileToMoveWithStartingPos.x  && transform.position.y == tileToMoveWithStartingPos.y &&
                tileToMoveWith.transform.position.x == currentStartingPos.x && tileToMoveWith.transform.position.y == currentStartingPos.y)
            {
                isSwaping = false;
                Deselect();
            }
        }
    }

    public void SwapSprite(Tile b)
    {
        //if (render.sprite == b.GetComponent<SpriteRenderer>().sprite)
        //    return;

        //Populating variables for swapping
        if (!isSwaping)
        {
            tileToMoveWithStartingPos =  b.transform.position;
            tileToMoveWith = b.gameObject;
            currentStartingPos = transform.position;
            isSwaping = true;
        }
    }
}