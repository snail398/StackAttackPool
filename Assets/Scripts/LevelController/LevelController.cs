using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FeederSpace;

namespace Root
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private float timeToFeed;
        [SerializeField] private float leftBorder;
        [SerializeField] private float rigthBorder;
        [SerializeField] private float craneSpeed;
        [SerializeField] private Vector2 defaultBoxPosition;

        private void Awake()
        {
            HeroInit();
            FeederInit();
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
            Feeder feeder = new Feeder(feederCtx);
        }
    }
}
