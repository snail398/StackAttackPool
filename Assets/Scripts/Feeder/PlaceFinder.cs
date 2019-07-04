using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FeederSpace
{
    public class PlaceFinder
    {
        public struct Ctx
        {
            public float leftBorder;
            public float rightBorder;
            public Vector2 defaultBoxPosition;
        };

        private Ctx _ctx;

        public PlaceFinder(Ctx ctx)
        {
            _ctx = ctx;
        }

        public Vector2 GetPositionToDrop()
        {
            return new Vector2(Random.Range(_ctx.leftBorder, _ctx.rightBorder), _ctx.defaultBoxPosition.y);
        }
    }
}
