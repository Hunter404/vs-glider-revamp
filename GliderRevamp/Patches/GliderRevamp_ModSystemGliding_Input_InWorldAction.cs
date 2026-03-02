namespace GliderRevamp.Patches;

[HarmonyPatch(typeof(ModSystemGliding), "Input_InWorldAction"), UsedImplicitly]
public class GliderRevamp_ModSystemGliding_Input_InWorldAction
{
    private static readonly AccessTools.FieldRef<ModSystemGliding, ICoreClientAPI> CapiRef
        = AccessTools.FieldRefAccess<ModSystemGliding, ICoreClientAPI>("capi");
    
    [UsedImplicitly]
    internal static bool Prefix(ModSystemGliding __instance, EnumEntityAction action, bool on, ref EnumHandling handled)
    {
        if (ModConfig.Instance.DisableGlider) return false;
        
        var entity = CapiRef(__instance)?.World.Player.Entity;
        if (entity == null)
        {
            return false;
        }

        return !(entity.Pos.Motion.Length() < ModConfig.Instance.StallSpeed || entity.Pos.Motion.Y > 3f / 60);
    }
}