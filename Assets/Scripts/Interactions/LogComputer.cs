using System;
using UnityEngine;

public class LogComputer : Computer
{
    [SerializeField] private GameObject logReaderScreen;

    protected override void StartCom(object sender, EventArgs e)
    {
        base.StartCom(sender, e);
        logReaderScreen.SetActive(true);
    }
    protected override void ExitCom(object sender, EventArgs e)
    {
        base.ExitCom(sender, e);
        logReaderScreen.SetActive(false);
    }
}
