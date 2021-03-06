﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using XUnity.AutoTranslator.Plugin.Core.Constants;

namespace XUnity.AutoTranslator.Plugin.Core.Hooks.TextMeshPro
{
   public static class TextMeshProHooks
   {
      public static readonly Type[] All = new[] {
         typeof( TeshMeshProUGUIAwakeHook ),
         typeof( TeshMeshProAwakeHook ),
         typeof( TextPropertyHook ),
         typeof( SetTextHook1 ),
         typeof( SetTextHook2 ),
         typeof( SetTextHook3 ),
         typeof( SetCharArrayHook1 ),
         typeof( SetCharArrayHook2 ),
         typeof( SetCharArrayHook3 ),
      };
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class TeshMeshProUGUIAwakeHook
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TextMeshProUGUI != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Method( Types.TextMeshProUGUI, "Awake" );
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextInitialized( __instance );
      }
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class TeshMeshProAwakeHook
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TextMeshPro != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Method( Types.TextMeshPro, "Awake" );
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextInitialized( __instance );
      }
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class TextPropertyHook
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TMP_Text != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Property( Types.TMP_Text, "text" ).GetSetMethod();
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextChanged( __instance );
      }
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class SetTextHook1
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TMP_Text != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Method( Types.TMP_Text, "SetText", new[] { typeof( StringBuilder ) } );
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextChanged( __instance );
      }
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class SetTextHook2
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TMP_Text != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Method( Types.TMP_Text, "SetText", new[] { typeof( string ), typeof( bool ) } );
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextChanged( __instance );
      }
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class SetTextHook3
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TMP_Text != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Method( Types.TMP_Text, "SetText", new[] { typeof( string ), typeof( float ), typeof( float ), typeof( float ) } );
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextChanged( __instance );
      }
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class SetCharArrayHook1
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TMP_Text != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Method( Types.TMP_Text, "SetCharArray", new[] { typeof( char[] ) } );
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextChanged( __instance );
      }
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class SetCharArrayHook2
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TMP_Text != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Method( Types.TMP_Text, "SetCharArray", new[] { typeof( char[] ), typeof( int ), typeof( int ) } );
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextChanged( __instance );
      }
   }

   [Harmony, HarmonyAfter( Constants.KnownPlugins.DynamicTranslationLoader )]
   public static class SetCharArrayHook3
   {
      static bool Prepare( HarmonyInstance instance )
      {
         return Types.TMP_Text != null;
      }

      static MethodBase TargetMethod( HarmonyInstance instance )
      {
         return AccessTools.Method( Types.TMP_Text, "SetCharArray", new[] { typeof( int[] ), typeof( int ), typeof( int ) } );
      }

      static void Postfix( object __instance )
      {
         AutoTranslationPlugin.Current.Hook_TextChanged( __instance );
      }
   }
}
