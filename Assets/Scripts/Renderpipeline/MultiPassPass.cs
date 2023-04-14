using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class MultiPassPass : ScriptableRenderPass
{
    private List<ShaderTagId> m_Tags;
    public MultiPassPass(List<string> tags)
    {
        m_Tags = new List<ShaderTagId>();
        foreach (string tag in tags)
        {
            m_Tags.Add(new ShaderTagId(tag));
        }

        this.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        //Get the opaque rendering filter settings
        FilteringSettings filteringSettings = FilteringSettings.defaultValue;

        foreach (ShaderTagId pass in m_Tags)
        {
            DrawingSettings drawingSettings = CreateDrawingSettings(pass, ref renderingData, SortingCriteria.CommonOpaque);
            context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
        }

        //submit the context, this will execute all of the queued up commands
        context.Submit();
    }
}
