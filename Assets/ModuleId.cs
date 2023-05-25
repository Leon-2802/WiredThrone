using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleId : MonoBehaviour
{
    [SerializeField] private string _lastPressedModuleName;
    [SerializeField] private ModuleSelect _leftModule;
    [SerializeField] private ModuleSelect _rightModule;

    public void SetModuleId(int id)
    {
        switch (_lastPressedModuleName)
        {
            case "Left":
                _leftModule.SetModuleId(id);
                break;
            case "Right":
                _rightModule.SetModuleId(id);
                break;
            case "Movement":
                _leftModule.SetModuleId(id);
                break;
        }
    }

    public void SetLastPressedModuleName(string name)
    {

        _lastPressedModuleName = name;

    }
}
