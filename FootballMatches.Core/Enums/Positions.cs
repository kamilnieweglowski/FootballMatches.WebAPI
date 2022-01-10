using System.ComponentModel;

namespace FootballMatches.Core.Enums
{
    public enum Positions
    {
        [Description("Goalkeeper")] Goalkeeper = 1,
        [Description("Defender")] Defender = 2,
        [Description("Midfielder")] Midfielder = 3,
        [Description("Forward")] Forward = 4
    }
}
