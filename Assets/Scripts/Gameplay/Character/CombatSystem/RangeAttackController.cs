
using Gameplay.Character.AnimationControllers;
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    class RangeAttackController : CharacterAttack
    {
        private RangeWeapon _rangeWeapon;

        public RangeAttackController(CharacterAnimationController animationController, Weapon rangeWeapon)
        {
            this._characterAnimator = animationController;
            _rangeWeapon = rangeWeapon as RangeWeapon;
            _attackType = AttackType.Range;
        }

        public override void Shoot()
        {
            base.Shoot();
            
            var bullet = Object.Instantiate(Resources.Load<Bullet>(_rangeWeapon.BULLET_PATH),
                                            new Vector3(_rangeWeapon.shotStart.transform.position.x,
                                            _rangeWeapon.shotStart.transform.position.y,
                                            _rangeWeapon.shotStart.transform.position.z),Quaternion.identity);

            bullet.OnStart(_rangeWeapon.damage);
        }

    }
}
