using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamMenuAnim : MonoBehaviour
{
    public GameObject PanelMenu;
    public GameObject DimmBackground;
    public bool menuOpen;
    public bool menuClosed;
    public void ShowMenu()
    {
       
        if (PanelMenu != null)
        {

            Animator animator = PanelMenu.GetComponent<Animator>();

            if (animator != null)
            {
                menuOpen = animator.GetBool("showMenu");
                animator.SetBool("showMenu", true);
                animator.SetBool("hideMenu", false);
                
                DimmLight();
            }

        }

    }
    public void HideMenu()
    {
        
        if (PanelMenu != null)
        {

            Animator animator = PanelMenu.GetComponent<Animator>();
            

            if (animator != null)
            {
                menuClosed = animator.GetBool("hideMenu");
                animator.SetBool("showMenu", false);
                animator.SetBool("hideMenu", true);
               
                DimmDark();
            }

        }
    }


    public void DimmDark()
    {
        DimmBackground.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void DimmLight()
    {
        DimmBackground.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
    }
}
