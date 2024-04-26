using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace LabaProg4
{
    public class Archer : Battler, IAttacker
    {
        public Archer() : base(10, 35, 10, 5, 1, 0.5)
        {
            Name = "Archer";
        }
        public (Battler, string) StrongAttack(List<Battler> team) // Ricochet
        {
            (int, int) tmp = (-1, Int32.MaxValue);
            Battler target = Attack(team);
            for (int i = 0; i < team.Count; i++)
            {
                if (team[i].Equals(target))
                    continue;
                if (team[i].CurrentHealth < tmp.Item2 && team[i].CurrentHealth != 0)
                {
                    tmp.Item2 = team[i].CurrentHealth;
                    tmp.Item1 = i;
                }
            }
            if (tmp.Item1 == -1)
                return (this, "nothing");
            team[tmp.Item1].CurrentHealth -= (damage / 2);
            currentMana -= spellCost;
            return (target, "ricochet");
        }
        public override string ShowStats()
        {
            string stats = base.ShowStats() + "Ricochet";
            return stats;
        }
    }
}
