using System;
using JsonModSettings;
using ModSettings;

namespace StormlampsAndFlashlights
{
    internal class StormlampsAndFlashlightsSettings : JsonModSettingsBase<StormlampsAndFlashlightsSettings>
    {
        [Section("Flashlight:")]
        [Name("Degrade from switching")]
        [Description("How much condition is lost when the flashlight is toggelt.")]
        [Slider(0, 2)]
        public float ConditionlossFlashSwitching = 0.25f;

        [Name("Degrade from turned on low")]
        [Description("How much condition is lost per hour when the Flashlight is burning on low settings.")]
        [Slider(0, 5)]
        public float ConditionlossFlashLow = 0.5f;

        [Name("degrade from turned on high")]
        [Description("How much condition is lost per hour when the Flashlight is burning on high settings.")]
        [Slider(0, 10)]
        public float ConditionlossFlashHigh = 5f;

        [Name("Degrade from emptying the Battery")]
        [Description("How much condition is lost when the flashlight is totlay discarged while on.")]
        [Slider(0, 100)]
        public float ConditionlossFlashEmptying = 5f;

        [Name("Discarge for perfect condition")]
        [Description("How much charge the flashlight uses to work, compared to unmoded, when in perfect condition")]
        [Slider(0, 10)]
        public float BatterylossFlashPerfect = 0.2f;

        [Name("Battery discarge for bad condition")]
        [Description("How many times longer the flashlight battery can work, compared to unmoded, when in worst condition")]
        [Slider(0, 10)]
        public float BatterylossFlashWorst = 5f;

        [Name("Battery Selfdiscarge ")]
        [Description("How fast the flashlight discarges it self overtime.If set to high a value, it will discarges it selfe within a few moments after the aurora ending")]
        [Slider(0, 5)]
        public float FlashSelfDiscarge = 2.5f;

        [Name("Work after Aurora")]
        [Description("Determines if the flashlight can be used outside of an Aurora. The battary will still only charge during the aurora.")]
        public bool WorkAfterAurora = true;

        [Section("Stromlantern:")]
        [Name("Degrade from switching")]
        [Description("How much condition is lost when the Stromlantern is toggelt.")]
        [Slider(0, 2)]
        public float ConditionlossLanternSwitching = 0.25f;

        [Name("Degrade when on")]
        [Description("How much condition is lost per hour when the Stromlantern is burning.")]
        [Slider(0, 5)]
        public float ConditionlossLanternOn = 0.5f;

        [Name("Fule consumtion for perfect condition")]
        [Description("How much fule the Stromlantern uses, compared to unmoded, when in perfect condition")]
        [Slider(0, 5)]
        public float FuleRateLanternPerfect = 0.5f;

        [Name("Fule consumtion for bad condition")]
        [Description("How much fule the Stromlantern uses, compared to unmoded, when in worst condition")]
        [Slider(0, 10)]
        public float FuleRateLanternWorst = 2f;

        [Name("Fuleleak ")]
        [Description("How fast fule is leaked from a damaged Stromlantern")]
        [Slider(0, 1)]
        public float FuleLeaksLantern = 0.05f;

        [Section("Flashlight and Stromlantern:")]
        [Name("Increaced conditionloss form holding in hand.")]
        [Description("Multiplies the conditionloss value from above with this value whenn the Stromlantern or Flashlight is on, wihle holding it")]
        [Slider(0, 10)]
        public float ConditionlossHolding = 2f;

        [Name("Increaced conditionloss form being outdoor.")]
        [Description("Multiplies the conditionloss value from above with this value whenn the Stromlantern or Flashlight is on, wihle outside")]
        [Slider(0, 10)]
        public float ConditionlossOutside = 2f;

        [Name("Increaced conditionloss form Bizzard.")]
        [Description("Multiplies the conditionloss value from above with this value whenn the Stromlantern or Flashlight is used outside during a blizard.")]
        [Slider(0, 10)]
        public float BatterylossBizzard = 5f;


        public static void OnLoad()
        {
            Instance = JsonModSettingsLoader.Load<StormlampsAndFlashlightsSettings>();
        }
    }
}
