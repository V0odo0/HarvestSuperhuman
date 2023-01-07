using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public static GameDataAsset Data =>
            _data ?? (_data = Resources.Load<GameDataAsset>(nameof(GameDataAsset)));
        private static GameDataAsset _data;
        
        public PlantSurfaceProcessor SurfaceProcessor { get; private set; }

        public PlantSurface PlantSurface => _plantSurface;
        [SerializeField] private PlantSurface _plantSurface;


        protected override void Awake()
        {
            base.Awake();

            SurfaceProcessor = new PlantSurfaceProcessor(App.ActiveGameProfile.PlantSurface);
            PlantSurface.SetProcessor(SurfaceProcessor);
        }

        protected override void Update()
        {
            base.Update();

            SurfaceProcessor.Update();
        }
    }
}
