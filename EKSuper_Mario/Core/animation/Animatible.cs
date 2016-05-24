using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EKSuper_Mario.Core.animation
{
    public class Animatible
    {
        private Animation currAnim;
        private int offsetX;
        private int offsetY;

        public virtual void draw(Graphics g, int pixelX, int pixelY) { }
        public virtual void draw(Graphics g, int pixelX, int pixelY, int offsetX, int offsetY) { }
        public virtual int getHeight() { return 0; }
        public virtual int getWidth() { return 0; }

        public Animation currentAnimation()
        {
            return currAnim;
        }

        public virtual void setAnimation(Animation currAnim)
        {
            this.currAnim = currAnim;
        }

        public virtual void update(int time)
        {
            currAnim.update(time);
        }

        public void setOffsetX(int offsetX)
        {
            this.offsetX = offsetX;
        }

        public void setOffsetY(int offsetY)
        {
            this.offsetY = offsetY;
        }

        public int getOffsetX()
        {
            return offsetX;
        }

        public int getOffsetY()
        {
            return offsetY;
        }
    }
}
