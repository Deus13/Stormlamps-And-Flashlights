using Harmony;
using System;
using UnityEngine;

namespace StormlampsAndFlashlights
{
   
   
    [HarmonyPatch(typeof(FlashlightItem))]
        [HarmonyPatch("GetNormalizedCharge")]
        internal class FlashlightItem_GetNormalizedCharge
        {
            private static void Postfix(ref float __result, float ___m_CurrentBatteryCharge)
            {
              __result = ___m_CurrentBatteryCharge;


            }
        }
    [HarmonyPatch(typeof(FlashlightItem))]
    [HarmonyPatch("IsLit")]
    internal class FlashlightItem_IsLit
    {
        private enum State
        {
            Off,
            Low,
            High
        }
        private static void Postfix(ref bool __result, State ___m_State)
        {
            __result = (___m_State != State.Off);


        }
    }

    

    [HarmonyPatch(typeof(FlashlightItem))]
    [HarmonyPatch("Update")]
    internal class FlashlightItem_Update
    {
        private enum State
        {
            Off,
            Low,
            High
        }

        private static void Postfix(ref State ___m_State, ref float ___m_CurrentBatteryCharge, GearItem ___m_GearItem)
        {
            float tODHours = GameManager.GetTimeOfDayComponent().GetTODHours(Time.deltaTime);

            float lampCondition = ___m_GearItem.GetNormalizedCondition();

            var settings = StormlampsAndFlashlightsSettings.Instance;
            
            if(!settings.WorkAfterAurora)
            {
                if (___m_State == State.Low)
                {
                    ___m_CurrentBatteryCharge += tODHours / 1f;
                        }
                if (___m_State == State.High)
                {
                    ___m_CurrentBatteryCharge += tODHours / 0.0833333358f;
                }
            }

            if (___m_State == State.Low)
            {
                ___m_CurrentBatteryCharge -= tODHours / 1f * Mathf.Lerp(settings.BatterylossFlashWorst, settings.BatterylossFlashPerfect, ___m_GearItem.GetNormalizedCondition());
                ___m_GearItem.Degrade(settings.ConditionlossFlashLow * tODHours);
            }
            if (___m_State == State.High)
            {
                ___m_CurrentBatteryCharge -= tODHours / 0.0833333358f * Mathf.Lerp(settings.BatterylossFlashWorst, settings.BatterylossFlashPerfect, ___m_GearItem.GetNormalizedCondition());
                ___m_GearItem.Degrade(settings.ConditionlossFlashHigh * tODHours);
            }
            if (!settings.WorkAfterAurora && GameManager.GetWeatherComponent().GetWeatherStage()!= WeatherStage.ClearAurora)
            {
                ___m_CurrentBatteryCharge = 0f;
            }
            ___m_CurrentBatteryCharge *=  Implementation.calcleak(lampCondition, tODHours, settings.FlashSelfDiscarge / 25f);



            if (___m_CurrentBatteryCharge < 0f)
            {
                ___m_CurrentBatteryCharge = 0f;
                ___m_State = State.Off;
                ___m_GearItem.Degrade(settings.ConditionlossFlashEmptying);
            }
        }
    }

   
    [HarmonyPatch(typeof(FlashlightItem))]
    [HarmonyPatch("Toggle")]
    internal class FlashlightItem_Toggle
    {


        private static void Prefix(FlashlightItem __instance, GearItem ___m_GearItem)
        {

            ___m_GearItem.Degrade(StormlampsAndFlashlightsSettings.Instance.ConditionlossFlashSwitching);

        }
    }
    [HarmonyPatch(typeof(KeroseneLampItem))]
    [HarmonyPatch("Update")]
    internal class KeroseneLampItem_Update
    {

        
        private static void Postfix(KeroseneLampItem __instance, GearItem ___m_GearItem)
        {
            var settings = StormlampsAndFlashlightsSettings.Instance;

            float tODHours = GameManager.GetTimeOfDayComponent().GetTODHours(Time.deltaTime);
            if (__instance.IsOn())
            {
                float Conditionlosstot = settings.ConditionlossLanternOn * Implementation.checkforInventoryAndWeather(___m_GearItem);              
                ___m_GearItem.Degrade(Conditionlosstot * tODHours);
            }

            __instance.m_CurrentFuelLiters *= Implementation.calcleak(___m_GearItem.GetNormalizedCondition(), tODHours, settings.FuleLeaksLantern/25);


        }

    }
    [HarmonyPatch(typeof(KeroseneLampItem))]
    [HarmonyPatch("Toggle")]
    internal class KeroseneLampItem_Toggle
    {


        private static void Prefix(KeroseneLampItem __instance, GearItem ___m_GearItem)
        {

            ___m_GearItem.Degrade(StormlampsAndFlashlightsSettings.Instance.ConditionlossLanternSwitching);

        }
    }

    [HarmonyPatch(typeof(KeroseneLampItem))]
    [HarmonyPatch("GetModifiedFuelBurnLitersPerHour")]
    internal class KeroseneLampItem_GetModifiedFuelBurnLitersPerHour
    {


        private static void Postfix(GearItem ___m_GearItem,float __result,float ___m_FuelBurnLitersPerHour)
        {

            __result = ___m_FuelBurnLitersPerHour * Mathf.Lerp(StormlampsAndFlashlightsSettings.Instance.FuleRateLanternWorst, StormlampsAndFlashlightsSettings.Instance.FuleRateLanternPerfect, ___m_GearItem.GetNormalizedCondition());

        }
    }

   

}