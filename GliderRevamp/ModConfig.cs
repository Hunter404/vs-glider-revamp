using System;

namespace GliderRevamp;

[UsedImplicitly]
public sealed class ModConfig
{
    public static ModConfig Instance { get; set; } = new();

    // ============================================================
    // Core toggles
    // ============================================================

    public bool DisableGlider { get; set; } = false;
    public bool ShowSpeed { get; set; } = false;

    // ============================================================
    // Flight Physics
    // ============================================================

    public float ClimbCoefficiency { get; set; } = 0.9f;

    public float TurnRateRadians { get; set; } = 1f;

    public float TurnRate
    {
        get => TurnRateRadians * 180f / (float)Math.PI;
        set => TurnRateRadians = value * (float)Math.PI / 180f;
    }

    public float DragCoefficiency { get; set; } = 0.1f;

    public float StallSpeed { get; set; } = 6 / 60f;

    public float StallSpeedMs
    {
        get => StallSpeed * 60;
        set => StallSpeed = value / 60;
    }

    public float LiftFactor { get; set; } = 0.5f;
}
