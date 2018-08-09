using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BattleCity
{
    public class Explosion
    {
        const float _pixelUnit = 93.0f;
        Vector2 _pos = new Vector2(0, 0);
        SlideShowMachine _slideShowMachine;
        float _elapsedSeconds;

        public void SetSlideShowMachine(SlideShowMachine slideShowMachine)
        {
            _slideShowMachine = slideShowMachine;
        }

        protected Slide GetSlide()
        {
            return _slideShowMachine.GetSlideShow(SlideShowMachine.SLIDESHOW_RIGHT).GetSlide(_elapsedSeconds);
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

        public void AddElapsedSeconds(float elapsedSeconds)
        {
            _elapsedSeconds += elapsedSeconds;
        }

        public float GetElapsedSeconds()
        {
            return _elapsedSeconds;
        }

        public float GetLifetimeSeconds()
        {
            return _slideShowMachine.GetSlideShow(SlideShowMachine.SLIDESHOW_RIGHT).GetTotalDisplayTime();
        }

    }
}

