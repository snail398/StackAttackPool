using UnityEngine;
using UniRx;
using System.Collections.Generic;

namespace LayerCheckerSpace
{
    public class LayerChecker : MonoBehaviour
    {
        public struct Ctx
        {
            public int boxInRowCount;
        }

        private Ctx _ctx;
        private RaycastHit[] hits;

        public void SetContext(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void StartCheck()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => Check())
                .AddTo(this);
        }

        private void Check()
        {
            List<PoolObject> boxList = new List<PoolObject>();
            hits = Physics.RaycastAll(transform.position, Vector3.right);
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject.tag == "Box")
                    if (hit.transform.gameObject.GetComponent<GroundControl>().IsGrounded)
                        boxList.Add(hit.transform.gameObject.GetComponent<PoolObject>());
            }
            if (boxList.Count == _ctx.boxInRowCount)
            {
                DeleteRow(boxList);
            }
        }

        private void DeleteRow(List<PoolObject> list)
        {
            foreach (PoolObject item in list)
            {
                item.ReturnToPool();
            }
        }
    }
}
