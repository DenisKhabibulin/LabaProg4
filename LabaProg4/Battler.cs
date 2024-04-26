using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LabaProg4
{
    public abstract class Battler
    {
        protected int damage, currentHealth, maxHealth, currentMana, maxMana, spellCost, level, priority;
        protected double probabilityOfSpellUsage;
        public string Name { get; set; }
        public int Damage { get => damage; set { damage = value; } }
        public int Priority { get => priority; set => priority = value; }
        public int SpellCost { get => spellCost; }
        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                if (value > maxHealth)
                {
                    currentHealth = maxHealth;
                    return;
                }
                if (value < 0)
                {
                    currentHealth = 0;
                    return;
                }
                currentHealth = value;
            }
        }
        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        public int MaxMana { get => maxMana; }
        public int CurrentMana
        {
            get { return currentMana; }
            set
            {
                if (value > maxMana)
                {
                    currentMana = maxMana;
                    return;
                }
                if (value < 0)
                {
                    currentMana = 0;
                    return;
                }
                currentMana = value;
            }
        }

        public double ProbabilityOfSpellUsage { get => probabilityOfSpellUsage; }

        public Battler(int damage, int maxHealth, int maxMana, int spellCost, int priority, double probabilityOfSpellUsage)
        {
            Name = "Battler";
            this.damage = damage;
            this.currentHealth = maxHealth;
            this.maxHealth = maxHealth;
            this.maxMana = maxMana;
            currentMana = maxMana;
            this.spellCost = spellCost;
            this.priority = priority;
            this.probabilityOfSpellUsage = probabilityOfSpellUsage;
        }

        public Battler Attack(List <Battler> team)
        {
            (int index, int priority) tmp = (-1, Int32.MaxValue); 
            for (int i = 0; i < team.Count; i++)
            {
                if (tmp.priority > team[i].Priority && team[i].CurrentHealth != 0)
                {
                    tmp.priority = team[i].Priority;
                    tmp.index = i;
                }
            }
            if (tmp.index == -1)
                return this;
            team[tmp.index].CurrentHealth -= damage;
            return team[tmp.index];
        }
        public virtual string ShowStats()
        {
            string stats = "HP: " + Convert.ToString(MaxHealth) +"\n" + "Damage: " + Convert.ToString(Damage)+"\n" + "Mana: " + Convert.ToString(MaxMana)+"\n" + "Spell cost: " + Convert.ToString(SpellCost)+"\n";
            return stats;
        }
        
    }
}

