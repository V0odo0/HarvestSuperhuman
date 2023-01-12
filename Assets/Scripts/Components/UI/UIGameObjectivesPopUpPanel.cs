using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH.UI
{
    public class UIGameObjectivesPopUpPanel : UIPopUpPanel
    {

        [SerializeField] private UIDnaStat _srcVitDnaStat;
        [SerializeField] private UIDnaStat _srcStrDnaStat;
        [SerializeField] private UIDnaStat _srcIntDnaStat;

        [SerializeField] private UIDnaStat _tarVitDnaStat;
        [SerializeField] private UIDnaStat _tarStrDnaStat;
        [SerializeField] private UIDnaStat _tarIntDnaStat;


        protected override void Awake()
        {
            base.Awake();
            
            _srcVitDnaStat.Set(0, 0, StatType.Vit);
            _srcStrDnaStat.Set(0, 0, StatType.Str);
            _srcIntDnaStat.Set(0, 0, StatType.Int);

            var tarStat = GameManager.Data.GameCore.MaxDnaStatValue;
            _tarVitDnaStat.Set(tarStat, tarStat, StatType.Vit);
            _tarStrDnaStat.Set(tarStat, tarStat, StatType.Str);
            _tarIntDnaStat.Set(tarStat, tarStat, StatType.Int);
        }
    }
}
