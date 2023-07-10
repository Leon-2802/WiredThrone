using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleId : MonoBehaviour
{
    [SerializeField] private string _lastPressedModuleName;
    [SerializeField] private ModuleSelect _leftModule;
    [SerializeField] private ModuleSelect _rightModule;

    public ModuleSelect LeftModule
    {
        get { return _leftModule; }
    }

    public ModuleSelect RightModule
    {
        get { return _rightModule; }
    }

    public void SetModuleId(int id)
    {
        switch (_lastPressedModuleName)
        {
            case "AttackLeft":
                _leftModule.SetModuleId(id);
                ModuleManager.instance.ToggleLeftAttackModule(id);
                break;
            case "AttackRight":
                _rightModule.SetModuleId(id);
                ModuleManager.instance.ToggleRightAttackModule(id);
                break;
            case "Movement":
                _leftModule.SetModuleId(id);
                ModuleManager.instance.ToggleMoveModule(id);
                break;
            case "ShellLeft":
                _leftModule.SetModuleId(id);
                ModuleManager.instance.ToggleShellLeftModule(id);
                break;
            case "ShellRight":
                _rightModule.SetModuleId(id);
                ModuleManager.instance.ToggleShellRightModule(id);
                break;
        }
    }

    public void SetLastPressedModuleName(string name)
    {

        _lastPressedModuleName = name;

    }
}
