using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class Traverse : MonoBehaviour
{
    [SerializeField] private Wire _mainBlock;
    [SerializeField] private GameObject _errorPanel;
    [SerializeField] private TMP_Text _errorField;
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private TMP_Text _successField;
    private string _errormsg = "";

    public void TraverseWires()
    {
        List<Block> result = new List<Block>();
        GameObject codeBlock = _mainBlock.ConnectionOut;
        while (codeBlock != null)
        {
            Debug.Log(codeBlock.name);
            // MoveBlocks always have a side connection.
            if (codeBlock.CompareTag("MoveBlock"))
            {
                GameObject sideBlock = codeBlock.GetComponent<SideConnection>().SideBlock;
                if (sideBlock == null)
                {
                    _errormsg = codeBlock.name + " has no side connection!";
                    StartCoroutine("Errormessage");
                    return;
                }
                int value = sideBlock.GetComponent<NumberBlock>().Value;
                Debug.Log("Value to walk: " + value);
                if (value == 0)
                {
                    _errormsg = sideBlock.name + " connected to " + codeBlock.name + " has a value of 0!";
                    StartCoroutine("Errormessage");
                    return;
                }
                Block<Action> moveBlock = new Block<Action>("MoveToClosestEnemy", Action.Move, value);
                // value is 100% an int here, ?? 0 is a workaround (use 0 if it really is null)
                result.Add(moveBlock);
            }
            //
            else if (codeBlock.CompareTag("AttackBlock"))
            {
                Side value = codeBlock.GetComponent<AttackBlock>().Value;
                Block<Action> attackBlock;
                switch (value)
                {
                    case Side.left:
                        attackBlock = new Block<Action>("Attack", Action.AttackLeft);
                        result.Add(attackBlock);
                        break;
                    case Side.right:
                        attackBlock = new Block<Action>("Attack", Action.AttackRight);
                        result.Add(attackBlock);
                        break;
                }
            }
            else if (codeBlock.CompareTag("VariableBlock"))
            {

            }
            codeBlock = codeBlock.GetComponentInChildren<Wire>().ConnectionOut;
        }
        StartCoroutine("Successmessage");
        BlockSceneManager.instance.Blocks = result;
       
    }

    private void DebugResult(List<Block> b)
    {
        for (int i = 0; i < b.Count; i++)
        {
            Debug.Log(b[i].GetName() + " " + b[i].GetType() + " " + b[i].GetSteps() + " " + b[i].GetMoveVector());
        }
    }

    IEnumerator Errormessage()
    {

        _errorPanel.SetActive(true);
        _errorField.text = _errormsg;
        yield return new WaitForSeconds(5);
        _errorPanel.SetActive(false);
        _errorField.text = "";
    }

    IEnumerator Successmessage()
    {
        _successPanel.SetActive(true);
        _successField.text = "Logic uploading";
        yield return new WaitForSeconds(.5f);
        _successField.text = "Logic uploading.";
        yield return new WaitForSeconds(.5f);
        _successField.text = "Logic uploading..";
        yield return new WaitForSeconds(.5f);
        _successField.text = "Logic uploading...";
        yield return new WaitForSeconds(.5f);
        _successField.text = "Logic uploading....";
        yield return new WaitForSeconds(1);
        _successField.text = "FINISHED";
        yield return new WaitForSeconds(2);
        _successPanel.SetActive(false);
        BlockSceneManager.instance.SwitchToModuleView();
    }
}