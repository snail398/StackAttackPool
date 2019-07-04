using Pool;
using UniRx;
using UnityEngine;

namespace FeederSpace
{
    public class Feeder
    {
        public struct Ctx
        {
            public float timeToFeed;
            public float leftBorder;
            public float rightBorder;
            public float craneSpeed;
            public Vector2 defaultBoxPosition;
            public GameObject controller;
        };

        private Ctx _ctx;
        private PlaceFinder _placeFinder;
        private Crane _crane;

        public Feeder(Ctx ctx)
        {
            _ctx = ctx;
            PlaceFinder.Ctx placeFinderCtx = new PlaceFinder.Ctx
            {
                defaultBoxPosition = _ctx.defaultBoxPosition,
                leftBorder = _ctx.leftBorder,
                rightBorder = _ctx.rightBorder,
            };
            _placeFinder = new PlaceFinder(placeFinderCtx);
            Crane.Ctx craneCtx = new Crane.Ctx
            {
                placeFinder = _placeFinder,
                speed = _ctx.craneSpeed,
            };
            _crane = new Crane(craneCtx);
            Observable.Timer(System.TimeSpan.FromSeconds(_ctx.timeToFeed))
                .Repeat()
                .Subscribe(_ => Feed())
                .AddTo(_ctx.controller);
        }

        private void Feed()
        {
            _crane.SendToField(PoolManager.GetObject("Box",GetBoxStartPosition(), Quaternion.identity));
        }

        private Vector3 GetBoxStartPosition()
        {
            float XPos;

            if (Random.Range(0, 2) == 0) XPos = -_ctx.defaultBoxPosition.x;
            else XPos = _ctx.defaultBoxPosition.x;
            return new Vector3(XPos, _ctx.defaultBoxPosition.y, 0 );
        }
    }
}
