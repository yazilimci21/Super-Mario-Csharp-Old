using EKSuper_Mario.Core.animation;
using EKSuper_Mario.objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EKSuper_Mario.Core.tile
{
    public class GameTile : Tile
    {
        public bool isCollidable = true;
        public bool isSloped = false;
        public List<Creature> collidingCreatures = new List<Creature>();

        public GameTile(int pixelX, int pixelY, Animation anim, Image img):base(pixelX, pixelY, anim, img)
        {
        }

        public GameTile(int pixelX, int pixelY, Image img)
            : this(pixelX, pixelY, null, img)
        {
            
        }

        public virtual void doAction() { }

        public void setIsCollidable(bool isCollidable)
        {
            this.isCollidable = isCollidable;
        }

        public void setIsSloped(bool isSloped)
        {
            this.isSloped = isSloped;
        }
    }
}
