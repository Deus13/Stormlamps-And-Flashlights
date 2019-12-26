using UnityEngine;


namespace StormlampsAndFlashlights
{
    public class Implementation
    {
        private const string NAME = "Stormlamps-And-Flashlights";

        public static void OnLoad()
        {
            Log("Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        }

        
       
      

      


        


        internal static float calcleak(float condition, float Hours, float decayconstant)  //decayconstant 0.004 for lamp and 0.008 for flash light
        {
            if (condition < 0.5f) decayconstant *= 25f;        //increas to medium leak
            if (condition < 0.1f) decayconstant *= 25f;         //increas to huge leak
            return Mathf.Exp(-Hours* decayconstant / 24f / Mathf.Clamp(condition,0.01f,1f));

        }


        internal static float checkforInventoryAndWeather(GearItem m_GearItem)  
        {
            var settings = StormlampsAndFlashlightsSettings.Instance;
            float tmp = 1f;
            if (m_GearItem.m_InPlayerInventory) tmp *= settings.ConditionlossHolding;
            if (!GameManager.GetWeatherComponent().IsIndoorEnvironment()) tmp *= settings.ConditionlossOutside;
            if (GameManager.GetWeatherComponent().GetWeatherStage() == WeatherStage.Blizzard) tmp *= settings.BatterylossBizzard;
            return tmp;
        }



        internal static void Log(string message)
        {
            Debug.LogFormat("[" + NAME + "] {0}", message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format("[" + NAME + "] {0}", message);
            Debug.LogFormat(preformattedMessage, parameters);
        }
    }
   
}