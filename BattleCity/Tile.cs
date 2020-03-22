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
        int _textureIndexDestroyed = -1;
        bool _isBlocked = false;
        BoundingArea _boundingArea;
        bool _leftUpDestroyed = false;
        bool _rightUpDestroyed = false;
        bool _leftDownDestroyed = false;
        bool _rightDownDestroyed = false;

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

        public void SetTextureIndexDestroyed(int textureIndexDestroyed)
        {
            _textureIndexDestroyed = textureIndexDestroyed;
        }

        public int GetTextureIndexDestroyed()
        {
            return _textureIndexDestroyed;
        }

        public void SetIsBlocked(bool isBlocked)
        {
            _isBlocked = isBlocked;
        }

        public bool GetIsBlocked()
        {
            return _isBlocked;
        }

        public void SetLeftUpDestroyed(bool leftUpDestroyed)
        {
            _leftUpDestroyed = leftUpDestroyed;
        }

        public void SetRightUpDestroyed(bool rightUpDestroyed)
        {
            _rightUpDestroyed = rightUpDestroyed;
        }

        public void SetLeftDownDestroyed(bool leftDownDestroyed)
        {
            _leftDownDestroyed = leftDownDestroyed;
        }

        public void SetRightDownDestroyed(bool rightDownDestroyed)
        {
            _rightDownDestroyed = rightDownDestroyed;
        }

        public bool GetLeftUpDestroyed()
        {
            return _leftUpDestroyed;
        }

        public bool GetRightUpDestroyed()
        {
            return _rightUpDestroyed;
        }

        public bool GetLeftDownDestroyed()
        {
            return _leftDownDestroyed;
        }

        public bool GetRightDownDestroyed()
        {
            return _rightDownDestroyed;
        }

        public bool GetDestroyed()
        {
            return _leftUpDestroyed && _rightUpDestroyed && _leftDownDestroyed &&_rightDownDestroyed;
        }

        public bool GetUntouched()
        {
            return !_leftUpDestroyed && !_rightUpDestroyed && !_leftDownDestroyed && !_rightDownDestroyed;
        }

        public Texture2D GetTextureFromList(TextureList textureList)
        {
            return textureList.GetTextureFromIndex(_textureIndex);
        }

        public Texture2D GetTextureDestroyedFromList(TextureList textureList)
        {
            return textureList.GetTextureFromIndex(_textureIndexDestroyed);
        }

        public bool ProjectileImpact(Projectile projectile)
        {
            bool impact = true;
            var projectileDirection = RotationDirection.RotationToDirection(projectile.GetRotation());
            if(projectileDirection == Direction.Down)
            {
                impact = ProjectileImpactingFromUp(projectile);
            }
            else if (projectileDirection == Direction.Up)
            {
                impact = ProjectileImpactingFromDown(projectile);
            }
            else if (projectileDirection == Direction.Left)
            {
                impact = ProjectileImpactingFromRight(projectile);
            }
            else// if (projectileDirection == Direction.Right)
            {
                impact = ProjectileImpactingFromLeft(projectile);
            }
            return impact;
        }

        protected bool ProjectileImpactingFromUp(Projectile projectile)
        {
            bool impact = true;
            var posWithOrigin = _boundingArea.GetPosWithOrigin();
            var projectilePosWithOrigin = projectile.GetBoundingArea().GetPosWithOrigin();
            if (projectilePosWithOrigin.X <= posWithOrigin.X)
            {
                if (!_leftUpDestroyed)
                    _leftUpDestroyed = true;
                else if (!_leftDownDestroyed)
                    _leftDownDestroyed = true;
                else
                    impact = false;
            }
            else
            {
                if (!_rightUpDestroyed)
                    _rightUpDestroyed = true;
                else if (!_rightDownDestroyed)
                    _rightDownDestroyed = true;
                else
                    impact = false;
            }
            return impact;
        }

        protected bool ProjectileImpactingFromDown(Projectile projectile)
        {
            bool impact = true;
            var posWithOrigin = _boundingArea.GetPosWithOrigin();
            var projectilePosWithOrigin = projectile.GetBoundingArea().GetPosWithOrigin();
            if (projectilePosWithOrigin.X <= posWithOrigin.X)
            {
                if (!_leftDownDestroyed)
                    _leftDownDestroyed = true;
                else if (!_leftUpDestroyed)
                    _leftUpDestroyed = true;
                else
                    impact = false;
            }
            else
            {
                if (!_rightDownDestroyed)
                    _rightDownDestroyed = true;
                else if (!_rightUpDestroyed)
                    _rightUpDestroyed = true;
                else
                    impact = false;
            }
            return impact;
        }

        protected bool ProjectileImpactingFromRight(Projectile projectile)
        {
            bool impact = true;
            var posWithOrigin = _boundingArea.GetPosWithOrigin();
            var projectilePosWithOrigin = projectile.GetBoundingArea().GetPosWithOrigin();
            if (projectilePosWithOrigin.Y <= posWithOrigin.Y)
            {
                if (!_rightUpDestroyed)
                    _rightUpDestroyed = true;
                else if (!_leftUpDestroyed)
                    _leftUpDestroyed = true;
                else
                    impact = false;
            }
            else
            {
                if (!_rightDownDestroyed)
                    _rightDownDestroyed = true;
                else if (!_leftDownDestroyed)
                    _leftDownDestroyed = true;
                else
                    impact = false;
            }
            return impact;
        }

        protected bool ProjectileImpactingFromLeft(Projectile projectile)
        {
            bool impact = true;
            var posWithOrigin = _boundingArea.GetPosWithOrigin();
            var projectilePosWithOrigin = projectile.GetBoundingArea().GetPosWithOrigin();
            if (projectilePosWithOrigin.Y <= posWithOrigin.Y)
            {
                if (!_leftUpDestroyed)
                    _leftUpDestroyed = true;
                else if (!_rightUpDestroyed)
                    _rightUpDestroyed = true;
                else
                    impact = false;
            }
            else
            {
                if (!_leftDownDestroyed)
                    _leftDownDestroyed = true;
                else if (!_rightDownDestroyed)
                    _rightDownDestroyed = true;
                else
                    impact = false;
            }
            return impact;
        }
    }
}
