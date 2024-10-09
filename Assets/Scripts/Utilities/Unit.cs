using System.Collections.Generic;

public class Unit 
{
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public List<Ability> Abilities { get; private set; }
        public int BarrierValue { get; set; }
        public int BurningDuration { get; set; }
        public int HealDuration { get; set; }

        public Unit(string name, int maxHealth, List<Ability> abilities)
        {
            Name = name;
            Health = maxHealth;
            MaxHealth = maxHealth;
            Abilities = abilities;
        }

        public void TakeDamage(int damage)
        {
            if (BarrierValue > 0)
            {
                BarrierValue -= damage;
                if (BarrierValue < 0)
                {
                    Health += BarrierValue;
                    BarrierValue = 0;
                }
            }
            else
            {
                Health -= damage;
            }
            if (Health < 0) Health = 0;
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }
}
