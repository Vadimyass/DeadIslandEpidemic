using Gameplay.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    class MeleeAttackController: CharacterAttack
    {
        private ITargetable _target;

        public override void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 35000))
                {
                    if (hit.collider.TryGetComponent(out ITargetable target))
                    {
                        _target = target;
                    }
                }

            base.Shoot();
        }

        public void DealDamage()
        {
            _target?.ApplyDamage(damage);
        }
    }
}
