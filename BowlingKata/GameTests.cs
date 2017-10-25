using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace BowlingKata
{
    public class GameTests
    {
        [Fact]
        public void given_a_game_which_all_rolls_are_0_then_score_should_be_0()
        {
            //arrange
            Game game = new Game();
            for (int i = 0; i < 20; i++)
            {
                game.Roll(0);
                game.CurrentFrameIndex.Should().Be(i / 2 + 1);
            }

            //act
            int score = game.Score();

            //assert
            score.Should().Be(0);
        }

        [Fact]
        public void given_a_game_which_all_rolls_are_1_then_score_should_be_20()
        {
            //arrange
            Game game = new Game();
            for (int i = 0; i < 20; i++)
            {
                game.Roll(1);
                game.CurrentFrameIndex.Should().Be(i / 2 + 1);
            }

            //act
            int score = game.Score();

            //assert
            score.Should().Be(20);
        }

        [Fact]
        public void given_a_game_which_frames_has_one_9_and_one_0_then_score_should_be_90()
        {
            //arrange
            Game game = new Game();
            for (int i = 0; i < 10; i++)
            {
                game.Roll(9);
                game.Roll(0);
                game.CurrentFrameIndex.Should().Be(i + 1);
            }

            //act
            int score = game.Score();

            //assert
            score.Should().Be(90);
        }

//        [Fact]
//        public void given_first_frame_has_first_roll_is_5_pins_then_score_should_5()
//        {
//            Game game = new Game();
//            game.Roll(5);
//            var score = game.Score();
//            score.Should().Be(5);
//        }
//
//        [Fact]
//        public void given_first_frame_has_first_roll_is_5_pins_and_second_roll_is_5_pins_then_score_should_10()
//        {
//            Game game = new Game();
//            game.Roll(5);
//            game.Roll(5);
//            var score = game.Score();
//            score.Should().Be(10);
//        }
//
//        [Fact]
//        public void given_first_frame_has_5_pins_and_5_pins_and_second_frame_has_5_pins_then_score_should_20()
//        {
//            Game game = new Game();
//            game.Roll(5);
//            game.Roll(5);
//            game.Roll(5);
//            var score = game.Score();
//            score.Should().Be(20);
//        }
    }


    public class Game
    {
        private int totalScore;

        private readonly FrameList frames = new FrameList();

        public int CurrentFrameIndex
        {
            get { return this.frames.CurrentFrame().Index; }
        }

        public void Roll(int pins)
        {
            frames.AddPinsToCurrentFrame(pins);
            Frame currentFrame = frames.CurrentFrame();

            totalScore += currentFrame.Score();
        }

        public int Score()
        {
            return totalScore;
        }
    }

    public class FrameList : List<Frame>
    {
        public FrameList()
        {
            this.Capacity = 10;
        }

        public Frame CurrentFrame()
        {
            if (this.Count == 0)
            {
                return null;
            }
            for (int i = 0; i < this.Capacity; i++)
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

        public void AddPinsToCurrentFrame(int pins)
        {
            var currentFrame = this.CurrentFrame();
            if (currentFrame == null)
            {
                this.Add(new Frame(pins,this.Count+1));
            }
            else
            {
                currentFrame.Add(pins);
            }
        }
    }

    public class Frame
    {
        private Roll firstRoll;
        private Roll secondRoll;
        private int index;

        public Frame(int pins, int index)
        {
            this.index = index;
            this.firstRoll = new Roll(pins);
        }

        public int Index
        {
            get { return index; }
        }

        public bool Add(int numberOfPins)
        {
            if (firstRoll == null)
            {
                firstRoll = new Roll(numberOfPins);
                return true;
            }
            else if (secondRoll == null)
            {
                secondRoll = new Roll(numberOfPins);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Score()
        {
            var roll = this.firstRoll;
            var firstRollNumberOfPins = 0;
            if (roll != null)
            {
                firstRollNumberOfPins = roll.NumberOfPins;
            }
            int secondRollNumberOfPins = 0;

            if (this.secondRoll != null)
            {
                secondRollNumberOfPins = this.secondRoll.NumberOfPins;
            }
            return firstRollNumberOfPins + secondRollNumberOfPins;
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