public abstract class Ability
{
    public string Name { get; protected set; }
    public int Cooldown { get; protected set; }
    public int CooldownTimer { get; protected set; }

    public abstract void Use(Unit user, Unit target);

    public void DecreaseCooldown()
    {
        if (CooldownTimer > 0) CooldownTimer--;
    }

    public bool IsReady() => CooldownTimer == 0;
}

public class Attack : Ability
{
    public Attack()
    {
        Name = "Attack";
        Cooldown = 0; 
    }

    public override void Use(Unit user, Unit target)
    {
        target.TakeDamage(8);
    }
}

public class Barrier : Ability
{
    public Barrier()
    {
        Name = "Barrier";
        Cooldown = 4;
    }

    public override void Use(Unit user, Unit target)
    {
        user.BarrierValue = 5; 
        CooldownTimer = Cooldown;
    }
}

public class Heal : Ability
{
    public Heal()
    {
        Name = "Heal";
        Cooldown = 5;
    }

    public override void Use(Unit user, Unit target)
    {
        user.HealDuration = 3;
        CooldownTimer = Cooldown;
    }
}

public class Fireball : Ability
{
    public Fireball()
    {
        Name = "FireBall";
        Cooldown = 6;
    }

    public override void Use(Unit user, Unit target)
    {
        target.TakeDamage(5); 
        target.BurningDuration = 5; 
        CooldownTimer = Cooldown;
    }
}

public class Purify : Ability
{
    public Purify()
    {
        Name = "Purify";
        Cooldown = 5;
    }

    public override void Use(Unit user, Unit target)
    {
        user.BurningDuration = 0;
        CooldownTimer = Cooldown;
    }
}
