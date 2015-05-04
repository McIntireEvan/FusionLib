using FusionLib.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FusionLib.Lighting
{
    public class LightingEngine
    {
        protected Dictionary<LightSource, Entity> dynamicLights;
        protected List<LightSource> staticLights;

        public LightingEngine()
        {
            dynamicLights = new Dictionary<LightSource, Entity>();
            staticLights = new List<LightSource>();
        }

        public void AddStaticLight(Vector2 source, int radius)
        {
            LightSource l = new LightSource(source, radius);
            staticLights.Add(l);
        }

        public void AddDynamicLight(Entity p, int radius)
        {
            dynamicLights.Add(new LightSource(p.GetHitbox().Center, radius), p);
        }

        public void Draw()
        {
            foreach (LightSource l in dynamicLights.Keys)
            {
                l.Draw();
            }
            foreach (LightSource l in staticLights)
            {
                l.Draw();
            }
        }

        //Returns the Stencil to render with. Might need changing in the future
        public static DepthStencilState getStencil()
        {
            return new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.Always,
                StencilPass = StencilOperation.Replace,
                ReferenceStencil = 1,
                DepthBufferEnable = false
            };
        }
    }
}