/*FlexibleGridLayout - A BETTER GRID LAYOUT THAN UNITY GRID (NOT OUR CODE)
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * https://www.youtube.com/watch?v=CGsEJToeXmA&t=3s <- Code from this tutorial
 *  Handles some UI organization.
 * 
 *  Unity support packages
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public FitType fitType;

    public int rows;
    public int columns;

    public Vector2 cellSize;
    public Vector2 spacing;

    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputVertical()
    {
        

    }

    public override void SetLayoutHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if(fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform) {

            fitX = true;
            fitY = true;

            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;
        //Original Youtube line: float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);

        //Fixed:  ((spacing.x / (float)columns) * 2) changed 2 to (columns-1)
        float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * (columns-1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
        //Fixed:  ((spacing.x / (float)rows) * 2) changed 2 to (rows-1)
        float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * (rows - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void SetLayoutVertical()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
