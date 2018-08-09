using Microsoft.Xna.Framework;
using System;

namespace BattleCity
{
    public static class VectorExtension
    {
        public static Vector2 Round(this Vector2 vectorToRound)
        {
            return new Vector2((int)(vectorToRound.X + 0.5f), (int)vectorToRound.Y + 0.5f);
            //return new Vector2((float)Math.Round(vectorToRound.X, MidpointRounding.AwayFromZero), (float)Math.Round(vectorToRound.Y, MidpointRounding.AwayFromZero));
        }
    }
}
