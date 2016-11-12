using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BattleCity
{
    public struct Slide
    {
        int _textureIndex;
        Vector2 _origin;
        BoundingArea _boundingArea;

        public void SetBoundingArea(BoundingArea boundingArea)
        {
            _boundingArea = boundingArea;
        }

        public BoundingArea GetBoundingArea()
        {
            return _boundingArea;
        }

        public Rectangle GetBoundingingRect()
        {
            return GetBoundingArea().GetRect();
        }

        public void SetTextureIndex(int textureIndex)
        {
            _textureIndex = textureIndex;
        }

        public Texture2D GetTextureFromList(TextureList textureList)
        {
            return textureList.GetTextureFromIndex(_textureIndex);
        }

        public Vector2 GetOrigin()
        {
            return _origin;
        }

        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
        }

    }
}
