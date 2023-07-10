using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSceneManager : MonoBehaviour
{
    public static BlockSceneManager instance;
    private List<Block> _blocks;
    [SerializeField] private GameObject _moduleCam;
    [SerializeField] GameObject _moduleCanvas;
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _sidebarCanvas;
    [SerializeField] private GameObject _blockCanvas;
    [SerializeField] private int _blockcount;


    public List<Block> Blocks
    {
        get { return _blocks; }
        set { _blocks = value; }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
        SwitchToModuleView();
    }

    private void Update()
    {
        if (_blocks != null)
        {
            _blockcount = _blocks.Count;
        }
    }

    public void SwitchToModuleView()
    {
        _mainCam.SetActive(false);
        _sidebarCanvas.SetActive(false);
        _blockCanvas.SetActive(false);
        _moduleCanvas.SetActive(true);
        _moduleCam.SetActive(true);
    }

    public void SwitchToProgramView()
    {
        _mainCam.SetActive(true);
        _sidebarCanvas.SetActive(true);
        _blockCanvas.SetActive(true);
        _moduleCanvas.SetActive(false);
        _moduleCam.SetActive(false);
    }

}
