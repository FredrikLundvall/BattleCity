using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;


namespace BattleCity
{
    public class TextureList
    {
        IList<Texture2D> _alltextures;

        public TextureList(IList<Texture2D> alltextures)
        {
            _alltextures = alltextures;
        }

        public int AddReturningIndex(Texture2D texture)
        {
            _alltextures.Add(texture);
            return _alltextures.IndexOf(texture);
        }

        public Texture2D GetTextureFromIndex(int index)
        {
            return _alltextures[index];
        }
    }
}
