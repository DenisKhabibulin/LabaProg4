using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace LabaProg4
{
    public class Healer : Battler, ISupport
    {
        public int HealingPower { get; set; }
        public Healer() : base(5, 30, 30, 10, 2, 0.9)
        {
            Name = "Healer";
            HealingPower = 7;
        }
        public (Battler, string) Support(List <Battler> team) //Heal
        {
            (int index, int priority) tmp = (-1, Int32.MaxValue); 
            for (int i = 0; i < team.Count; i++)
            {
                if (team[i].CurrentHealth == team[i].MaxHealth)
                    continue;
                if (tmp.priority > team[i].Priority && team[i].CurrentHealth != 0)
                {
                    tmp.priority = team[i].Priority;
                    tmp.index = i;
                }
            }
            if (tmp.index == -1)
                return (this, "nothing");
            team[tmp.index].CurrentHealth += HealingPower;
            currentMana -= spellCost;
            return (team[tmp.index], "heal");
        }
        public override string ShowStats()
        {
            string stats = base.ShowStats() + "Heal";
            return stats;
        }
    }
}
