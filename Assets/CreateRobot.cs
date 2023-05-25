using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRobot : MonoBehaviour
{
    [SerializeField] private ModuleSelect _leftAttackModule;
    [SerializeField] private ModuleSelect _rightAttackModule;
    [SerializeField] private ModuleSelect _movementModule;
    [SerializeField] private ModuleSelect _shell1Module;
    [SerializeField] private ModuleSelect _shell2Module;
    [SerializeField] private GameManager _gameManager;

    private void Start() {
        
    }

    public void GenerateRobot() {
        //Robot robot = NewRobot();
        //if (robot != null && _gameManager != null) _gameManager.AddRobot(robot);
    }

    public Robot NewRobot() {
        if (_leftAttackModule.ModuleID == -1) {
            Debug.Log("Left attack module not picked");
            return null;
        }
        if (_rightAttackModule.ModuleID == -1) {
            Debug.Log("Right attack module not picked");
            return null;
        }
        if (_movementModule.ModuleID == -1) {
            Debug.Log("Movement module not picked");
            return null;
        }
        if (_shell1Module.ModuleID == -1) {
            Debug.Log("Left shell module not picked");
            return null;
        }
        if (_shell2Module.ModuleID == -1) {
            Debug.Log("Right shell module not picked");
            return null;
        }

        WeaponModule lWeapon = WeaponModule.GetFromID(_leftAttackModule.ModuleID);
        WeaponModule rWeapon = WeaponModule.GetFromID(_rightAttackModule.ModuleID);
        MoveModule movement = MoveModule.GetFromID(_movementModule.ModuleID);
        ShellModule lShell = ShellModule.GetFromID(_shell1Module.ModuleID);
        ShellModule rShell = ShellModule.GetFromID(_shell2Module.ModuleID);

        Debug.Log("CREATED ROBOT: " + lWeapon.Name + ", " + rWeapon.Name + ", " + movement.Name + ", " + lShell.Name + ", " + rShell.Name + ".");
        return new Robot(lWeapon, rWeapon, movement, lShell, rShell);
    }
}
