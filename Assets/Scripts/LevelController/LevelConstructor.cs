using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelConstructor : MonoBehaviour
{
    public struct Ctx
    {
        public Transform leftWall;
        public Transform rightWall;
        public int boxInRowCount;
        public Camera mcamera;
    }

    private Ctx _ctx;
    public LevelConstructor(Ctx ctx)
    {
        _ctx = ctx;
        LevelCreate();
    }

    public void LevelCreate()
    {
        _ctx.leftWall.position = new Vector3((-1)*(((float)(_ctx.boxInRowCount))/2) - 0.5f, _ctx.leftWall.position.y, _ctx.leftWall.position.z);
        _ctx.rightWall.position = new Vector3((((float)(_ctx.boxInRowCount)) / 2) + 0.5f, _ctx.rightWall.position.y, _ctx.rightWall.position.z);
        _ctx.mcamera.orthographicSize = _ctx.boxInRowCount;
        _ctx.mcamera.transform.position = new Vector3(_ctx.mcamera.transform.position.x, _ctx.boxInRowCount, _ctx.mcamera.transform.position.z);
 
        Debug.Log(_ctx.leftWall.position.x + " " + _ctx.rightWall.position.x);

    }
}