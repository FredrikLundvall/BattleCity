using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleCity
{
    public class ExplosionSpawner
    {
        IList<Explosion> _spawnedExplosions;
        SlideShowMachine _slideShowMachine;

        public ExplosionSpawner(IList<Explosion> spawnedExplosions)
        {
            _spawnedExplosions = spawnedExplosions;
        }

        public void SetSlideShowMachine(SlideShowMachine slideShowmachine)
        {
            _slideShowMachine = slideShowmachine;
        }

        public void Spawn(Vector2 pos, float speed)
        {
            var explosion = new Explosion();
            explosion.SetSlideShowMachine(_slideShowMachine);
            explosion.SetPos(pos);
            _spawnedExplosions.Add(explosion);
        }

        public void AddElapsedSeconds(float elapsedSeconds)
        {
            foreach (Explosion explosion in _spawnedExplosions)
                explosion.AddElapsedSeconds(elapsedSeconds);
        }

        public void Draw(Vector2 pos, SpriteBatch spriteBatch, TextureList textureList)
        {
            foreach (Explosion explosion in _spawnedExplosions)
                spriteBatch.Draw(explosion.GetTextureFromList(textureList), (explosion.GetPos() + pos), null, Color.White, 0, explosion.GetOrigin(), 1f, SpriteEffects.None, 0f);
        }


        public void RemoveIfLifetimePassed()
        {
            int i = 0;
            while (i < _spawnedExplosions.Count)
            {
                if (_spawnedExplosions[i].GetElapsedSeconds() > _spawnedExplosions[i].GetLifetimeSeconds())
                    _spawnedExplosions.RemoveAt(i);
                else
                    i++;
            }
        }

    }

}
