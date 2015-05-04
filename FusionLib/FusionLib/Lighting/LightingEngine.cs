using FusionLib.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}