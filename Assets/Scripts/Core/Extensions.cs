using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;
using Rect = UnityEngine.Rect;

namespace HSH
{
    public static class Extensions
    {
        static Vector2[] EmptyVector2Array = new Vector2[0];
        static Random GlobalRng = new Random();


        public static void DestroyGameObjects<T>(this IEnumerable<T> t) where T : MonoBehaviour
        {
            if (t == null)
                return;

            foreach (var o in t)
            {
                if (o == null)
                    continue;

                Object.Destroy(o.gameObject);
            }
        }

        public static bool Equals(this Resolution r1, Resolution r2)
        {
            return r1.width == r2.width && r1.height == r2.height && r1.refreshRate == r2.refreshRate;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = GlobalRng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void ShuffleWithSeed<T>(this IList<T> list, int seed)
        {
            Random rng = new Random(seed);
            
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string ToStringFine(this double[] d)
        {
            if (d == null || d.Length == 0)
                return string.Empty;

            var b = new StringBuilder();

            for (var i = 0; i < d.Length; i++)
            {
                b.Append(i);
                b.Append(':');
                b.Append(' ');
                b.Append(d[i]);
            }

            return b.ToString();
        }

        public static Vector2 ToVector2(this double2 d)
        {
            return new Vector2(Convert.ToSingle(d.x), Convert.ToSingle(d.y));
        }

        public static Vector2Int Clamp(this Vector2Int value, int min, int max)
        {
            return new Vector2Int(Mathf.Clamp(value.x, min, max), Mathf.Clamp(value.y, min, max));
        }

        public static Vector2 Clamp(this Vector2 value, float min, float max)
        {
            return new Vector2(Mathf.Clamp(value.x, min, max), Mathf.Clamp(value.y, min, max));
        }

        public static bool IsInRange(this Vector2Int range, int value)
        {
            return value >= range.x && value <= range.y;
        }

        public static void Resize(this RenderTexture rt, int w, int h)
        {
            if (rt == null)
                return;

            if (rt.width == w && rt.height == h)
                return;

            rt.Release();
            rt.width = Mathf.Clamp(w, 1, SystemInfo.maxTextureSize);
            rt.height = Mathf.Clamp(h, 1, SystemInfo.maxTextureSize);
            rt.Create();
        }

        public static void Clear(this RenderTexture rt, Color clearColor)
        {
            if (rt == null)
                return;

            var prevActiveRt = RenderTexture.active;
            RenderTexture.active = rt;

            GL.Clear(true, true, clearColor);

            RenderTexture.active = prevActiveRt;
        }

        

        public static float RemapClamped(this float aValue, float aIn1, float aIn2, float aOut1, float aOut2)
        {
            float t = (aValue - aIn1) / (aIn2 - aIn1);
            if (t > 1f)
                return aOut2;
            if (t < 0f)
                return aOut1;
            return aOut1 + (aOut2 - aOut1) * t;
        }

        public static float Remap(this float val, float in1, float in2, float out1, float out2)
        {
            return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
        }

        public static float Remap(this int val, float in1, float in2, float out1, float out2)
        {
            return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
        }

        public static double Remap(this double val, double in1, double in2, double out1, double out2)
        {
            return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
        }

        public static float Max(this Vector2 v) => Mathf.Max(v.x, v.y);
        
        public static int Max(this Vector2Int v) => Mathf.Max(v.x, v.y);

        public static float GetRandom(this Vector2 v)
        {
            return UnityEngine.Random.Range(v.x, v.y);
        }

        public static Texture2D ToTexture2D(this RenderTexture rt, bool mipMaps = false)
        {
            if (rt == null)
                return null;

            var lastActiveRt = RenderTexture.active;
            RenderTexture.active = rt;

            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, mipMaps);
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            tex.Apply();

            RenderTexture.active = lastActiveRt;

            return tex;
        }

        public static string EncodeToPNGBase64(this Texture2D tex)
        {
            if (tex == null)
                return string.Empty;

            return Convert.ToBase64String(tex.EncodeToPNG());
        }

        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public static Color SetAlpha(this Color c, float a)
        {
            c.a = a;
            return c;
        }

        public static void CopyTo(this RectTransform copyFrom, RectTransform copyTo)
        {
            if (copyFrom == null || copyTo == null)
                return;

            copyTo.anchorMin = copyFrom.anchorMin;
            copyTo.anchorMax = copyFrom.anchorMax;
            copyTo.anchoredPosition = copyFrom.anchoredPosition;
            copyTo.sizeDelta = copyFrom.sizeDelta;
        }


        private static NumberFormatInfo _niceFormat = new NumberFormatInfo { NumberGroupSeparator = " " };
        public static string ToStringNice(this int val)
        {
            return val.ToString("D", _niceFormat);
        }

        public static string ToDigit(this float value)
        {
            return value.ToString("N1").Replace(",", ".");
        }

        public static string ToDigit2(this float value)
        {
            return value.ToString("N2").Replace(",", ".");
        }

        public static string ToRoman(this int number)
        {
            if ((number < 0) || (number > 3999))
                return number.ToString();

            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            return "I" + ToRoman(number - 1);
        }

        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static AnimationClip GetClipByIndex(this Animation anim, int index)
        {
            if (anim == null)
                return null;

            int i = 0;
            foreach (AnimationState animationState in anim)
            {
                if (i == index)
                    return animationState.clip;
                i++;
            }
            return null;
        }

    }
}
