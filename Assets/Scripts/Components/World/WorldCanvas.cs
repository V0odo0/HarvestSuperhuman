using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    [RequireComponent(typeof(Canvas))]
    public class WorldCanvas : MonoBehaviourBase
    {
        public Canvas Canvas => _canvas ?? (_canvas = GetComponent<Canvas>());
        private Canvas _canvas;


        protected override void Awake()
        {
            base.Awake();

            Canvas.worldCamera = Camera.main;
        }
    }
}
