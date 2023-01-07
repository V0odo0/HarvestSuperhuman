using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSH
{
    public interface INotifyPropertyChanged
    {
        event Action<string> PropertyChanged;
    }
}
