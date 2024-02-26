namespace DefaultNamespace.MyScripts.SharedData
{
    public struct DamageData
    {
        public int DamageAmount;
        public float PushPower;
        public float PushDuration;

        public DamageData(int damageAmount, float pushPower,float pushDuration)
        {
            DamageAmount = damageAmount;
            PushPower = pushPower;
            PushDuration = pushDuration;
        }
    }
}