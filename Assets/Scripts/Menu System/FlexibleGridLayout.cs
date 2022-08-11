using UnityEngine;
using UnityEngine.UI;

namespace MenuAsset
{
    /*
     * Code by Kristopher Kath
     */


    [ExecuteAlways]
    public class FlexibleGridLayout : LayoutGroup
    {
        public enum FitType
        {
            Uniform,
            Width,
            Heigth,
            FixedRows,
            FixedColumns
        }

        [Tooltip("The layout type to fit grid into.")]
        [SerializeField] private FitType fitType;
        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private Vector2 spacing;

        [SerializeField] private Vector2 cellSize;
        private bool fitX;
        private bool fitY;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            if (fitType == FitType.Heigth || fitType == FitType.Uniform || fitType == FitType.Width)
            {
                fitX = true;
                fitY = true;

                //calculate rows and columns by number of children
                float sqrt = Mathf.Sqrt(transform.childCount);
                rows = Mathf.CeilToInt(sqrt);
                columns = Mathf.CeilToInt(sqrt);
            }

            if (fitType == FitType.Width || fitType == FitType.FixedColumns)
            {
                rows = Mathf.CeilToInt(transform.childCount / (float)columns);
            }
            if (fitType == FitType.Heigth || fitType == FitType.FixedRows)
            {
                columns = Mathf.CeilToInt(transform.childCount / (float)rows);
            }


            //get parent width/height
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            //get children width/height
            float cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
            float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * (columns - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

            //set children width/height
            cellSize.x = fitX ? cellWidth : cellSize.x;
            cellSize.y = fitY ? cellHeight : cellSize.y;

            int colCount = 0;
            int rowCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                colCount = i % columns;
                rowCount = i / columns; //divide by zero

                var item = rectChildren[i];

                var xPos = (cellSize.x * colCount) + (spacing.x * colCount) + padding.left;
                var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical()
        {

        }

        public override void SetLayoutHorizontal()
        {

        }

        public override void SetLayoutVertical()
        {

        }
    }
}


