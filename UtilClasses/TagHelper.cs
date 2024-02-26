using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.UtilClasses;

namespace UtilClasses
{
    public static class TagHelper
    {
        public static string[] AllTeams()
        {
            return new string[]
            {
                UnitTeam.PlayerTeam.ToString(),
                UnitTeam.EnemyTeam.ToString()
            };
        }
    }
}