using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleCity
{
    public class ProjectileSpawner
    {
        IList<Projectile> _spawnedProjectiles;
        SlideShowMachine _slideShowMachine;

        public ProjectileSpawner(IList<Projectile> spawnedProjectiles)
        {
            _spawnedProjectiles = spawnedProjectiles;
        }

        public void SetSlideShowMachine(SlideShowMachine slideShowmachine)
        {
            _slideShowMachine = slideShowmachine;
        }

        //protected Slide GetSlide()
        //{
        //    return _slideShowMachine.GetSlideShow(SlideShowMachine.SLIDESHOW_RIGHT).GetSlide();
        //}
 
        public void Spawn(Vector2 pos, float rotation, float speed)
        {
            var projectile = new Projectile();
            projectile.SetSlideShowMachine(_slideShowMachine);
            projectile.SetPos(pos);
            projectile.SetRotation(rotation);
            projectile.SetSpeed(speed);
            //TODO: Detta görs för att inte spränga sin egen tank, lös kanske på något annat sätt
            projectile.Move(0.1f); 
            _spawnedProjectiles.Add(projectile);
        }

        public void Move(float elapsedSeconds)
        {
            foreach (Projectile projectile in _spawnedProjectiles)
                projectile.Move(elapsedSeconds);
        }

        public void Draw(Vector2 pos, SpriteBatch spriteBatch, TextureList textureList)
        {
            foreach (Projectile projectile in _spawnedProjectiles)
                spriteBatch.Draw(projectile.GetTextureFromList(textureList), projectile.GetPos() + pos, null, Color.White, projectile.GetRotation(), projectile.GetOrigin(), 1f, SpriteEffects.None, 0f);
        }


        public void RemoveIfOutsideBounds(BoundingArea boundingArea)
        {
            int i = 0;
            while(i < _spawnedProjectiles.Count)
            {
                if (!boundingArea.Contains(_spawnedProjectiles[i].GetBoundingArea()))
                    _spawnedProjectiles.RemoveAt(i);
                else
                    i++;
            }
        }

        public void RemoveIfHittingBound(BoundingArea boundingArea)
        {
            //TODO: Returnera något för att spränga tanks också
            int i = 0;
            while (i < _spawnedProjectiles.Count)
            {
                if (_spawnedProjectiles[i].GetBoundingArea().Intersects(boundingArea))
                    _spawnedProjectiles.RemoveAt(i);
                else
                    i++;
            }
        }
    }

}
