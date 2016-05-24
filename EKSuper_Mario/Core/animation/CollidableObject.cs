using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EKSuper_Mario.Core.animation
{
    public class CollidableObject : Sprite
    {
        protected MarioSoundManager soundManager;
        public bool isCollidable;
        public bool isOnScreen;

        public CollidableObject(int pixelX, int pixelY, MarioSoundManager soundManager)
        {
            this.x = x;
            this.y = y;
            dx = 0;
            dy = 0;
            this.isCollidable = true;
            this.isOnScreen = false;
            this.soundManager = soundManager;
        }

        public CollidableObject(int pixelX, int pixelY)
            : this(pixelX, pixelY, null)
        {
        }

        public CollidableObject()
            : this(0, 0, null)
        {
        }
    }
}
