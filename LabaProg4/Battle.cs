using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaProg4
{
    public static class Battle
    {
        public static string Round(List<Battler> teamA, List<Battler> teamB)
        {
            int sumOfHealthA = -1;
            int sumOfHealthB = -1;
            string log = "";
            (Battler target, string action) actionLog;
            do
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i < teamA.Count && teamA[i].CurrentHealth != 0 && sumOfHealthB !=0)
                    {
                        actionLog = Action(teamA[i], teamA, teamB);
                        if (teamA.Contains(actionLog.target))
                            log += $"{teamA[i].Name} (HP:{teamA[i].CurrentHealth}/{teamA[i].MaxHealth}) из команды Игрока 1 применяет заклинание {actionLog.action} " +
                                $"на союзного {actionLog.target.Name} (HP:{actionLog.target.CurrentHealth}/{actionLog.target.MaxHealth})\n";
                        else
                            log += $"{teamA[i].Name} (HP:{teamA[i].CurrentHealth}/{teamA[i].MaxHealth}) из команды Игрока 1 совершает {actionLog.action} " +
                                $"на {actionLog.target.Name} (HP:{actionLog.target.CurrentHealth}/{actionLog.target.MaxHealth}) противника\n";
                    }
                    if (i < teamB.Count && teamB[i].CurrentHealth != 0 && sumOfHealthA !=0)
                    {
                        actionLog = Action(teamB[i], teamB, teamA);
                        if (teamB.Contains(actionLog.target))
                            log += $"{teamB[i].Name} (HP:{teamB[i].CurrentHealth}/{teamB[i].MaxHealth}) из команды Игрока 2 применяет заклинание {actionLog.action} " +
                                $"на союзного {actionLog.target.Name} (HP:{actionLog.target.CurrentHealth}/{actionLog.target.MaxHealth})\n";
                        else
                            log += $"{teamB[i].Name} (HP:{teamB[i].CurrentHealth}/{teamB[i].MaxHealth}) из команды Игрока 2 совершает {actionLog.action} " +
                                 $"на {actionLog.target.Name} (HP:{actionLog.target.CurrentHealth}/{actionLog.target.MaxHealth}) противника\n";
                    }

                    sumOfHealthA = 0;
                    sumOfHealthB = 0;
                    foreach (Battler battler in teamA)
                        sumOfHealthA += battler.CurrentHealth;
                    foreach (Battler battler in teamB)
                        sumOfHealthB += battler.CurrentHealth;
                }
            }

            while (sumOfHealthA > 0 && sumOfHealthB > 0);

            foreach (Battler battler in teamA)
            {
                battler.CurrentHealth = battler.MaxHealth;
                battler.CurrentMana = battler.MaxMana;
            }
            foreach (Battler battler in teamB)
            {
                battler.CurrentHealth = battler.MaxHealth;
                battler.CurrentMana = battler.MaxMana;
            }

            if (sumOfHealthB == 0)
            {
                log +="Команда Игрока 1 выиграла раунд\n";
                return log;
            }
            else
            {
                log += "Команда Игрока 2 выиграла раунд\n";
                return log;
            }

        }
        public static (Battler, string) Action(Battler battler, List <Battler> myTeam, List<Battler> oppTeam)
        {
            Battler target = battler;
            var rand = new Random();
            if (rand.NextDouble() < battler.ProbabilityOfSpellUsage && battler.SpellCost <= battler.CurrentMana)
            {
                if (battler is ISupport support)
                    return support.Support(myTeam);

                if (battler is IAttacker attacker)
                    return attacker.StrongAttack(oppTeam);
                else
                    return (battler, "nothing");
            }
            else
                return (battler.Attack(oppTeam), "attack");
        }
    }
}
