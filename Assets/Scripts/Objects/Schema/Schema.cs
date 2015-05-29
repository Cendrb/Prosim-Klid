using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Schema
{
    class Schema
    {
        private BlockWithRotation[,] blocksGrid;

        public Schema(int xSize, int ySize)
        {
            blocksGrid = new BlockWithRotation[xSize, ySize];
        }
    }
}
