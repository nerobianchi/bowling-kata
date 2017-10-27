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
        public void given_1_roll_has_5_pins_then_score_should_5()
        {
            game.Roll(5);
            game.Score().Should().Be(5);
        }

        [Fact]
        public void given_2_rolls_has_5_pins_then_score_should_10()
        {
            RollMany(2, 5);

            game.Score().Should().Be(10);
        }

        [Fact]
        public void given_3_rolls_has_5_pins_then_score_should_20()
        {
            RollMany(3, 5);

            game.Score().Should().Be(20);
        }

        [Fact]
        public void given_3_roll_has_5_pins_and_1_roll_has_1_then_score_should_21()
        {
            RollMany(3, 5);
            game.Roll(1);

            game.Score().Should().Be(21);
        }
        [Fact]
        public void given_10_frame_has_rolls_has_5_pins_each_and_5_pins_for_extra_roll_then_score_should_150()
        {
            for (int i = 0; i < 10; i++)
            {
                game.Roll(5);
                game.Roll(5);
            }
            game.Roll(5);

            game.Score().Should().Be(150);
        }
    }
}