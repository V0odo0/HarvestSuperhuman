using System;
using Knot.Localization;
using UnityEngine;

namespace HSH
{
    public abstract class ModConfigAsset : ScriptableObject
    {
        public ModInfo Info => _info;
        [SerializeField] private ModInfo _info;

        
        public virtual string GetName()
        {
            if (Info.Name.Value.Contains("{0}"))
            {
                return string.Format(Info.Name.Value, Info.Level.ToRoman());
            }

            return Info.Name.Value;
        }

        public virtual string GetDescription()
        {
            return Info.Description.Value;
        }

        public virtual GameProfileData.ModData ToData()
        {
            return new GameProfileData.ModData(Info.Id);
        }

        public abstract void Process(BreedProcessor processor);
    }

    [Serializable]
    public class ModInfo
    {
        public string GroupId => _groupId;
        [SerializeField] private string _groupId;

        public string Id => _id;
        [SerializeField] private string _id;

        public ModType Type => _type;
        [SerializeField] private ModType _type;

        public int Level => _level;
        [SerializeField] private int _level;

        public virtual KnotTextKeyReference Name => _name;
        [SerializeField] private KnotTextKeyReference _name;

        public virtual KnotTextKeyReference Description => _description;
        [SerializeField] private KnotTextKeyReference _description;

        public virtual Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;
    }

    [Serializable]
    public enum ModType
    {
        Negative,
        Neutral,
        Positive
    }
}
