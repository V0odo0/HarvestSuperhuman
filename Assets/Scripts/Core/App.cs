using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    public static class App
    {
        public static AppDataAsset AppData =>
            _appData ?? (_appData = Resources.Load<AppDataAsset>(nameof(AppDataAsset)));
        private static AppDataAsset _appData;
    }
}
