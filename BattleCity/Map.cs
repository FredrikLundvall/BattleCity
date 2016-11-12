using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BattleCity
{
    public class Map
    {
        readonly int _width = 32;
        readonly int _height = 32;
        readonly int _tileWidth = 8;
        readonly int _tileHeight = 8;

        readonly Tile[,] _tileArray;

        public Map(int width = 32, int height = 32, int tileWidth = 8, int tileHeight = 8)
        {
            _width = width;
            _height = height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _tileArray = new Tile[width, height];
        }

        public bool ValidatePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _width && y < _height;
        }

        public void SetTileAtPosition(int x, int y, Tile tile)
        {
            _tileArray[x, y] = tile;
        }

        public Tile GetTileAtPosition(int x, int y)
        {
            return _tileArray[x, y];
        }

        public BoundingArea GetBoundingArea()
        {
            BoundingArea limitBoundingArea = _tileArray[0, 0].GetBoundingArea();
            limitBoundingArea.SetSize(new Vector2(_width * _tileWidth,_height * _tileHeight) );
            return limitBoundingArea;
        }

        public Nullable<Rectangle> GetRectIfPlayerIsBlocked(BoundingArea boundingArea)
        {
            Rectangle limitRect = _tileArray[0, 0].GetBoundingArea().GetRect();
            limitRect.Width = _width * _tileWidth;
            limitRect.Height = _height * _tileHeight;
            if (!limitRect.Contains(boundingArea.GetRect()))
                return limitRect;

            return getRectangle(boundingArea);
        }

        protected Nullable<Rectangle> getRectangle(BoundingArea boundingArea)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_tileArray[x, y].GetIsBlocked() && boundingArea.Intersects(_tileArray[x, y].GetBoundingArea()))
                        return _tileArray[x, y].GetBoundingArea().GetRect();
                }
            }
            return null;
        }

        public void Draw(Vector2 pos, SpriteBatch spriteBatch, TextureList textureList)
        {
            Vector2 tilepos = new Vector2(0, 0);
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    tilepos.X = x * _tileWidth;
                    tilepos.Y = y * _tileHeight;
                    if (GetTileAtPosition(x, y) != null)
                        spriteBatch.Draw(GetTileAtPosition(x, y).GetTextureFromList(textureList), tilepos + pos, Color.White);                
                }
            }
        }

    }
}
