using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EKSuper_Mario.Core.animation
{
    public class Animation
    {
        private List<AnimFrame> frames = new List<AnimFrame>();
        private int currFrameIndex;
        private long animTime;
        private long totalDuration;
        private long defaultAnimLength;
        private int width = 0, height = 0;

        public Animation()
        {
            frames = new List<AnimFrame>();
            totalDuration = 0;
            defaultAnimLength = 0;
            start();
        }

        public Animation(long defaultAnimLength)
        {
            totalDuration = 0;
            this.defaultAnimLength = defaultAnimLength;
            start();
        }

        public Animation setDAL(long defaultAnimLength)
        {
            this.defaultAnimLength = defaultAnimLength;
            return this;
        }

        public Animation addFrame(Image image)
        {
            totalDuration += defaultAnimLength;
            frames.Add(new AnimFrame(image, totalDuration));
            width = frames[currFrameIndex].image.Width;
            height = frames[currFrameIndex].image.Height;
            return this;
        }

        public Animation addFrame(Image image, long duration)
        {
            totalDuration += duration;
            frames.Add(new AnimFrame(image, totalDuration));
            width = frames[currFrameIndex].image.Width;
            height = frames[currFrameIndex].image.Height;
            defaultAnimLength = duration;
            return this;
        }

        public void start()
        {
            animTime = 0;
            currFrameIndex = 0;
        }

        public int getHeight()
        {
            return height;
        }

        public int getWidth()
        {
            return width;
        }

        public void update(long elapsedTime)
        {
            if (frames.Count > 1)
            { 
                animTime += elapsedTime;

                if (animTime >= totalDuration)
                {
                    animTime = 0;
                    currFrameIndex = 0;
                    endOfAnimationAction();
                }
                if (animTime > getFrame(currFrameIndex).endTime)
                {
                    currFrameIndex++;
                    width = frames[currFrameIndex].image.Width;
                    height = frames[currFrameIndex].image.Height;
                }
            }
        }

        public virtual void endOfAnimationAction() { }

        private Image[] getImages()
        {
            if (frames.Count == 0)
            {
                return null;
            }
            else
            {
                List<Image> images = new List<Image>();
                foreach (AnimFrame frame in frames.ToArray())
                {
                    images.Add(frame.image);
                }
                return (Image[])images.ToArray();
            }
        }

        public Image getImage()
        {
            if (frames.Count == 0)
            {
                return null;
            }
            else
            {
                return getFrame(currFrameIndex).image;
            }
        }

        private AnimFrame getFrame(int i)
        {
            return frames[i];
        }
    }

    public class AnimFrame
    {
        public Image image;
        public long endTime; 

        public AnimFrame(Image image, long endTime)
        {
            this.image = image;
            this.endTime = endTime;
        }
    }
}
