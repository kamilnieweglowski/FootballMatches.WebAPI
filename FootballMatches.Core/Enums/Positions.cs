using System.ComponentModel;

namespace FootballMatches.Core.Enums
{
    public enum Positions
    {
        [Description("Goalkeeper")] Goalkeeper = 1,
        [Description("Defender")] Defender = 2,
        [Description("Midfielder")] Midfielder = 3,
        [Description("Forward")] Forward = 4,

        //[Description("Right Full-back")] RightFullBack = 5,
        //[Description("Left Full-back")] LeftFullBack = 6,
        //[Description("Center-back")] CenterBack = 7,
        //[Description("Sweeper")] Sweeper = 8,
        //[Description("Defensive Midfielder")] DefensiveMidfielder = 9,
        //[Description("Attacking Midfielder")] AttackingMidfielder = 10,
        //[Description("Right Midfielder")] RightMidfielder = 11,
        //[Description("Left Midfielder")] LeftMidfielder = 12,
        //[Description("Center Midfielder")] CenterMidfielder = 13,
        //[Description("Striker")] Striker = 14,
    }
}
