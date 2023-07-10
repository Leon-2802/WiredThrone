using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class ModuleManager : MonoBehaviour
{

    public static ModuleManager instance;
    [SerializeField] private ModuleId _attackModules;
    [SerializeField] private ModuleId _moveModule;
    [SerializeField] private ModuleId _shellModule;
    [SerializeField] private GameObject _robot;
    [SerializeField] private GameObject _errorPanel;
    [SerializeField] private TMP_Text _errorField;
    private string _errormsg;

    private void Start()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void ToggleLeftAttackModule(int id)
    {
        for (int i = 0; i < _robot.transform.GetChild(0).transform.childCount; i++)
        {
            GameObject weapon = _robot.transform.GetChild(0).transform.GetChild(i).gameObject;
            weapon.SetActive(false);
        }
        _robot.transform.GetChild(0).transform.GetChild(id - 1).gameObject.SetActive(true);
    }

    public void ToggleRightAttackModule(int id)
    {
        for (int i = 0; i < _robot.transform.GetChild(1).transform.childCount; i++)
        {
            GameObject weapon = _robot.transform.GetChild(1).transform.GetChild(i).gameObject;
            weapon.SetActive(false);
        }
        _robot.transform.GetChild(1).transform.GetChild(id - 1).gameObject.SetActive(true);
    }

    public void ToggleMoveModule(int id)
    {
        for (int i = 0; i < _robot.transform.GetChild(2).transform.childCount; i++)
        {
            GameObject weapon = _robot.transform.GetChild(2).transform.GetChild(i).gameObject;
            Debug.Log(weapon.name);
            weapon.SetActive(false);
        }
        _robot.transform.GetChild(2).transform.GetChild(id - 1).gameObject.SetActive(true);
    }

    public void ToggleShellLeftModule(int id)
    {
    // for (int i = 0; i < _robot.transform.GetChild(3).transform.childCount; i++)
    //     {
    //         GameObject shell = _robot.transform.GetChild(3).transform.GetChild(i).gameObject;
    //         Debug.Log(shell.name);
    //         shell.SetActive(false);
    //     }
    //     _robot.transform.GetChild(3).transform.GetChild(id - 1).gameObject.SetActive(true);
    }

    public void ToggleShellRightModule(int id)
    {
    // for (int i = 0; i < _robot.transform.GetChild(4).transform.childCount; i++)
    //     {
    //         GameObject shell = _robot.transform.GetChild(4).transform.GetChild(i).gameObject;
    //         Debug.Log(shell.name);
    //         shell.SetActive(false);
    //     }
    //     _robot.transform.GetChild(4).transform.GetChild(id - 1).gameObject.SetActive(true);
    }

    public void ContinueButton()
    {
        int attackLeftId = -1;
        int attackRightId = -1;
        int moveId = -1;
        int shellLeftId = -1;
        int shellRightId = -1;
        // Check if any module slots are empty.
        if (_attackModules.LeftModule.ModuleID == -1 || _attackModules.RightModule.ModuleID == -1)
        {
            _errormsg = "Check attack module slots!";
            StartCoroutine("ErrorMessage");
            return;
        }
        attackLeftId = _attackModules.LeftModule.ModuleID;
        attackRightId = _attackModules.RightModule.ModuleID;

        if (_moveModule.LeftModule.ModuleID == -1)
        {
            _errormsg = "Check movement module slot!";
            StartCoroutine("ErrorMessage");
            return;
        }
        moveId = _moveModule.LeftModule.ModuleID;

        if (_shellModule.LeftModule.ModuleID == -1 || _shellModule.RightModule.ModuleID == -1)
        {
            _errormsg = "Check shell module slots!";
            StartCoroutine("ErrorMessage");
            return;
        }
        shellLeftId = _shellModule.LeftModule.ModuleID;
        shellRightId = _shellModule.RightModule.ModuleID;

        if (BlockSceneManager.instance.Blocks == null || BlockSceneManager.instance.Blocks.Count == 0)
        {
            _errormsg = "Check the code!";
            StartCoroutine("ErrorMessage");
            return;
        }
        WeaponModule leftWeapon = WeaponModule.GetFromID(attackLeftId);
        WeaponModule rightWeapon = WeaponModule.GetFromID(attackRightId);
        MoveModule moveModule = MoveModule.GetFromID(moveId);
        ShellModule shellLeft = ShellModule.GetFromID(shellLeftId);
        ShellModule shellRight = ShellModule.GetFromID(shellRightId);
        List<Block> logic = BlockSceneManager.instance.Blocks;

        Debug.Log(logic.Count);
        _robot.AddComponent<Robot>();
        Robot robot = _robot.GetComponent<Robot>();
        robot._leftAttackModule = leftWeapon;
        robot._rightAttackModule = rightWeapon;
        robot._moveModule = moveModule;
        robot._leftShellModule = shellLeft;
        robot._rightShellModule = shellRight;
        robot._logic = logic;
        RobotManager.instance.AddRobot(robot);
        SceneManager.LoadScene(3);
        _robot.transform.position = new Vector3(5.888f, 0.73f, -13);
        _robot.transform.localScale = new Vector3(0.23f, 0.23f, 0.23f);
    }

    IEnumerator ErrorMessage()
    {
        _errorPanel.SetActive(true);
        _errorField.text = _errormsg;
        yield return new WaitForSeconds(5);
        _errorPanel.SetActive(false);
        _errorField.text = "";
    }
}