namespace Kpi.Intro2GameDev.TowerDefense.Core.Towers.Shooters
{
    using JetBrains.Annotations;
    using UnityEngine;

    public class GunShooter : Shooter
    {
        [SerializeField, EditorAssigned]
        private Bullet bulletPrefab;

        [SerializeField, EditorAssigned]
        private Transform muzzle;

        [SerializeField, EditorAssigned]
        private GameObject weapon;

        protected override void Shot()
        {
            var aimOffset = weapon.transform.position - muzzle.position;
            var aimPosition = Enemy.transform.position + aimOffset + Vector3.up * 2;
            weapon.transform.rotation = Quaternion.LookRotation(aimPosition - weapon.transform.position);

            var bullet = Instantiate(bulletPrefab, muzzle, false);
            bullet.Shoot(muzzle.position, muzzle.rotation, muzzle.forward);
        }
    }
}