using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace LabaProg4
{
    public class Knight : Battler, IAttacker
    {
        public Knight() : base(6, 50, 5, 5, 0, 0.5)
        {
            Name = "Knight";
        }
        public (Battler, string) StrongAttack(List<Battler> team) // SplashAttack
        {
            Battler target = Attack(team);
            for (int i = 0; i < team.Count; i++)
            {
                if (team[i].Equals(target))
                {
                    if (i - 1 >= 0)
                        team[i - 1].CurrentHealth -= damage / 3;
                    if (i + 1 < team.Count)
                        team[i + 1].CurrentHealth -= damage / 3;
                    currentMana -= spellCost;
                    break;
                }
            }
            return (target, "splash Attack");
        }
        public override string ShowStats()
        {
            string stats = base.ShowStats() + "Splash Attack";
            return stats;
        }
    }
}
