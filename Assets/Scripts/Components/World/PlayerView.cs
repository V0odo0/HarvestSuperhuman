using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    public class PlayerView : SingletonMonoBehaviour<PlayerView>
    {
        [SerializeField] private Camera _camera;
    }
}
