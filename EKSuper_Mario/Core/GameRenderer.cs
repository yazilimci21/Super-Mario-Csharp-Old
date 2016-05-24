using EKSuper_Mario.Core.animation;
using EKSuper_Mario.Core.tile;
using EKSuper_Mario.objects;
using EKSuper_Mario.objects.creatures;
using EKSuper_Mario.objects.mario;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EKSuper_Mario.Core
{
    public class GameRenderer
    {
        private int AdjustYScroll = 0;
	    private List<TileMap> maps = new List<TileMap>();
	    private int lastLife = -5;
	    private string df2 = "#,###,###,##0.00";
        public static int skor = 0;

	    // the size in bits of the tile
        private static int TILE_SIZE = 16;
        // Math.pow(2, TILE_SIZE_BITS) == TILE_SIZE
        private static int TILE_SIZE_BITS = 4;

        private Image background;

        // Converts a pixel position to a tile position.
        public static int pixelsToTiles(float pixels) {
            return pixelsToTiles((int)Math.Round((decimal)pixels));
        }

        // Converts a pixel position to a tile position.
        public static int pixelsToTiles(int pixels) {
            return pixels >> TILE_SIZE_BITS;
        }

        // Converts a tile position to a pixel position.
        public static int tilesToPixels(int numTiles) {
            return numTiles << TILE_SIZE_BITS;
        }

        // Sets the background to draw.
        public void setBackground(Image background) {
            this.background = background;
        }
    
	    // Returns the tile that a Sprite has collided with. Returns null if no 
	    // collision was detected. The last parameter, right, is used to check if multiple blocks
	    // are hit when a sprite jumps.
	    public static Point getTileCollision(TileMap map, Sprite sprite, float currX, float currY, float newX, float newY) {

	        float fromX = Math.Min(currX, newX);
	        float fromY = Math.Min(currY, newY);
	        float toX = Math.Max(currX, newX);
	        float toY = Math.Max(currY, newY);
	
	        // get the tile locations
	        int fromTileX = GameRenderer.pixelsToTiles(fromX);
	        int fromTileY = GameRenderer.pixelsToTiles(fromY);
	        int toTileX = GameRenderer.pixelsToTiles(toX + sprite.getWidth() - 1);
	        int toTileY = GameRenderer.pixelsToTiles(toY + sprite.getHeight() - 1);
	
	        // check each tile for a collision
	        for (int x=fromTileX; x<=toTileX; x++) {
	            for (int y=fromTileY; y<=toTileY; y++) {
	                if (x > 0 || x <= map.getWidth() || map.getImage(x, y) != null) {
	            	    GameTile tile = map.getTile(x,y);
	            	    if(tile != null && tile.isCollidable) {
	            		    return new Point(x,y);
	            	    }
	                }
	            }
	        }
	        // no collision found, return null
	        return new Point(0, 0);
	    }
	
	    /**
	     * @return A List of Points, where each Point corresponds to the location of a tile the sprite is 
	     * colliding with in map.tiles().
	     */
	    public static List<Point> getTileCollisionAll(TileMap map, Sprite sprite, float currX, float currY, float newX, float newY) {
		
		    List<Point> collisionPoints = new List<Point>(); 
	        float fromX = Math.Min(currX, newX);
	        float fromY = Math.Min(currY, newY);
	        float toX = Math.Max(currX, newX);
	        float toY = Math.Max(currY, newY);
	
	        // get the tile locations
	        int fromTileX = GameRenderer.pixelsToTiles(fromX);
	        int fromTileY = GameRenderer.pixelsToTiles(fromY);
	        int toTileX = GameRenderer.pixelsToTiles(toX + sprite.getWidth() - 1);
	        int toTileY = GameRenderer.pixelsToTiles(toY + sprite.getHeight() - 1);
	
	        // check each tile for a collision
	        for (int x=fromTileX; x<=toTileX; x++) {
	            for (int y=fromTileY; y<=toTileY; y++) {
	                if (x < 0 || x >= map.getWidth() || map.getImage(x, y) != null) {
	            	    Tile tile = map.getTile(x,y);
	            	    if(tile != null && map.getTile(x, y).isCollidable) {
	                    // collision found and the tile is collidable, return the tile
	            		    collisionPoints.Add(new Point(x,y));
	            	    } 
	                }
	            }
	        }
	        // no collision found, return null
	        return collisionPoints;
	    }
    
        /**
         * Draws all game elements. I did the best I can to seperate all updating from drawing. However, it 
         * seems its much more efficient to do some updating here where I have all the information I need
         * to make important decisions. So calling draw() DOES change the game state.
         */
        public void draw(Graphics g, TileMap mainMap, TileMap backgroundMap, TileMap foregroundMap, int screenWidth, int screenHeight) {
    	
    	    // add the three maps to the list of maps to draw, only mainMap is interactive
            if (maps.Count > 0) maps.Clear();

            if (backgroundMap != null && backgroundMap != default(TileMap)) maps.Add(backgroundMap);
            if (mainMap != null) maps.Add(mainMap);
            if (foregroundMap != null && foregroundMap != default(TileMap)) maps.Add(foregroundMap);
            Mario player = mainMap.getPlayer();
            int mapWidth = tilesToPixels(mainMap.getWidth());
            int mapHeight = tilesToPixels(mainMap.getHeight());
        
            // get the scrolling position of the map based on player's position...
        
            int offsetX = (int)(screenWidth/2 - Math.Round(player.getX()) - TILE_SIZE);
            offsetX = Math.Min(offsetX, 0); // if this gets set to 0, player is within a screen width
            offsetX = Math.Max(offsetX, screenWidth - mapWidth);
        
            int round = (int)Math.Round(player.getY());
        
            // initialize AdjustYScroll
            if (AdjustYScroll == 0) {
        	    AdjustYScroll = round;
            }
        
            // if the player is jumping, change the level at which the screen is drawn.
            if (player.isJumping || player.isAbovePlatform || player.onSlopedTile)
            {
        	    AdjustYScroll = round;
            }
        
            int offsetY = screenHeight/2 - AdjustYScroll - TILE_SIZE;
            offsetY = Math.Min(offsetY, 0);
            offsetY = Math.Max(offsetY, screenHeight - mapHeight - 25); // 25 fixs the JPanel height error

            // draw parallax background image
            if (background != null) {
        	    // x and y are responsible for fitting the background image to the size of the map
                int x = offsetX * (screenWidth - background.Width) / (screenWidth - mapWidth);
                int y = offsetY * (screenHeight - background.Height) / (screenHeight - mapHeight);

                g.DrawImage(background, -x, -y, screenWidth + 150, screenHeight + 100);
            }


            int firstTileX = pixelsToTiles(-offsetX);
            int lastTileX = firstTileX + pixelsToTiles(screenWidth) + 1;
            int firstTileY = pixelsToTiles(-offsetY);
            int lastTileY = firstTileY + pixelsToTiles(screenHeight) + 1;

            try
            {
                foreach (TileMap map in maps.ToArray())
                {
                    // draw the visible tiles
                    if (map != null)
                    {
                        for (int y = firstTileY; y <= lastTileY; y++)
                        {
                            for (int x = firstTileX; x <= lastTileX; x++)
                            {
                                GameTile tile = map.getTile(x, y);
                                if (tile != null)
                                {
                                    tile.draw(g, tilesToPixels(x), tilesToPixels(y),
                                            tile.getOffsetX() + offsetX, tile.getOffsetY() + offsetY);
                                }
                            }
                        }
                    }

                    if (map == mainMap)
                    {

                        for (int i = 0; i < map.creatures.Count; i++)
                        {

                            Creature c = map.creatures[i];
                            int x = (int)Math.Round(c.getX()) + offsetX;
                            int y = (int)Math.Round(c.getY()) + offsetY;
                            int tileX = pixelsToTiles(x);
                            int tileY = pixelsToTiles(y);

                            if (c == null || !c.isAlive)
                            {
                                map.creatures.RemoveAt(i);
                                i--;
                            }
                            else
                            {
                                //if (Creature.WAKE_UP_VALUE_UP_LEFT <= tileX && Creature.WAKE_UP_VALUE_DOWN_RIGHT >= tileX &&
                                //        Creature.WAKE_UP_VALUE_UP_LEFT <= tileY && Creature.WAKE_UP_VALUE_DOWN_RIGHT >= tileY)
                                //{

                                    // Only want to deal with platforms that are awake.
                                    if (c.GetType() == typeof(Platform)) { map.platforms.Add((Platform)c); }
                                    // Wake up the creature the first time the sprite is in view.
                                    if (c.isSleeping) { c.isSleeping = false; }

                                    c.isOnScreen = true;
                                    if (!c.isInvisible)
                                    {
                                        c.draw(g, x, y); // draw the creature
                                    }
                                    map.relevantCreatures.Add(c);

                            //    }
                            //    else
                            //    {
                            //        if (c.isAlwaysRelevant) { map.relevantCreatures.Add(c); }
                            //        c.isOnScreen = false;
                            //    }
                            }

                            if (!(((Mario)player).isInvisible))
                            {
                                player.draw(g, (int)Math.Round(player.getX()) + offsetX, (int)Math.Round(player.getY()) + offsetY,
                                    player.getOffsetX(), player.getOffsetY());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
            
            float dd2dec = float.Parse(player.getX().ToString(df2));
            Font fnt = new Font("Arial", 14.0f, FontStyle.Bold);
            SizeF size = g.MeasureString("Skor: " + skor, fnt);
            g.DrawString("Skor: " + skor, fnt, Brushes.Red, ((screenWidth/2) - (size.Width/2)), 0);
       
	        lastLife = player.getHealth();
        	Color myColor = Color.FromArgb(50, 50, 50, 50);
            //ControlPaint.DrawBorder3D(g, new Rectangle(hbStart, 4, hbWidth, 13), Border3DStyle.Flat);
	        //g.DrawRectangle(new Pen(new SolidBrush(myColor)), );
            ControlPaint.DrawBorder3D(g, new Rectangle(0, 0, screenWidth - 0, 22), Border3DStyle.Etched);
            ControlPaint.DrawVisualStyleBorder(g, new Rectangle(0, 0, screenWidth - 0, 22));
	        int hbStart = 4;
	        int hbWidth = 35;

	        //Color myColor2 = new Color(200, 60, 60, 50);
            for (int i = 0; i < 5; i++)
            {
                ControlPaint.DrawBorder3D(g, new Rectangle(hbStart + i * hbWidth, 5, hbWidth, 13), Border3DStyle.Sunken);
                g.FillRectangle(new SolidBrush(ControlPaint.Dark(Color.Green)), new Rectangle(hbStart + i * hbWidth + 2, 7, hbWidth - 3, 10));
            }
	        for(int i=0; i < player.getHealth(); i++) {
                g.FillRectangle(new SolidBrush(ControlPaint.Light(Color.Green)) ,new Rectangle(hbStart + i * hbWidth+2, 7, hbWidth-3, 10));
                //ControlPaint.DrawGrid(g, new Rectangle(hbStart + i * hbWidth, 5, hbWidth, 13), new Size(3, 3), Color.Black);
	        	//g.FillRectangle(Brushes.Black, hbStart + i*hbWidth, 5, hbWidth, 13);
	        } 
        
            maps.Clear(); 
        }
    }
}
