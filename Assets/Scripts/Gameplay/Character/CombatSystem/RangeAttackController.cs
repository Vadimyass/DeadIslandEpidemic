
using Gameplay.Character.AnimationControllers;
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    class RangeAttackController : CharacterAttack
    {
        private Bullet _bullet;
        private GameObject _shootStart;
        private RangeWeapon _rangeWeapon;

        public RangeAttackController(CharacterAnimationController animationController, RangeWeapon rangeWeapon)
        {
            this._characterAnimator = animationController;
            _shootStart = rangeWeapon.shotStart;
            _rangeWeapon = rangeWeapon;
            _attackType = AttackType.Range;
        }

        public override void Shoot()
        {
            base.Shoot();
            
            var bullet = Object.Instantiate(Resources.Load<Bullet>(_rangeWeapon.bulletName),
                                            new Vector3(_shootStart.transform.position.x,
                                            _shootStart.transform.position.y,
                                            _shootStart.transform.position.z),
                                            _shootStart.transform.rotation);

            bullet.OnStart(_rangeWeapon.damage);
        }

    }
}
