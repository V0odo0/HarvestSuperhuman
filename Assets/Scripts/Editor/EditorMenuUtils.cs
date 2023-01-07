using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HSH.Editor
{
    public static class EditorMenuUtils
    {
        [MenuItem("HSH/Open User Local")]
        static void OpenUserLocal()
        {
            EditorUtility.RevealInFinder(UserLocalFileBase<Object>.FolderName);
        }
    }
}
