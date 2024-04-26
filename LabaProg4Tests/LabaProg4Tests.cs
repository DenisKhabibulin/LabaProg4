using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabaProg4;

namespace LabaProg4.Tests
{
    [TestClass()]
    public class AbilitiesTests
    {
        [TestMethod()]
        public void AttackTest()
        {
            List<Battler> team = new List<Battler>();
            Knight knight = new Knight();
            Archer archer = new Archer();
            Healer healer = new Healer();
            knight.Priority = 0;
            archer.Priority = 1;
            healer.Priority = 2;
            team.Add(knight); // - highest priority
            team.Add(archer);
            team.Add(healer);
            Bard bard = new Bard();
            bard.Attack(team);
            int expectedHealth = knight.MaxHealth - bard.Damage;
            int actualHealth = knight.CurrentHealth;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [TestMethod()]
        public void SplashAttackTest()
        {
            List<Battler> team = new List<Battler>();
            Bard bard = new Bard();
            Archer archer = new Archer();
            Healer healer = new Healer();
            archer.Priority = 0;
            bard.Priority = 1;
            healer.Priority = 2;
            team.Add(bard);
            team.Add(archer);// - highest priority
            team.Add(healer);
            Knight knight = new Knight();
            knight.StrongAttack(team);
            int[] expectedHealth = { bard.MaxHealth - knight.Damage / 3, archer.MaxHealth - knight.Damage, healer.MaxHealth - knight.Damage / 3 };
            int[] actualHealth = { bard.CurrentHealth, archer.CurrentHealth, healer.CurrentHealth };
            for (int i = 0; i < expectedHealth.Length; i++)
                Assert.AreEqual(expectedHealth[i], actualHealth[i]);
        }

        [TestMethod()]
        public void HealTest()
        {
            List<Battler> team = new List<Battler>();
            Knight knight = new Knight();
            Archer archer = new Archer();
            Healer healer = new Healer();
            knight.Priority = 0;
            archer.Priority = 1;
            healer.Priority = 2;
            team.Add(knight); // - highest priority
            team.Add(archer);
            team.Add(healer);
            knight.CurrentHealth /= 2;
            archer.CurrentHealth /= 2;
            healer.CurrentHealth /= 2;
            int expectedHealth = knight.CurrentHealth + healer.HealingPower;
            healer.Support(team);
            int actualHealth = knight.CurrentHealth;
            Assert.AreEqual(expectedHealth, actualHealth);
        }

        [TestMethod()]
        public void RicochetTest()
        {
            List<Battler> team = new List<Battler>();
            Bard bard = new Bard();
            Knight knight = new Knight();
            Healer healer = new Healer();
            knight.Priority = 0;
            bard.Priority = 1;
            healer.Priority = 2;
            bard.MaxHealth = 50;
            bard.CurrentHealth = bard.MaxHealth;
            knight.MaxHealth = 100;
            knight.CurrentHealth = knight.MaxHealth;
            healer.MaxHealth = 75;
            healer.CurrentHealth = healer.MaxHealth;
            team.Add(bard); // - lowest health
            team.Add(knight);// - highest priority
            team.Add(healer);
            Archer archer = new Archer();
            archer.StrongAttack(team);
            int[] expectedHealth = { bard.MaxHealth - archer.Damage / 2, knight.MaxHealth - archer.Damage, healer.MaxHealth };
            int[] actualHealth = { bard.CurrentHealth, knight.CurrentHealth, healer.CurrentHealth };
            for (int i = 0; i < expectedHealth.Length; i++)
                Assert.AreEqual(expectedHealth[i], actualHealth[i]);
        }

        [TestMethod()]
        public void BuffTest()
        {
            List<Battler> team = new List<Battler>();
            Knight knight = new Knight();
            Archer archer = new Archer();
            Bard bard = new Bard();
            knight.Priority = 0;
            archer.Priority = 1;
            bard.Priority = 2;
            team.Add(knight); // - highest priority
            team.Add(archer);
            team.Add(bard);
            int expectedDamage = knight.Damage + bard.BuffPower;
            bard.Support(team);
            int actualDamage = knight.Damage;
            Assert.AreEqual(expectedDamage, actualDamage);
        }
    }
}