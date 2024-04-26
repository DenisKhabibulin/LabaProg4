using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaProg4
{
    public interface IAttacker
    {
        (Battler target, string abilityName) StrongAttack(List <Battler> team);
    }
}
