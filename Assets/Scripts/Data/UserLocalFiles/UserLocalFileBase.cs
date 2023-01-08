using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace HSH
{
    public class UserLocalFileBase<T>
    {
        public static string FolderName => Application.persistentDataPath;
        public static string FileName => $"{typeof(T).Name}.dat";
        public static string FilePath => Path.Combine(FolderName, FileName);


        public virtual bool Save()
        {
            try
            {
                File.WriteAllText(FilePath, JsonUtility.ToJson(this, true));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T Load()
        {
            var d = File.Exists(FilePath) ? JsonUtility.FromJson<T>(File.ReadAllText(FilePath)) : default(T);
            if (d == null)
                return Activator.CreateInstance<T>();

            return d;
        }
    }
}
