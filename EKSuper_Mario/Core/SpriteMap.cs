using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EKSuper_Mario.Core
{
    public class SpriteMap
    {
        private Image spriteMap;
        private Image[] sprites; 
    
        public SpriteMap(String filename, int c, int r) {
            this.loadSprites(filename, c, r);
        }
    
        // returns the sprites array
	    public Image[] getSprites() {
		    return sprites;
	    }
    
        // loads a BufferedImage into spriteSheet and then cuts the image
        // into individual sprites based on amount of columns and rows
        private void loadSprites(String filename, int c, int r) {
    	    spriteMap = loadImage(filename);
    	    sprites = splitSprites(c, r);
        }
    
	    // loads a BufferedImage and returns it
	    private Image loadImage(String refe) {   
            Image bimg = null;   
            try {   
                bimg = Image.FromFile(refe);   
            } catch {   
                
            }   
            return bimg;   
        }
	
        private Image[] splitSprites(int c, int r)
        {
		    int pWidth = spriteMap.Width / c; // width of each sprite
		    int pHeight = spriteMap.Height / r; // height of each sprite
            Image[] sprites = new Image[c * r];
		    int n = 0; // used to count sprites
		
		    //int xOff = 0; if needed to adjust cutting precision
		    int yOff = 0;
	
		    for(int y=0; y < r; y++) {
			    for(int x = 0; x < c; x++) {
                    sprites[n] = new Bitmap(pWidth, pHeight);
                    Graphics g = Graphics.FromImage(sprites[n]);
                    Rectangle bmpRect = new Rectangle(0, 0, spriteMap.Width, spriteMap.Height);
                    Rectangle cutRect = new Rectangle(pWidth*x, pHeight*y, pWidth*x+pWidth, pHeight*y+pHeight-yOff);
                    g.DrawImage(spriteMap, bmpRect, cutRect, GraphicsUnit.Pixel);
                    g.Dispose();
                    n++;
			    }
		    }
		    return sprites;
	    }
    }
}
