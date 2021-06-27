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


    public class StepTrackScript : MonoBehaviour
    {
        GameObject dialog = null;
        public Text stepText;
        public Text distanceMeterText;
        public Text distanceKMText;


        //public Text currentStepgoal;
        //public Text newStepgoal;
        private int stepgoal = 8500;
        public GameObject CircleProgressBar;


        private Pedometer pedometer;
        public int loadSteps;
        public float loadDistance;
        public float distancef;

        public double loadTest2;


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
            float loadTest = UpdateD(loadDistance);
            loadTest2 = loadTest;
            OnStep(loadSteps, loadTest2);

        }

        public void OnStep(int steps, double distance)
        {
            // Display the values     
            //stepText.text=steps;

            //adding the new Steps to the old steps
            int adderS = loadSteps + steps;

            //display steps every update
            if (loadSteps == steps)
            {
                stepText.text = loadSteps.ToString();
            }
            else
            {
                stepText.text = adderS.ToString();
            }

            //progressbar
            float adderSf = adderS;
            float stepgoalf = stepgoal;
            circlebar(adderSf, stepgoalf);

            //display distance in m every update
            double adderD = loadTest2 + distance;

            if (loadTest2 == distance)
            {
                distanceMeterText.text = loadTest2.ToString();
            }
            else
            {
                distanceMeterText.text = adderD.ToString();
            }

            //converting m to km
            double adderKM = adderD / 1000;
            double loadKM = loadTest2 / 1000;

            if (loadTest2 == distance)
            {
                distanceKMText.text = loadKM.ToString();
            }
            else
            {
                distanceKMText.text = adderKM.ToString();
            }

            //save variables
            SaveVarInt(adderS);
            SaveVarFloat(adderD);
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

        public void ResetBtn()
        {
            //PlayerPrefs.DeleteKey("Steps Since App Runs");
            PlayerPrefs.DeleteAll();
            UpdateS(0);
            UpdateD(0);
            OnStep(0, 0);
            Debug.Log(PlayerPrefs.GetInt("Steps Since App Runs"));
            Debug.Log(PlayerPrefs.GetFloat("Distance Since App Runs"));
        }

        public void SaveVarInt(int stepssave)
        {
            PlayerPrefs.SetInt("Steps Since App Runs", stepssave);
            Debug.Log(PlayerPrefs.GetInt("Steps Since App Runs"));
        }

        public void SaveVarFloat(double distancesave)
        {
            float distancesavef = (float)distancesave;
            PlayerPrefs.SetFloat("Distance Since App Runs", distancesavef);
            Debug.Log(PlayerPrefs.GetFloat("Distance Since App Runs"));
        }



        public int UpdateS(int loaderS)
        {
            loadSteps = PlayerPrefs.GetInt("Steps Since App Runs");
            return loadSteps;
        }

        public float UpdateD(float loaderD)
        {
            loadDistance = PlayerPrefs.GetFloat("Distance Since App Runs");
            return loadDistance;
        }

        private void OnDisable()
        {
            // Release the pedometer
            PlayerPrefs.Save();
            pedometer.Dispose();
            pedometer = null;
        }
    }
}