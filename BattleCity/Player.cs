using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BattleCity
{
    public class Player
    {
        const float _pixelUnit = 93.0f;
        Vector2 _pos = new Vector2(0, 0);
        float _rotation = -(float)(Math.PI / 2.0);
        bool _firePushed = false;
        TimeSpan _timeFireWasPushed = new TimeSpan(0);
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

        public Vector2 GetPosWithOrigin()
        {
            return new Vector2(_pos.X - GetSlide().GetOrigin().X, _pos.Y - GetSlide().GetOrigin().Y);
        }

        public Vector2 GetOrigin()
        {
            return GetSlide().GetOrigin();
        }

        public float GetRotation()
        {
            return _rotation;
        }

        public void SetRotationUp()
        {
            _rotation = -(float)(Math.PI / 2.0);
        }

        public void SetRotationDown()
        {
            _rotation = (float)(Math.PI / 2.0);
        }

        public void SetRotationLeft()
        {
            _rotation = (float)(Math.PI);
        }

        public void SetRotationRight()
        {
            _rotation = 0f;
        }

        public void MoveUp(float speed, float elapsedSeconds)
        {
            float distance = speed * elapsedSeconds * _pixelUnit;
            _pos.Y -= distance;
        }

        public void MoveDown(float speed, float elapsedSeconds)
        {
            float distance = speed * elapsedSeconds * _pixelUnit;
            _pos.Y += distance;
        }

        public void MoveLeft(float speed, float elapsedSeconds)
        {
            float distance = speed * elapsedSeconds * _pixelUnit;
            _pos.X -= distance;
        }

        public void MoveRight(float speed, float elapsedSeconds)
        {
            float distance = speed * elapsedSeconds * _pixelUnit;
            _pos.X += distance;
        }

        public void SetFirePushed(bool firePushed)
        {
            _firePushed = firePushed;
        }

        public bool GetFirePushed()
        {
            return _firePushed;
        }

        public void SetTimeFireWasPushed(TimeSpan timeFireWasPushed)
        {
            _timeFireWasPushed = timeFireWasPushed;
        }

        public TimeSpan GetTimeFireWasPushed()
        {
            return _timeFireWasPushed;
        }

    }
}
