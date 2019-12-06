
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Board : MonoBehaviour
{
    public List<Sprite> iconList;
    bool isSwaping;
    public float speed;
    public int numColumns;
    public int numRows;
    private GameObject[,] tiles;
    public GameObject tile;
    public float xOffset;
    public float yOffset;

    private void Start()
    {
        GenerateMap();
    }
    void GenerateMap()
    {
        tiles = new GameObject[numColumns, numRows];
        Sprite previousLeft = null;//These are for duplicates when map generation
        Sprite below = null;                //These are for duplicates when map generation
        if (numColumns > 0 && numRows > 0)
        {
            for (int y = 0; y < numRows; ++y)
            {
                for (int x = 0; x < numColumns; ++x)
                {
                    var deltaPos = new Vector3(transform.position.x + (xOffset * x), transform.position.y + (yOffset * y));
                    tiles[x, y] = Instantiate(tile, deltaPos, Quaternion.identity, gameObject.transform);
                    List<Sprite> possibleCharacters = new List<Sprite>();
                    possibleCharacters.AddRange(iconList); // add the iconlist elements into possibleCharacters;

                    if(x > 0)
                        below = tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite;
                    if (y > 0)
                        below = tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite;
                    if (previousLeft)
                        possibleCharacters.Remove(previousLeft);
                    if(below)
                        possibleCharacters.Remove(below);
                    
                    Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
                    tiles[x,y].GetComponent<SpriteRenderer>().sprite = newSprite;
                    //if(newSprite != null)
                    //    previousLeft = newSprite;
                } 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwaping)
        {
            //float step = speed * Time.deltaTime; // calculate distance to move
            //milk.transform.position = Vector3.Lerp(milk.transform.position, toChocolate, step);
            //chocolote.transform.position = Vector3.Lerp(chocolote.transform.position, toMilk, step);
        }
    }
}
