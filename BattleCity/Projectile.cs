using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BattleCity
{
    public class Projectile
    {
        const float _pixelUnit = 93.0f;
        Vector2 _pos = new Vector2(0, 0);
        float _rotation = -(float)(Math.PI / 2.0);
        float _speed = 4f;
        SlideShowMachine _slideShowMachine;

        public void SetSlideShowMachine(SlideShowMachine slideShowMachine)
        {
            _slideShowMachine = slideShowMachine;
        }

        protected Slide GetSlide()
        {
            return _slideShowMachine.GetSlideShow(SlideShowMachine.SLIDESHOW_RIGHT).GetSlide();
        }

        public BoundingArea GetBoundingArea()
        {
            BoundingArea boundingArea = GetSlide().GetBoundingArea();
            boundingArea.SetPosition(GetPos());
            return boundingArea;
        }

        public Rectangle GetBoundingingRect()
        {
            return GetBoundingArea().GetRect();
        }

        public Texture2D GetTextureFromList(TextureList textureList)
        {
            return GetSlide().GetTextureFromList(textureList);
        }

        public void SetPos(Vector2 pos)
        {
            _pos = pos;
        }

        public Vector2 GetPos()
        {
            return _pos;
        }

        public Vector2 GetOrigin()
        {
            return GetSlide().GetOrigin();
        }

        public float GetRotation()
        {
            return _rotation;
        }

        public void SetRotation(float rotation)
        {
            _rotation = rotation;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public void Move(float elapsedSeconds)
        {
            _pos.X += _speed * elapsedSeconds * _pixelUnit * (float)Math.Cos(_rotation);
            _pos.Y += _speed * elapsedSeconds * _pixelUnit * (float)Math.Sin(_rotation);
        }
    }
}

