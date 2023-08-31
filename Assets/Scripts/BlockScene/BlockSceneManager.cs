using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSceneManager : MonoBehaviour
{
    public static BlockSceneManager instance;
    private List<Block> _blocks;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Camera _moduleCam;
    [SerializeField] private Canvas _moduleCanvas;
    [SerializeField] private Canvas _sidebarCanvas;
    [SerializeField] private Canvas _blockCanvas;
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
        _mainCam = GameObject.FindGameObjectWithTag("BlockCam").GetComponent<Camera>();
        _moduleCam = GameObject.FindGameObjectWithTag("ModuleCam").GetComponent<Camera>();

        if (SwitchGameplayManager.instance == null)
        {
            SwitchToModuleView();
        }
    }

    private void Update()
    {
        if (_blocks != null)
        {
            _blockcount = _blocks.Count;
        }
    }

    public void CloseBlockSceneView()
    {
        if (SwitchGameplayManager.instance != null)
            SwitchGameplayManager.instance.SwitchToMainCamera();
    }

    public void SwitchToModuleView()
    {
        _mainCam.enabled = false;
        _moduleCam.enabled = true;
        _sidebarCanvas.enabled = false;
        _blockCanvas.enabled = false;
        _moduleCanvas.enabled = true;
    }

    public void SwitchToProgramView()
    {
        _mainCam.enabled = true;
        _moduleCam.enabled = false;
        _sidebarCanvas.enabled = true;
        _blockCanvas.enabled = true;
        _moduleCanvas.enabled = false;
    }

}
