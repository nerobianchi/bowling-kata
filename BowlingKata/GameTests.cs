using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace BowlingKata
{
    public class GameTests
    {
        private readonly Game game;

        public GameTests()
        {
            game = new Game();
        }

        private void RollMany(int rolls, int pins)
        {
            for (int i = 0; i < rolls; i++)
            {
                game.Roll(pins);
            }
        }

        [Fact]
        public void given_a_game_which_all_rolls_are_0_then_score_should_be_0()
        {
            //arrange

            RollMany(20, 0);

            //assert
            game.Score().Should().Be(0);
        }


        [Fact]
        public void given_a_game_which_all_rolls_are_1_then_score_should_be_20()
        {
            //arrange
            RollMany(20, 1);

            //assert
            game.Score().Should().Be(20);
        }

        [Fact]
        public void given_a_game_which_frames_has_one_9_and_one_0_then_score_should_be_90()
        {
            //arrange
            for (int i = 0; i < 10; i++)
            {
                game.Roll(9);
                game.Roll(0);
            }

            //assert
            game.Score().Should().Be(90);
        }

        [Fact]
        public void given_first_frame_has_first_roll_is_5_pins_then_score_should_5()
        {
            game.Roll(5);
            var score = game.Score();
            score.Should().Be(5);
        }

        [Fact]
        public void given_first_frame_has_first_roll_is_5_pins_and_second_roll_is_5_pins_then_score_should_10()
        {
            game.Roll(5);
            game.Roll(5);
            var score = game.Score();
            score.Should().Be(10);
        }

        [Fact]
        public void given_first_frame_has_5_pins_and_5_pins_and_second_frame_has_5_pins_then_score_should_20()
        {
            game.Roll(5);
            game.Roll(5);
            game.Roll(5);
            var score = game.Score();
            score.Should().Be(20);
        }
    }


    public class Game
    {
        //private int totalScore;

        private readonly FrameList frames = new FrameList();

        public void Roll(int pins)
        {
            Frame currentFrame = frames.CurrentFrame();

            currentFrame.Add(pins);
//            frames.AddPinsToCurrentFrame(pins);

            //totalScore += currentFrame.Score();
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
//            if (this.Count == 0)
//            {
//                this.Add(new Frame(this.Count + 1));
//            }
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

//            if (this.Count < this.Capacity)
//            {
//                var frame = new Frame(this.Count + 1);
//                this.Add(frame);
//                return frame;
//            }

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