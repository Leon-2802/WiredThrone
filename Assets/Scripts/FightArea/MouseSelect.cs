using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseSelect : MonoBehaviour, IPointerEnterHandler
{
    private MeshRenderer _renderer;
    private Material _originalMaterial;

    public void OnPointerEnter(PointerEventData eventData)
    {
         _renderer.materials[0].color = Color.yellow;
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _originalMaterial = _renderer.materials[0];


    // Update is called once per frame
    void Update()
    {
        
    }
}
}
