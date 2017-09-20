namespace Kpi.Intro2GameDev.TowerDefense.Core.Towers
{
    using DG.Tweening;
    using Enemies;
    using JetBrains.Annotations;
    using UnityEngine;
    public class Bullet : MonoBehaviour
    {
        public void Shoot()
        {
        }

        [UsedImplicitly]
        public void OnTriggerEnter(Collider other)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.HitWith(3);
                Destroy(gameObject);
            }
        }

        public void Shoot(Vector3 muzzlePosition, Quaternion muzzleRotation, Vector3 direction)
        {
            transform.position = muzzlePosition;
            transform.rotation = muzzleRotation;

            transform.DOMove(direction * 30, 80)
                     .SetSpeedBased(true)
                     .SetEase(Ease.Linear)
                     .SetRelative(true)
                     .OnComplete(() =>
                     {
                         if (gameObject)
                         {
                             var tweener =
                                 transform.DOMove(direction * 5, 20)
                                          .SetSpeedBased(true)
                                          .SetEase(Ease.Linear)
                                          .SetRelative(true);
                             var duration = 5 / 20f;
                             transform.DOScale(0, duration)
                                      .SetEase(Ease.InElastic)
                                      .OnComplete(() => Destroy(gameObject));
                         }
                     });
        }
    }
}