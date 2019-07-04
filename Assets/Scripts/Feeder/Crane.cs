using System;
using UnityEngine;
using UniRx;

namespace FeederSpace
{
    public class Crane
    {
        public struct Ctx
        {
            public float speed;
            public PlaceFinder placeFinder;
        };

        private Ctx _ctx;
        private bool _isBusy;
        private IDisposable updateHandler;

        public Crane(Ctx ctx)
        {
            _ctx = ctx;
            _isBusy = false;
        }

        public void SendToField(GameObject objectToSend)
        {
            if (_isBusy) return;
            Vector2 placeToDrop = _ctx.placeFinder.GetPositionToDrop();
            _isBusy = true;
            Rigidbody rb = objectToSend.GetComponent<Rigidbody>();
            rb.useGravity = false;
            updateHandler = Observable.EveryUpdate()
                .Subscribe(_ => MoveTo(placeToDrop, objectToSend.transform, rb));
        }

        private void MoveTo(Vector2 position, Transform transform, Rigidbody rb)
        {
            float tempSpeed;
            tempSpeed = _ctx.speed * Mathf.Sign(position.x - transform.position.x);
            transform.position = new Vector2(transform.position.x + tempSpeed * Time.deltaTime,transform.position.y);
            if (Mathf.Abs(position.x - transform.position.x) < 0.1f)
            {
                rb.useGravity = true;
                updateHandler?.Dispose();
                _isBusy = false;
                return;
            }
        }
    }
}
