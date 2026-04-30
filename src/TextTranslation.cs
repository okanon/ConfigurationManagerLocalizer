using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;


namespace ConfigurationManagerLocalizer
{
    [HarmonyPatch(typeof(ConfigurationManager.ConfigurationManager))]
    [HarmonyPatch("CalculateWindowRect")]
    public static class Patch_CalculateWindowRect_AdjustWidth
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var code in instructions)
            {
                if (
                    code.opcode == OpCodes.Ldc_I4
                    && code.operand is int width
                    && width == 650
                )
                    code.operand = 670;

                yield return code;
            }
        }
    }

    [HarmonyPatch]
    public static class Patch_DrawUnknownField_AdjustWidth
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method("ConfigurationManager.SettingFieldDrawer:DrawUnknownField");
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var arguments = new List<CodeInstruction>(instructions);

            for (var i = 0; i < arguments.Count; i++)
            {
                var code = arguments[i];

                if (code.opcode == OpCodes.Ldarg_2)
                {
                    arguments.Insert(i + 1, new CodeInstruction(OpCodes.Ldc_I4_S, 14));
                    arguments.Insert(i + 2, new CodeInstruction(OpCodes.Sub));

                    i += 2;
                }
            }

            return arguments;
        }
    }

    [HarmonyPatch(typeof(ConfigurationManager.ConfigurationManager))]
    [HarmonyPatch("DrawWindowHeader")]
    public static class Patch_DrawWindowHeader_ReplaceText
    {
        private static readonly Dictionary<string, string> Table = new Dictionary<string, string>
        {
            { "Normal settings", " 基本設定" },
            { "Keyboard shortcuts", " ショートカット" },
            { "Advanced settings", " 詳細設定" },
            { "Debug info", " デバッグ情報" },
            { "Open Log", "ログを開く" },
            { "Close", "閉じる" },
            { "Search: ", " 検索: " },
            { "Clear", "クリア" },
            { "Expand All", "すべて展開" },
            { "Collapse All", "すべて折りたたむ" }
        };

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return TranspilerUtil.ReplaceLdstr(instructions, Table);
        }
    }

    [HarmonyPatch(typeof(ConfigurationManager.ConfigurationManager))]
    [HarmonyPatch("DrawTips")]
    public static class Patch_DrawTips_ReplaceText
    {
        private static readonly Dictionary<string, string> Table = new Dictionary<string, string>
        {
            {
                "Tip: Click plugin names to expand. Click setting and group names to see their descriptions.",
                "ヒント: プラグイン名をクリックで展開。設定名・グループ名をクリックで説明表示。"
            },
            {
                "Tip: You can drag this window to move it. It will stay open while you interact with the game.",
                "ヒント: ウィンドウはドラッグで移動できます。ゲーム中も開いたままです。"
            }
        };

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return TranspilerUtil.ReplaceLdstr(instructions, Table);
        }
    }

    [HarmonyPatch(typeof(ConfigurationManager.ConfigurationManager))]
    [HarmonyPatch("SettingsWindow")]
    public static class Patch_SettingsWindow_ReplaceText
    {
        private static readonly Dictionary<string, string> Table = new Dictionary<string, string>
        {
            { "Plugins with no options available: ", "オプションが利用できないプラグイン: " }
        };

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return TranspilerUtil.ReplaceLdstr(instructions, Table);
        }
    }

    [HarmonyPatch(typeof(ConfigurationManager.ConfigurationManager))]
    [HarmonyPatch("DrawDefaultButton")]
    public static class Patch_DrawDefaultButton_ReplaceText
    {
        private static readonly Dictionary<string, string> Table = new Dictionary<string, string>
        {
            { "Reset", "リセット" }
        };

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return TranspilerUtil.ReplaceLdstr(instructions, Table);
        }
    }

    [HarmonyPatch]
    public static class Patch_CollectSettings_ReplaceText
    {
        private static readonly Dictionary<string, string> Table = new Dictionary<string, string>
        {
            { "!Allow plugin to run on every frame", "プラグインの毎フレーム実行" },
            {
                "Disabling this will disable some or all of the plugin's functionality.\nHooks and event-based functionality will not be disabled.\nThis setting will be lost after game restart.",
                "これを無効にすると、プラグインの一部の機能またはすべての機能が無効になります。\nフックやイベントベースの機能は無効になりません。\nこの設定は、ゲームを再起動すると失われます。"
            }
        };

        private static MethodBase TargetMethod()
        {
            return AccessTools.Method("ConfigurationManager.SettingSearcher:CollectSettings");
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return TranspilerUtil.ReplaceLdstr(instructions, Table);
        }
    }

    [HarmonyPatch]
    public static class Patch_DrawBoolField_ReplaceText
    {
        private static readonly Dictionary<string, string> Table = new Dictionary<string, string>
        {
            { "Enabled", " オン" },
            { "Disabled", " オフ" }
        };

        private static MethodBase TargetMethod()
        {
            return AccessTools.Method("ConfigurationManager.SettingFieldDrawer:DrawBoolField");
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return TranspilerUtil.ReplaceLdstr(instructions, Table);
        }
    }

    [HarmonyPatch]
    public static class Patch_DrawKeyCode_ReplaceText
    {
        private static readonly Dictionary<string, string> Table = new Dictionary<string, string>
        {
            { "Set...", "設定..." },
            { "Cancel", "キャンセル" },
            { "Press any key", "任意のキーを押してください" },
            {
                "Set the key by pressing any key on your keyboard.",
                "キーボードの任意のキーを押して、キーを設定してください。"
            }
        };

        private static MethodBase TargetMethod()
        {
            return AccessTools.Method("ConfigurationManager.SettingFieldDrawer:DrawKeyCode");
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return TranspilerUtil.ReplaceLdstr(instructions, Table);
        }
    }
}
