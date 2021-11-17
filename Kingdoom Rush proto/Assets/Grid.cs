using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid {

    private int height;
    private int width;
    private float cellsize;
    private int[,] gridArray;
    private Vector3 originPosition;

    public const int sortingOrderDefault = 5000;

    public Grid(int height, int width, float cellsize, Vector3 originPosition)
    {
        this.height = height;
        this.width = width;
        this.cellsize = cellsize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];

        for(int x=0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Debug.Log(x + ", " + y);
                CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellsize, cellsize)*0.5f, 10, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellsize + originPosition;
    }

    private void getXY(Vector3 pos, out int x, out int y)
    {
        x = Mathf.FloorToInt((pos - originPosition).x / cellsize);
        y = Mathf.FloorToInt((pos - originPosition).y / cellsize);
    }

    public void setValue(int x, int y, int value)
    {
        if(x>=0 && y>=0 && x<width && y<height)
        {
            gridArray[x, y] = value;
        }   
    }

    public void setValue(Vector3 pos, int value)
    {
        int x, y;
        getXY(pos, out x, out y);
        setValue(x, y, value);
    }

    public int getValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        } else
        {
            return -1;
        }
    }

    public int getValue(Vector3 pos)
    {
        int x, y;
        getXY(pos, out x, out y);
        return gridArray[x, y];
    }


    // Create Text in the World
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
