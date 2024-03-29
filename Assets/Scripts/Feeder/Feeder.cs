﻿using Pool;
using UniRx;
using UnityEngine;
using System.Collections.Generic;

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
            public List<Material> materials;
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
            Observable.Timer(System.TimeSpan.FromSeconds(_ctx.timeToFeed))
                .Repeat()
                .Subscribe(_ => Feed())
                .AddTo(_ctx.controller);
        }

        private void Feed()
        {
            SetBoxType(PoolManager.GetObject("Box", GetBoxStartPosition(), Quaternion.identity));
        }

        private Vector2 GetBoxStartPosition()
        {
            return _placeFinder.GetPositionToDrop();
        }

        private void SetBoxType(GameObject box)
        {
            int rnd = Random.Range(0, _ctx.materials.Count);
            box.GetComponent<MeshRenderer>().material = _ctx.materials[rnd];
        }
    }
}
