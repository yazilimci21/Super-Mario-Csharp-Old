using EKSuper_Mario.Core.animation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EKSuper_Mario.Core.tile
{
    public class Tile : Animatible
    {
        // fields
        private int tileX;
        private int tileY;
        private int pixelX;
        private int pixelY;
        protected Image img;

        public Tile(int pixelX, int pixelY, Animation anim, Image img)
        {
            tileX = GameRenderer.pixelsToTiles(pixelX);
            tileY = GameRenderer.pixelsToTiles(pixelY);
            this.pixelX = pixelX;
            this.pixelY = pixelY;
            this.img = img;
            setAnimation(anim);
        }

        public Tile(int pixelX, int pixelY, Image img)
            : this(pixelX, pixelY, null, img)
        {
        }

        public override void draw(Graphics g, int pixelX, int pixelY)
        {
            g.DrawImage(getImage(), pixelX, pixelY, getWidth(), getHeight());
        }

        public override void draw(Graphics g, int pixelX, int pixelY, int offsetX, int offsetY)
        {
            draw(g, pixelX + offsetX, pixelY + offsetY);
        }

        public Image getImage()
        {
            return (currentAnimation() == null) ? img : currentAnimation().getImage();
        }

        public int getPixelX()
        {
            return pixelX;
        }

        public int getPixelY()
        {
            return pixelY;
        }

        public override int getWidth()
        {
            return getImage().Width;
        }

        public override int getHeight()
        {
            return getImage().Height;
        }
    }
}
