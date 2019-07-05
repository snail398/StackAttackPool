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
        [SerializeField] private float leftBorder;
        [SerializeField] private float rigthBorder;
        [SerializeField] private float craneSpeed;
        [SerializeField] private Vector2 defaultBoxPosition;
        [SerializeField] private LayerChecker layerCheckerPrefab;
        [SerializeField] private int boxInRowCount;

        private void Awake()
        {
            HeroInit();
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
                rightBorder = rigthBorder,
                leftBorder = leftBorder,
                craneSpeed = craneSpeed,
            };
           // Feeder feeder = new Feeder(feederCtx);
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
    }
}
