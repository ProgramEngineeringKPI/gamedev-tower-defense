namespace Kpi.Intro2GameDev.Assets.TowerDefense.Utils
{
    using JetBrains.Annotations;

    using strange.extensions.mediation.impl;

    using UnityEngine;

    [RequireComponent(typeof(BoxCollider))]
    public class ItemMover : MonoBehaviour
    {
        private GameObject movingObject;
        
        [UsedImplicitly]
        private void OnMouseDrag()
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            //Debug.DrawRay(ray.origin, ray.direction * 100);
            RaycastHit info;
            if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Plane")))
            {
                transform.position = info.point;
            }
        }

        public void SetObject(GameObject gameObject)
        {
            movingObject = gameObject;
            gameObject.transform.SetParent(transform, false);
        }
    }
}