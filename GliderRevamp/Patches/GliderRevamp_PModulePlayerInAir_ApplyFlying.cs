using System;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;

namespace GliderRevamp.Patches;

[HarmonyPatch(typeof(PModulePlayerInAir), "ApplyFlying"), UsedImplicitly]
public class GliderRevamp_PModulePlayerInAir_ApplyFlying
{
    private static Vec3d RotateTowards(Vec3d fromDir, Vec3d toDir, double maxRadians)
    {
        fromDir = fromDir.Normalize();
        toDir = toDir.Normalize();

        var dot = GameMath.Clamp(fromDir.Dot(toDir), -1, 1);
        var angle = Math.Acos(dot);
        if (angle < 1e-6) return toDir;

        var t = Math.Min(1.0, maxRadians / angle);

        // Slerp on the unit sphere
        var sinAngle = Math.Sin(angle);
        var a = Math.Sin((1 - t) * angle) / sinAngle;
        var b = Math.Sin(t * angle) / sinAngle;

        var blended = fromDir * a + toDir * b;
        return blended.Normalize();
    }
    
    [UsedImplicitly]
    internal static bool Prefix(PModulePlayerInAir __instance, float dt, Entity entity, EntityPos pos, EntityControls controls)
    {
        if (!controls.Gliding)
        {
            GliderRevamp_PModuleInAir_ApplyFlying.ApplyFlying(__instance, dt, entity, pos, controls);
            return false;
        }

        var config = ModConfig.Instance;

        var v = pos.Motion;
        var speed = v.Length();
        if (speed < config.StallSpeed * 0.66f)
        {
            controls.Gliding = false;
            controls.GlideSpeed = 0;
            return false;
        }

        if (controls.GlideSpeed <= 0)
        {
            controls.GlideSpeed = speed;
        }

        var vDir = v.Normalize();
        var viewDir = pos.GetViewVector().ToVec3d().Normalize();

        var turnRateRadPerSec = config.TurnRateRadians;
        var maxTurn = turnRateRadPerSec * dt;
        
        var newDir = RotateTowards(vDir, viewDir, maxTurn);

        var energy = controls.GlideSpeed;
        
        // Apply lift.
        energy -= config.ClimbCoefficiency * v.Y * dt;
        
        // Apply drag.
        energy -= config.DragCoefficiency * Math.Max(speed * speed, 0.15f) * dt;
        
        // Limit new speed to terminal velocity.
        energy = GameMath.Clamp(energy, 0, 40f / 60f);

        controls.GlideSpeed = energy;
        
        pos.Motion = newDir * energy;

        return false;
    }
}
