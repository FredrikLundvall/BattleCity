using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public static class RotationDirection
    {
        public static Direction RotationToDirection(float rotation)
        {
            if (Similar.AreSimiliar(rotation, -(float)(Math.PI / 2.0)))
                return Direction.Up;
            else if (Similar.AreSimiliar(rotation, (float)(Math.PI / 2.0)))
                return Direction.Down;
            else if (Similar.AreSimiliar(rotation, (float)(Math.PI)))
                return Direction.Left;
            else if (Similar.AreSimiliar(rotation, 0f))
                return Direction.Right;
            else
                throw new Exception("Unable to convert rotation to direction");
        }
    }
}
