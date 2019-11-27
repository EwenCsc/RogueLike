using MapRogueLike.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRogueLike.V2
{
    public class RoomManager : Singleton<RoomManager>
    {
        Room[,] grid = null;

        public void SetGrid(Room[,] _grid)
        {
            grid = _grid;
        }

        public Room GetRoom(Vector2i gridPos)
        {
            if (grid == null)
            {
                Console.WriteLine("No grid in roomManager");
                return null;
            }
            return grid[gridPos.X, gridPos.Y];
        }
        
        public Room GetCenterRoom()
        {
            return grid[grid.GetLength(0) / 2, grid.GetLength(1) / 2];
        }
    }
}
