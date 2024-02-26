using System;

namespace DefaultNamespace.MyScripts.SkillRelated
{
    [Serializable]
    public struct SkillImplementData
    {
        public int Damage;
        public int ManaCost;
        public float DurationBeforeActualHit;
        public string VfxNameRightAfterCast;
        public string VfxNameOnEachHit;
        public string VfxNameAfterCast;
        public float Radius;
        public UnitSO UnitSO;
    }
}