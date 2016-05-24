using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EKSuper_Mario.Core.animation
{
    public class Sprite : Animatible
    {
        protected float x;
        protected float y;
        protected float dx;
        protected float dy;

        public Sprite()
            : this(0, 0)
        {
        }

        public Sprite(int x, int y)
        {
            this.x = x;
            this.y = y;
            dx = 0;
            dy = 0;
        }

        public override void draw(Graphics g, int x, int y)
        {
            g.DrawImage(currentAnimation().getImage(), x, y, currentAnimation().getWidth(), currentAnimation().getHeight());
        }

        public override void draw(Graphics g, int x, int y, int offsetX, int offsetY)
        {
            draw(g, x + offsetX, y + offsetY);
        }

        public float getX()
        {
            return x;
        }

        public void setX(float x)
        {
            this.x = x;
        }

        public float getY()
        {
            return y;
        }

        public void setY(float y)
        {
            this.y = y;
        }

        public float getdX()
        {
            return dx;
        }

        public void setdX(float dx)
        {
            this.dx = dx;
        }

        public void setdY(float dy)
        {
            this.dy = dy;
        }

        public float getdY()
        {
            return dy;
        }

        public override int getHeight()
        {
            return currentAnimation().getHeight();
        }

        public override int getWidth()
        {
            return currentAnimation().getWidth();
        }

        public virtual void keyReleased(KeyEventArgs e) { }
        public virtual void keyPressed(KeyEventArgs e) { }
        public virtual void keyTyped(KeyEventArgs e) { }

        public static bool isCollision(Sprite s1, Sprite s2)
        {
            if (s1 == s2)
            {
                return false;
            }

            decimal s1x = Math.Round((decimal)s1.getX());
            decimal s1y = Math.Round((decimal)s1.getY());
            decimal s2x = Math.Round((decimal)s2.getX());
            decimal s2y = Math.Round((decimal)s2.getY());

            return (s1x < s2x + s2.getWidth() && s2x < s1x + s1.getWidth() &&
                    s1y < s2y + s2.getHeight() && s2y < s1y + s1.getHeight());
        }
    }
}
