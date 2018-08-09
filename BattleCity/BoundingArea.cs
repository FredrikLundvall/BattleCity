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
        float _rotation;

        public BoundingArea(Vector2 position, Vector2 size, Vector2 origin, float rotation = 0)
        {
            _position = position;
            _size = size;
            _origin = origin;
            _rotation = rotation;
        }
        //public BoundingArea(Vector2 position, Rectangle size, Vector2 origin) : this(position, new Vector2(size.Width,size.Height), origin) { }

        //public BoundingArea(Rectangle size) : this(new Vector2(0f, 0f), new Vector2(size.Width,size.Height), new Vector2(0, 0)) { }

        //public BoundingArea(Vector2 size) : this(new Vector2(0f,0f), size, new Vector2(0f, 0f)) {}

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public void SetRotation(float rotation)
        {
            _rotation = rotation;
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
            var rotM = Matrix.CreateRotationZ(_rotation);
            Vector2 leftTop = Vector2.Transform(new Vector2(-_origin.X, -_origin.Y), rotM);
            Vector2 leftBottom = Vector2.Transform(new Vector2(-_origin.X, -_origin.Y + _size.Y), rotM);
            Vector2 rightTop = Vector2.Transform(new Vector2(-_origin.X + _size.X, -_origin.Y), rotM);
            Vector2 rightBottom = Vector2.Transform(new Vector2(-_origin.X + _size.X, -_origin.Y + _size.Y), rotM);
            var left = Math.Min(Math.Min(Math.Min(leftTop.X, leftBottom.X), rightTop.X), rightBottom.X);
            var top = Math.Min(Math.Min(Math.Min(leftTop.Y, leftBottom.Y), rightTop.Y), rightBottom.Y);
            var right = Math.Max(Math.Max(Math.Max(leftTop.X, leftBottom.X), rightTop.X), rightBottom.X);
            var bottom = Math.Max(Math.Max(Math.Max(leftTop.Y, leftBottom.Y), rightTop.Y), rightBottom.Y);

            return new Rectangle((int)(_position.X + left + 0.5f), (int)(_position.Y + top + 0.5f), (int)(right - left + 0.5f), (int)(bottom - top + 0.5f));
            //return new Rectangle((int)(_position.X - _origin.X + 0.5f), (int)(_position.Y - _origin.Y + 0.5f), (int)_size.X, (int)_size.Y);
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
