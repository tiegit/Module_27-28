using System;

[Serializable]
public class DeathCondition
{
    public DeathReason Reason;
    public Func<Enemy, bool> Condition;

    public DeathCondition(DeathReason reason, Func<Enemy, bool> condition)
    {
        Reason = reason;
        Condition = condition;
    }
}