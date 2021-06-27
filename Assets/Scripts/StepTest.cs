/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*   Modified by Pia Hettinger (2021)
*/

namespace PedometerU.Tests
{

    using UnityEngine;
    using UnityEngine.UI;
#if PLATFORM_ANDROID
    using UnityEngine.Android;
    using System;
#endif


    public class StepTest : MonoBehaviour
    {
        GameObject dialog = null;
        public Text stepText;
        public Text distanceMeterText;
        public Text distanceKMText;

        //public Text distanceFeetText;

        //public Text currentStepgoal;
        //public Text newStepgoal;
        private float stepgoal = 8500;
        public Image CircleProgressBar;

        private Pedometer pedometer;
        private int loadSteps;
        private float loadDistance;
        private int adderS;

        

        private void Start()
        {

            //Asking for Permission, as newer Versions of Android Require this.
#if PLATFORM_ANDROID

            if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
            {
                Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
                dialog = new GameObject();
            }

#endif
            // Create a new pedometer
            pedometer = new Pedometer(OnStep);
            // Reset UI
            //OnStep(0, 0);

            //Instead of Reseting the UI:
            UpdateS(loadSteps);
            UpdateD(loadDistance);
            OnStep(loadSteps, loadDistance);

            
            //Making a button to Reset Data

            CircleProgressBar = GetComponent<Image>();
        }

      
       
        public void ResetBtn()
        {
            //PlayerPrefs.DeleteKey("Steps Since App Runs");
            PlayerPrefs.DeleteAll();
            UpdateS(0);
            UpdateD(0);
            OnStep(0, 0);
            Debug.Log(PlayerPrefs.GetInt("Steps Since App Runs"));
        }

       
        public void OnStep(int steps, double distance)
        {
            // Display the values     
            //stepText.text=steps;

            //adding the new Steps to the old steps

            float distancef = (float)distance;
            
            
            if (loadSteps == steps)
            {

                stepText.text = loadSteps.ToString();

                distanceMeterText.text = loadDistance.ToString();

                float loadKM = loadDistance / 1000;
                distanceKMText.text = loadKM.ToString();

                //progressbar
                //float adderSf = loadSteps;
                //float stepgoalf = stepgoal;
                //circlebar(adderSf, stepgoalf);

            }
            else
            {
                adderS = loadSteps + steps;

                stepText.text = adderS.ToString();
                SaveVarInt(adderS);

                    
                float adderD = loadDistance + distancef;
                distanceMeterText.text = adderD.ToString();
                SaveVarFloat(adderD);

                float adderKM = adderD / 1000;
                distanceKMText.text = adderKM.ToString();

                //progressbar
                //float adderSf = adderS;
                //float stepgoalf = stepgoal;
                //circlebar(adderSf, stepgoalf);
            }


            // Distance in feet
            //distanceFeetText.text = (distance  *3.28084).ToString("F2") + " f";
            // Distance in meter
            //distanceMeterText.text = (distance).ToString("F2") + " m";
        }


        private void OnDisable()
        {
            // Release the pedometer
            pedometer.Dispose();
            pedometer = null;

        }

        private void circlebar(float adderSf, float stepgoalf)
        {
            float percentage = adderSf / stepgoalf;

            if (percentage > 0.999)
            {
                percentage = 1.0f;
            }
            CircleProgressBar.GetComponent<Image>().fillAmount = percentage;
        }

        public void SaveVarInt(int stepssave)
        {
            PlayerPrefs.SetInt("Steps Since App Runs", stepssave);
            Debug.Log(PlayerPrefs.GetInt("Steps Since App Runs"));

            
        }

        public void SaveVarFloat(float distancesave)
        {
            PlayerPrefs.SetFloat("Distance Since App Runs", distancesave);
            Debug.Log(PlayerPrefs.GetInt("Distance Since App Runs"));
        }

   

        public int UpdateS(int loaderS)
        {
            loadSteps = PlayerPrefs.GetInt("Steps Since App Runs");
            return loadSteps;
            //TODO Reset at 12am.
        }

        public float UpdateD(float loaderD)
        {
            loadDistance = PlayerPrefs.GetFloat("Distance Since App Runs");
            return loadDistance;

            //TODO Reset at 12am.
        }


    }
}