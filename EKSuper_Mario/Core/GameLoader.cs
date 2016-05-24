using EKSuper_Mario.Core.tile;
using EKSuper_Mario.objects.creatures;
using EKSuper_Mario.objects.mario;
using EKSuper_Mario.objects.tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EKSuper_Mario.Core
{
    public class GameLoader
    {
        private List<Image> plain;
	    private Image[] plainTiles;
	
	    private Image sloped_image;
	    private Image grass_edge;
	    private Image grass_center;
	
	    public GameLoader() {
		 
		    plain = new List<Image>();
		    plainTiles = (new SpriteMap(Application.StartupPath+"\\tiles\\Plain_Tiles.png", 6, 17)).getSprites();
		
		    foreach (Image bImage in plainTiles) {
			    plain.Add(bImage);
		    }

            sloped_image = loadImage(Application.StartupPath + "\\items\\Sloped_Tile.png");
            grass_edge = loadImage(Application.StartupPath + "\\items\\Grass_Edge.png");
            grass_center = loadImage(Application.StartupPath + "\\items\\Grass_Center.png");
	    }
	
	    public Image loadImage(String filename) {
		    Image img = null;
		    try {
		        img = Image.FromFile(filename);
		    } catch { }
		    return img;
	    }

	    // loads a tile map, given a map to load..
        // use this to load the background and foreground. Note: the status of the tiles (ie collide etc)
        // is irrelevant. Why? I don't check collision on maps other than the main map. 
        public TileMap loadOtherMaps(String filename)
        {
		    // lines is a list of strings, each element is a row of the map
		    List<String> lines = new List<String>();
		    int width = 0;
		    int height = 0;
		
		    // read in each line of the map into lines
            StreamReader reader = new StreamReader(filename);
            string line = null;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
			    if(!line.StartsWith("#")) {
				    lines.Add(line);
				    width = Math.Max(width, line.Length);
			    }
		    }
		    height = lines.Count; // number of elements in lines is the height
		
		    TileMap newMap = new TileMap(width, height);
		    for (int y=0; y < height; y++) {
			    line = lines[y];
			    for (int x=0; x < line.Length; x++) {
				    char ch = line[x];
				
				    if (ch == 'n') {
					    newMap.setTile(x, y, plain[92]);
				    } else if (ch == 'm') {
					    newMap.setTile(x, y, plain[93]);
				    } else if (ch == 'v') {
					    newMap.setTile(x, y, plain[90]);
				    } else if (ch == 'b') {
					    newMap.setTile(x, y, plain[91]);
				    } else if (ch == 'q') { // rock left
					    newMap.setTile(x, y, plain[48]);
				    } else if (ch == 'w') { // rock right
					    newMap.setTile(x, y, plain[49]);
				    } 
			    }
		    }
		    return newMap;	
	    }
    	
        // Use this to load the main map
	    public TileMap loadMap(String filename, MarioSoundManager soundManager) 
        {
		    // lines is a list of strings, each element is a row of the map
		    List<String> lines = new List<String>();
		    int width = 0;
		    int height = 0;
		
		    // read in each line of the map into lines
            StreamReader reader = new StreamReader(filename);
            string line = null;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
			    if(!line.StartsWith("#")) {
				    lines.Add(line);
				    width = Math.Max(width, line.Length);
			    }
		    }
		    height = lines.Count; // number of elements in lines is the height
		
		    TileMap newMap = new TileMap(width, height);
		    for (int y=0; y < height; y++) {
			    line = lines[y];
			    for (int x=0; x < line.Length; x++) {
				    char ch = line[x];
				
				    int pixelX = GameRenderer.tilesToPixels(x);
				    int pixelY = GameRenderer.tilesToPixels(y);
				    // enumerate the possible tiles...
                    if (ch == 'M')
                    {
                        MarioSoundManager smanager = new MarioSoundManager();
                        Mario mario = new Mario(pixelX, pixelY, smanager);
                        newMap.setPlayer(mario);
                    }
				    else if (ch == 'G') {
					    newMap.creatures.Add(new Goomba(pixelX, pixelY, soundManager));
				    } else if (ch == 'K') {
					    newMap.creatures.Add(new RedKoopa(pixelX, pixelY, soundManager));
				    } else if (ch == 'V') {
					    GameTile t = new GameTile(pixelX, pixelY, plain[56]);
					    newMap.setTile(x, y, t);
				    } else if (ch == 'R') {
					    RotatingBlock r = new RotatingBlock(pixelX, pixelY);
					    newMap.setTile(x, y, r);
					    newMap.animatedTiles.Add(r);
				    } else if (ch == '3') {
					    GameTile t = new GameTile(pixelX, pixelY, plain[4]);
					    newMap.setTile(x, y, t);
				    } else if (ch == '4') {
					    GameTile t = new GameTile(pixelX, pixelY, plain[10]);
					    newMap.setTile(x, y, t);
				    } else if (ch == '2') {
					    GameTile t = new GameTile(pixelX, pixelY, plain[86]);
					    newMap.setTile(x, y, t);
				    } else if (ch == 'Q') {
					    QuestionBlock q = new QuestionBlock(pixelX, pixelY, newMap, soundManager, true, false);
					    newMap.setTile(x, y, q);
					    newMap.animatedTiles.Add(q);
				    } else if (ch == 'W') {
					    QuestionBlock q = new QuestionBlock(pixelX, pixelY, newMap, soundManager, false, true);
					    newMap.setTile(x, y, q);
					    newMap.animatedTiles.Add(q);
				    } else if (ch == 'S') {
					    newMap.creatures.Add(new RedShell(pixelX, pixelY, newMap, soundManager, true));
				    } else if(ch == 'C') {
					    newMap.creatures.Add(new Coin(pixelX, pixelY));
				    } else if(ch == 'P') {
					    Platform p = new Platform(pixelX, pixelY);
					    newMap.creatures.Add(p);
				    } else if(ch == '9') {
					    SlopedTile t = new SlopedTile(pixelX, pixelY, sloped_image, true);
					    newMap.setTile(x, y, t);
					    newMap.slopedTiles.Add(t);
				    } else if(ch == '8') {
					    GameTile t = new GameTile(pixelX, pixelY, grass_edge);
					    newMap.setTile(x, y, t);
				    } else if(ch == '7') {
					    GameTile t = new GameTile(pixelX, pixelY, grass_center);
					    newMap.setTile(x, y, t);
				    }
			    }
		    }
		    return newMap;	
	    }
    }
}
