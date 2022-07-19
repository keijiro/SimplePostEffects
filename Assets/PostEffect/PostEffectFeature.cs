using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UrpTestbed {

sealed class PostEffectPass : ScriptableRenderPass
{
    public Material material;

    public override void Execute
      (ScriptableRenderContext context, ref RenderingData data)
    {
        if (material == null) return;
        var cmd = CommandBufferPool.Get("PostEffect");
        Blit(cmd, ref data, material, 0);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}

public sealed class PostEffectFeature : ScriptableRendererFeature
{
    public Material material;

    PostEffectPass _pass;

    public override void Create()
      => _pass = new PostEffectPass
           { material = material,
             renderPassEvent = RenderPassEvent.AfterRendering };

    public override void AddRenderPasses
      (ScriptableRenderer renderer, ref RenderingData data)
      => renderer.EnqueuePass(_pass);
}

} // namespace UrpTestbed
