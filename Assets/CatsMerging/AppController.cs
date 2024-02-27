using Assets.CatsMerging.Classes;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatMerge
{
    public class AppController : MonoBehaviour
    {
        public ConfigCatalogue ConfigCatalogue;
        public static ConfigCatalogue GlobalConfigCatalogue;

        Systems GameSystems;

        void Start()
        {
            if (ConfigCatalogue == null)
                ConfigCatalogue = new ConfigCatalogue();

            GlobalConfigCatalogue = ConfigCatalogue;

            //DOTween
            DOTween.Init();

            //Adding all features
            GameSystems = new GameSystems(ConfigCatalogue);

            //Feature starts here
            GameSystems.Startup();
        }

        void Update()
        {
            //Feature execute loops here
            GameSystems.Execute();
        }
    }
}