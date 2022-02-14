
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    class RangeAttackController : CharacterAttack
    {
        private Bullet _bullet;
        private GameObject _shootStart;

        public override void Shoot()
        {
            base.Shoot();
            
            var bullet = Object.Instantiate(_bullet,
                                            new Vector3(_shootStart.transform.position.x,
                                            _shootStart.transform.position.y,
                                            _shootStart.transform.position.z),
                                            _shootStart.transform.rotation);

            bullet.OnStart(damage);
        }

    }
}
