using EKSuper_Mario.objects;
using EKSuper_Mario.objects.creatures;
using EKSuper_Mario.objects.mario;
using EKSuper_Mario.objects.tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EKSuper_Mario.Core.tile
{
    public class TileMap
    {
        public GameTile[,] tiles;
        public List<Platform> platforms = new List<Platform>();
        public List<Creature> creatures = new List<Creature>();
        public List<Creature> relevantCreatures = new List<Creature>();
        public List<Creature> creaturesToAdd = new List<Creature>();
        public List<GameTile> animatedTiles = new List<GameTile>();
        public List<SlopedTile> slopedTiles = new List<SlopedTile>();
        public Mario player;
        int width = 0, height = 0;
        public TileMap(int width, int height) 
        {
            this.width = width;
            this.height = height;
		    tiles = new GameTile[width, height];
    	}

        public GameTile[,] getTiles()
        {
            return tiles;
        }

        public int getWidth()
        {
            return this.width;
        }

        public int getHeight()
        {
            return this.height;
        }

        public GameTile getTile(int x, int y)
        {
            try
            {
                if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                {
                    return null;
                }
                else
                {
                    if ((this.width > x && this.height > y) && tiles[x, y] != null)
                    {
                        return tiles[x, y];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch { return null; }
        }

        public Image getImage(int x, int y)
        {
            if (x < 0 || x >= getWidth() || y < 0 || y >= getHeight())
            {
                return null;
            }
            else
            {
                try
                {
                    if (tiles[x, y] != null)
                    {
                        return tiles[x, y].getImage();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch { }
            }
            return null;
        }

        public void setTile(int x, int y, GameTile tile)
        {
            try
            {
                tiles[x, y] = tile;
            }
            catch
            { }
        }

        public void setTile(int x, int y, Image img)
        {
            try
            {
                tiles[x, y] = new GameTile(x, y, null, img);
            }
            catch
            { }
        }

        public Mario getPlayer()
        {
            return player;
        }

        public void setPlayer(Mario player)
        {
            this.player = player;
        }
    }
}
