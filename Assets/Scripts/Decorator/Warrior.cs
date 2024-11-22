using UnityEngine;

public abstract class Warrior
{
    string description = "ภüป็";

    public virtual string Description { get { return description; } }

    public abstract float Attack();
}

public class RealWarrior : Warrior
{
    public override string Description => base.Description;
    public override float Attack()
    {
        return 10;
    }
}

public abstract class WarriorDecorator : Warrior
{
    public Warrior warrior;
}

public class Sword : WarriorDecorator 
{ 
    public Sword(Warrior warrior)
    {
        this.warrior = warrior;
    }

    public override string Description {  get { return warrior.Description + " with Sword "; } }
    public override float Attack()
    {
        return warrior.Attack() + 10;
    }
}

public class Shield : WarriorDecorator
{
    public Shield(Warrior warrior)
    {
        this.warrior = warrior;
    }

    public override string Description { get { return warrior.Description + " with Shield "; } }
    public override float Attack()
    {
        return warrior.Attack() + 3;
    }
}

