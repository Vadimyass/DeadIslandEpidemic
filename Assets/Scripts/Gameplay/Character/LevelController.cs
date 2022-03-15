using DeadIsland.Events;
using Gameplay.Character;
using Gameplay.Character.Ability;
using Gameplay.Character.Ability.UpgradeEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int xp = 0;
    public int level = 0;
    [SerializeField] public int[] needXP = new int[10];
    public int upgradePoints;
    [SerializeField] private AbilityContainer _abilities;

    void Start()
    {
        this.BindGameEventObserver<UpLevelEvent>(UpLevel);
        this.BindGameEventObserver<FirstAbilityUpgradeEvent>((eventBase) => UpgradeAbility(_abilities.firstAbility));
        this.BindGameEventObserver<SecondAbilityUpgradeEvent>((eventBase) => UpgradeAbility(_abilities.secondAbility));
        this.BindGameEventObserver<ThirdAbilityUpgradeEvent>((eventBase) => UpgradeAbility(_abilities.thirdAbility));
        this.BindGameEventObserver<UltimateAbilityUpgradeEvent>((eventBase) => UpgradeAbility(_abilities.ultimateAbility));
    }
    public void TakeXP(int addedXP)
    {
        xp += addedXP;
        while (xp > needXP[level])
        {
            if (xp > needXP[level])
            {
                new UpLevelEvent().Invoke();
            }
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
