using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BattleCity
{
    public struct BoundingArea
    {
        Vector2 _position;
        Vector2 _size;
        Vector2 _origin;

        public BoundingArea(Vector2 position, Vector2 size, Vector2 origin)
        {
            _position = position;
            _size = size;
            _origin = origin;
        }
        //public BoundingArea(Vector2 position, Rectangle size, Vector2 origin) : this(position, new Vector2(size.Width,size.Height), origin) { }

        //public BoundingArea(Rectangle size) : this(new Vector2(0f, 0f), new Vector2(size.Width,size.Height), new Vector2(0, 0)) { }

        //public BoundingArea(Vector2 size) : this(new Vector2(0f,0f), size, new Vector2(0f, 0f)) {}

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
        }

        public void SetSize(Vector2 size)
        {
            _size = size;
        }

        public Rectangle GetRect()
        {
            return new Rectangle((int)(_position.X - _origin.X + 0.5f), (int)(_position.Y - _origin.Y + 0.5f), (int)(_size.X + 0.5f), (int)(_size.Y + 0.5f));
        }

        public bool Intersects(BoundingArea boundingArea)
        {
            return GetRect().Intersects(boundingArea.GetRect());
        }

        public bool Contains(BoundingArea boundingArea)
        {
            return GetRect().Contains(boundingArea.GetRect());
        }

    }
}
