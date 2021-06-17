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
#endif


    public class StepTest : MonoBehaviour
    {
        GameObject dialog = null;
        public Text stepText;
        public Text distanceMeterText;
        
        //public Text distanceFeetText;

        private Pedometer pedometer;
        private int loadSteps;
        public float loadDistance;
        public float distancef;
        public float adderD;

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
            int adderS = loadSteps + steps;
            

            if (loadSteps == steps)
            {
                stepText.text = loadSteps.ToString();
            }
            else
            {
               
                stepText.text = adderS.ToString();
            }


            //transferSteps = steps;

            // Distance in feet
            //distanceFeetText.text = (distance  *3.28084).ToString("F2") + " f";
            // Distance in meter
            //distanceMeterText.text = (distance).ToString("F2") + " m";

            distancef = (float)distance;
            adderD = loadDistance + distancef; 

            

            if (loadDistance == distancef)
            {
                distanceMeterText.text = loadDistance.ToString();

            }
            else
            {

                distanceMeterText.text = adderD.ToString();
            }


            SaveVar(adderS,adderD);
            
        }

        private void OnDisable()
        {
            // Release the pedometer
            pedometer.Dispose();
            pedometer = null;
            
        }

        public void SaveVar(int stepssave,float distancesave)
        {
            
            PlayerPrefs.SetInt("Steps Since App Runs", stepssave);
            Debug.Log(PlayerPrefs.GetInt("Steps Since App Runs"));

            PlayerPrefs.SetFloat("Distance Since App Runs", distancesave);
            Debug.Log(PlayerPrefs.GetInt("Distance Since App Runs"));

        }

       
        

        public int UpdateS(int loaderS)
        {
            loadSteps = PlayerPrefs.GetInt("Steps Since App Runs");
            loadDistance = PlayerPrefs.GetFloat("Distance Since App Runs");

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