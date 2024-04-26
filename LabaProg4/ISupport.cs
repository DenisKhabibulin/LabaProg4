using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaProg4
{
    public interface ISupport
    {
        (Battler target, string abilityName) Support(List<Battler> team);
    }
}
