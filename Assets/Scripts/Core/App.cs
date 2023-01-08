using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HSH
{
    public static class App
    {
        public static AppDataAsset Data =>
            _data ?? (_data = Resources.Load<AppDataAsset>(nameof(AppDataAsset)));
        private static AppDataAsset _data;

        public static UserAppConfig UserConfig =>
            _userConfig ?? (_userConfig = UserAppConfig.Load() ?? new UserAppConfig());
        private static UserAppConfig _userConfig;

        public static UserLocalGameProfile UserLocalGameProfile =>
            _userLocalGameProfile ?? (_userLocalGameProfile = UserLocalGameProfile.Load() ?? new UserLocalGameProfile());
        private static UserLocalGameProfile _userLocalGameProfile;

        public static GameProfileData ActiveGameProfile => UserLocalGameProfile.GameProfileData;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Init()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += change =>
            {
                switch (change)
                {
                    case PlayModeStateChange.ExitingPlayMode:
                        OnQuit();
                        break;
                }
            };
#else
            Application.quitting += OnQuit;
#endif
        }


        static void OnQuit()
        {
            _userConfig?.Save();
            _userLocalGameProfile?.Save();

            _userConfig = null;
            _userLocalGameProfile = null;
        }
    }
}
