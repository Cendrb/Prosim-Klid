using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Klid
{
    [Serializable]
    class Schema
    {
        private BlockWithRotation[][] blocksGrid;
        private int xSize;
        private int ySize;

        public Schema(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            blocksGrid = new BlockWithRotation[ySize][];
            for (int y = 0; y < ySize; y++)
                blocksGrid[y] = new BlockWithRotation[xSize];
            
        }

        public void Render(GameObject blocksGridGO, GameObject gridCellPrefab, Sprite defaultGrid)
        {
            RectTransform blocksGridRect = blocksGridGO.GetComponent<RectTransform>();
            float size = blocksGridRect.rect.width / xSize;
            for(int y = 0; y < ySize; y++)
            {
                gridCellPrefab.InstantiateEnumerable<BlockWithRotation>(blocksGrid[y], blocksGridGO, new Vector2(0, 0), RectTransform.Axis.Horizontal, new Vector2(0, 0), new UnityEngine.Events.UnityAction<GameObject, BlockWithRotation>((go, b) => initializeGridCell(go, b, defaultGrid)));
            }
        }

        private void initializeGridCell(GameObject cell, BlockWithRotation block, Sprite defaultGrid)
        {
            Image image = cell.GetComponent<Image>();
            if (block == null)
                image.sprite = defaultGrid;
            else
                image.sprite = block.Block.Sprite;
        }
    }
}
