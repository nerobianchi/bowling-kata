using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class Game
    {
        private readonly FrameList frames = new FrameList();

        public void Roll(int pins)
        {
            Frame currentFrame = frames.CurrentFrame();

            currentFrame.Add(pins);
        }

        public int Score()
        {
            return this.frames.Sum(m => m.Score());
        }
    }

    public class FrameList : List<Frame>
    {
        public FrameList()
        {
            this.Capacity = 10;
            for (int i = 0; i < this.Capacity; i++)
            {
                this.Add(new Frame(i, this));
            }
        }

        public Frame CurrentFrame()
        {
            for (int i = 0; i < this.Count; i++)
            {
                var frame = this[i];
                if (frame != null)
                {
                    if (!frame.IsCompleted())
                    {
                        return frame;
                    }
                }
            }

            return null;
        }
    }

    public class Frame
    {
        private Roll firstRoll;
        private Roll secondRoll;
        private readonly int index;
        private readonly FrameList frameList;

        public Frame(int index, FrameList frameList)
        {
            this.index = index;
            this.frameList = frameList;
        }

        public void Add(int pins)
        {
            if (firstRoll == null)
            {
                firstRoll = new Roll(pins);
            }
            else if (secondRoll == null)
            {
                secondRoll = new Roll(pins);
            }
        }

        public int Score()
        {
            var firstRollNumberOfPins = 0;
            if (this.firstRoll != null)
            {
                firstRollNumberOfPins = this.firstRoll.NumberOfPins;
            }
            int secondRollNumberOfPins = 0;

            if (this.secondRoll != null)
            {
                secondRollNumberOfPins = this.secondRoll.NumberOfPins;
            }
            var total = firstRollNumberOfPins + secondRollNumberOfPins;
            if (total == 10)
            {
                var frame = this.frameList[this.index + 1];
                if (frame != null) total += frame.FirstRollPins();
            }
            return total;
        }

        private int FirstRollPins()
        {
            return this.firstRoll?.NumberOfPins ?? 0;
        }

        public bool IsCompleted()
        {
            return this.firstRoll != null && this.secondRoll != null;
        }
    }

    public class Roll
    {
        public int NumberOfPins { get; }

        public Roll(int numberOfPins)
        {
            this.NumberOfPins = numberOfPins;
        }
    }
}