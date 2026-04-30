using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;


namespace ConfigurationManagerLocalizer
{
    public static class TranspilerUtil
    {
        public static IEnumerable<CodeInstruction> ReplaceLdstr(
            IEnumerable<CodeInstruction> instructions,
            Dictionary<string, string> map
        )
        {
            foreach (var code in instructions)
            {
                if (code.opcode == OpCodes.Ldstr && code.operand is string str)
                {
                    if (map.TryGetValue(str, out var jp))
                    {
                        code.operand = jp;
                    }
                }

                yield return code;
            }
        }
    }
}
