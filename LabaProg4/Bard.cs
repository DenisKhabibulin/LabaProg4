using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace LabaProg4
{
    public class Bard : Battler, ISupport
    {
        public int BuffPower { get; set; }
        public Bard() : base(7, 25, 20, 5, 3, 0.7)
        {
            Name = "Bard";
            BuffPower = 6;
        }
        public (Battler, string) Support(List <Battler> team) //buff
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
                return (this, "nothing");
            team[tmp.index].Damage += BuffPower;
            currentMana -= spellCost;
            return (team[tmp.index], "increase damage");
        }
        public override string ShowStats()
        {
            string stats = base.ShowStats() + "Buff";
            return stats;
        }
    }
}
