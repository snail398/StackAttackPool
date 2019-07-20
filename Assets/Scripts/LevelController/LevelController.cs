using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FeederSpace;
using LayerCheckerSpace;
using System;

namespace Root
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private float timeToFeed;
        [SerializeField] private float craneSpeed;
        [SerializeField] private Vector2 defaultBoxPosition;
        [SerializeField] private LayerChecker layerCheckerPrefab;
        [SerializeField] private int boxInRowCount;
        [SerializeField] private List<Material> materials;
        [SerializeField] private Transform leftWall;
        [SerializeField] private Transform rightWall;
        [SerializeField] private Camera mcamera;
        private void Awake()
        {
            HeroInit();
            ConstructorInit();
            FeederInit();
            LevelCheckersInit();

        }

        private void HeroInit()
        {
            //Инициализация персонажа
        }

        private void FeederInit()
        {
            //Инициализация податчика ящиков
            Feeder.Ctx feederCtx = new Feeder.Ctx
            {
                controller = this.gameObject,
                defaultBoxPosition = defaultBoxPosition,
                timeToFeed = timeToFeed,
                rightBorder = rightWall.position.x - 1,
                leftBorder = leftWall.position.x + 1,
                craneSpeed = craneSpeed,
                materials = materials,
            };
            Feeder feeder = new Feeder(feederCtx);
        }

        private void LevelCheckersInit()
        {
            LayerChecker.Ctx layersCheckerCtx = new LayerChecker.Ctx
            {
                boxInRowCount = boxInRowCount,
            };
            for (int i = 1; i < GetLayerCheckerCount(); i++)
            {
                LayerChecker temp = Instantiate(layerCheckerPrefab, new Vector2(-6, 1 + (i - 1) * 1), Quaternion.identity);
                temp.SetContext(layersCheckerCtx);
                temp.StartCheck();
            }
        }

        private int GetLayerCheckerCount()
        {
            return 10;
        }

        private void ConstructorInit()
        {
            
            LevelConstructor.Ctx levelConstructorCtx = new LevelConstructor.Ctx
            {
                boxInRowCount = boxInRowCount,
                leftWall = leftWall,
                rightWall = rightWall,
                mcamera = mcamera,
            };
            LevelConstructor levelConstructor = new LevelConstructor(levelConstructorCtx);
        }
    }
}
