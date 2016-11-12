using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace BattleCity
{
    public class Tile
    {
        int _textureIndex;
        bool _isBlocked = false;
        BoundingArea _boundingArea;

        public void SetBoundingArea(BoundingArea boundingArea)
        {
            _boundingArea = boundingArea;
        }

        public BoundingArea GetBoundingArea()
        {
            return _boundingArea;
        }

        public void SetTextureIndex(int textureIndex)
        {
            _textureIndex = textureIndex;
        }

        public int GetTextureIndex()
        {
            return _textureIndex;
        }

        public void SetIsBlocked(bool isBlocked)
        {
            _isBlocked = isBlocked;
        }

        public bool GetIsBlocked()
        {
            return _isBlocked;
        }

        public Texture2D GetTextureFromList(TextureList textureList)
        {
            return textureList.GetTextureFromIndex(_textureIndex);
        }

    }
}
