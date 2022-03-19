using DeadIsland.Events;
using Gameplay.Character.Leveling.Events;
using Gameplay.Character.Abilities;
using Gameplay.Character.Abilities.UpgradeEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Leveling
{
    public class HeroLeveling : MonoBehaviour
    {
        public int xp = 0;
        public int level = 1;
        private int _maxLevel = 20;
        [SerializeField] public int[] needXP = new int[10];
        public int upgradePoints;
        private AbilityContainer _abilities;

        public void SetParams(AbilityContainer abilityContainer)
        {
            _abilities = abilityContainer;
        }
        void Awake()
        {
            this.BindGameEventObserver<UpLevelEvent>(UpLevel);
            this.BindGameEventObserver<FirstAbilityUpgradeEvent>((eventBase) => UpgradeAbility(_abilities.firstAbility));
            this.BindGameEventObserver<SecondAbilityUpgradeEvent>((eventBase) => UpgradeAbility(_abilities.secondAbility));
            this.BindGameEventObserver<ThirdAbilityUpgradeEvent>((eventBase) => UpgradeAbility(_abilities.thirdAbility));
            this.BindGameEventObserver<UltimateAbilityUpgradeEvent>((eventBase) => UpgradeAbility(_abilities.ultimateAbility));
        }
        public void TakeXP(int addedXP)
        {
            if (level < _maxLevel)
            {
                xp += addedXP;
                while (xp >= needXP[level - 1])
                {
                    new UpLevelEvent().Invoke();
                }
                new TakeXpEvent().Invoke();
            }
        }
        public void UpLevel(EventBase eventBase)
        {
            level++;
            upgradePoints++;
        }

        public void UpgradeAbility(Ability ability)
        {
            if (upgradePoints != 0)
            {
                ability.UpLevel();
                upgradePoints--;
            }
        }
    }
}