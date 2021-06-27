using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoAnim : MonoBehaviour
{
    
    public bool eating;
    public bool drinking;
    public bool jumping;

  
    public void Eat()
    {
        Animator dinoanimator = DataHandler.Instance.petmodel.GetComponent<Animator>();
        
        if (dinoanimator != null)
        {
            eating = dinoanimator.GetBool("eat");
            dinoanimator.SetBool("eat", true);
            dinoanimator.SetBool("drink", false);
            dinoanimator.SetBool("jump", false);
        }

        //StartCoroutine(ExecuteAfterTime(10));
        //dinoanimator.SetBool("isEating", false);
    }

    public void Drink()
    {
        Animator dinoanimator = DataHandler.Instance.petmodel.GetComponent<Animator>();

        if (dinoanimator != null)
        {
            drinking = dinoanimator.GetBool("drink");
            dinoanimator.SetBool("eat", false);
            dinoanimator.SetBool("drink", true);
            dinoanimator.SetBool("jump", false);
        }

    }


    public void Jump()
    {
        Animator dinoanimator = DataHandler.Instance.petmodel.GetComponent<Animator>();

        if (dinoanimator != null)
        {
            drinking = dinoanimator.GetBool("jump");
            dinoanimator.SetBool("eat", false);
            dinoanimator.SetBool("drink", false);
            dinoanimator.SetBool("jump", true);
        }

        

    }
/*
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
    }

    */
}
