using DefaultNamespace.MyScripts;
using DefaultNamespace.MyScripts.SharedData;

namespace SharedData
{
    public struct UnitSpawnData
    {
        public string OwnerName { get; set; }
        public UnitTeam Team { get; set; }
        public bool MoveToDestinationImmediately { get; set; }
        public UnitSO UnitSO { get; set; }
        public int ManaRequiredToSpawn { get; set; }
        public bool IsPlayerController { get; set; }
        public UnitSpawnData(string ownerName, UnitTeam targetTeam,bool moveToDestinationImmediately,UnitSO unitSo,bool isPlayerController = false)
        {
            OwnerName = ownerName;
            Team = targetTeam;
            UnitSO = unitSo;
            MoveToDestinationImmediately = moveToDestinationImmediately;
            ManaRequiredToSpawn = unitSo.ManaRequiredToSpawn;
            IsPlayerController = isPlayerController;
        }
        
    }
}