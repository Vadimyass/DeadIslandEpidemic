
using Gameplay.Character.AnimationControllers;
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    class RangeAttackController : CharacterAttack
    {
        private Bullet _bullet;
        private GameObject _shootStart;

        public RangeAttackController(CharacterAnimationController animationController, GameObject shootStart)
        {
            this._characterAnimator = animationController;
            _shootStart = shootStart;
            _attackType = AttackType.Range;
        }

        public override void Shoot()
        {
            base.Shoot();
            
            var bullet = Object.Instantiate(Resources.Load<Bullet>("bullet"),
                                            new Vector3(_shootStart.transform.position.x,
                                            _shootStart.transform.position.y,
                                            _shootStart.transform.position.z),
                                            _shootStart.transform.rotation);

            bullet.OnStart(damage);
        }

    }
}
