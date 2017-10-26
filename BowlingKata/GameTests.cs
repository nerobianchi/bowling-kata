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
            RollMany(2, 5);

            var score = game.Score();
            score.Should().Be(10);
        }

        [Fact]
        public void given_first_frame_has_5_pins_and_5_pins_and_second_frame_has_5_pins_then_score_should_20()
        {
            RollMany(3, 5);

            var score = game.Score();
            score.Should().Be(20);
        }
    }
}