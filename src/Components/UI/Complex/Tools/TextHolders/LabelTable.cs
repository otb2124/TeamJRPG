using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class LabelTable : UIComposite
    {

        public string[][] tableData;
        public Vector2 cellSize;

        public LabelTable(string[][] tableData, Vector2 startPosition, Vector2 cellSize) 
        {
            this.tableData = tableData;
            this.position = startPosition;
            this.cellSize = cellSize;


            for (int row = 0; row < tableData.Length; row++)
            {
                for (int col = 0; col < tableData[row].Length; col++)
                {
                    if (tableData[row][col] != null)
                    {
                        // Calculate the position for each label based on row and column
                        Vector2 labelPosition = new Vector2(
                            position.X + col * cellSize.X,
                            position.Y + row * cellSize.Y
                        );

                        // Create a new Label with the text from the tableData
                        Label label = new Label(tableData[row][col], labelPosition, 0, Color.White, null);
                        children.Add(label);
                    }
                }
            }
        }
    }
}
