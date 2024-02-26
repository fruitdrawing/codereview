namespace DefaultNamespace.MyScripts
{
    public struct PlaySfxEvent
    {
        public string AudioName;
        public PlaySfxEvent(string sfxName)
        {
            AudioName = sfxName;
        }
    }

    public class UnitSpawnEvent
    {
        public UnitSO UnitSO;
        public UnitSpawnEvent(UnitSO unitSo)
        {
            this.UnitSO = unitSo;
        }
    }
}