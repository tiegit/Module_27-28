using System;

[Serializable]
public class DeathConditionPair
{
    public DeathReason Reason;
    public Func<Enemy, bool> Condition;

    public DeathConditionPair(DeathReason reason, Func<Enemy, bool> condition)
    {
        Reason = reason;
        Condition = condition;
    }
}