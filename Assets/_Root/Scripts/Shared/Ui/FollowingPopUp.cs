namespace Kpi.Intro2GameDev.Assets.Shared.Ui
{
    using DG.Tweening;
    using JetBrains.Annotations;
    using strange.extensions.mediation.impl;
    using UnityEngine;

    public abstract class FollowingPopUp<T> : View where T : IFolowee
    {
        protected T Folowee;

        private Canvas canvas;

        public void Init(T folowee)
        {
            if (folowee?.Anchor == null)
            {

//                this.Close();
            }

            Folowee = folowee;
            canvas = GetComponentInParent<Canvas>();

            UpdatePosition();

            InitInternal();
        }

        protected virtual void InitInternal()
        {

        }

        protected void AnimateShow(float duration, float delay)
        {
            if (this.Folowee.Anchor == null)
            {
                return;
            }

            transform.DOScale(Vector3.zero, duration).From().SetEase(Ease.OutBack).SetDelay(delay);
            UpdatePosition();
        }

        protected void UpdatePosition()
        {
            if (this.Folowee.Anchor == null)
            {
                return;
            }

            Vector2 p;
            var screenPoint = Camera.main.WorldToScreenPoint(Folowee.Anchor.Value);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                screenPoint, Camera.main, out p);
            GetComponent<RectTransform>().anchoredPosition = new Vector3(p.x, p.y, 0);
        }

        [UsedImplicitly]
        public void LateUpdate()
        {
            if (Folowee?.Anchor != null)
            {
                UpdatePosition();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public interface IFolowee
    {
        Vector3? Anchor { get; }
    }
}