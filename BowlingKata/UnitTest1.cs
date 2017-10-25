using System;
using FluentAssertions;
using Xunit;

namespace BowlingKata
{
    public class UnitTest1
    {
        [Fact]
        public void given_a_game_which_all_rolls_are_0_when_playing_game_then_score_should_be_0()
        {
            //arrange
            Game game = new Game();
            for (int i = 0; i < 20; i++)
            {
                game.Roll(0);
            }
            
            //act
            int score = game.Score();
            
            //assert
            score.Should().Be(0);
        }

        [Fact]
        public void given_a_game_which_all_rolls_are_1_when_playin_game_then_score_should_be_20()
        {
            //arrange
            Game game = new Game();
            for (int i = 0; i < 20; i++)
            {
                game.Roll(1);
            }
            
            //act
            int score = game.Score();
            
            //assert
            score.Should().Be(20);
        }

        [Fact]
        public void given_a_game_which_frames_has_one_9_and_one_0_when_playin_game_then_score_should_be_90()
        {
            //arrange
            Game game = new Game();
            for (int i = 0; i < 10; i++)
            {
                game.Roll(9);
                game.Roll(0);
            }
            
            //act
            int score = game.Score();
            
            //assert
            score.Should().Be(90);
        }
    }

    public class Game
    {
        private int totalScore;

        public void Roll(int numberOfPins)
        {
            totalScore += numberOfPins;
        }

        public int Score()
        {
            return totalScore;
        }
    }
}