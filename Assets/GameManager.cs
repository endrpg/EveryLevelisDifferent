using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button GoldButton, HealthButton, AttackupButton, DefenseupButton;
    public int Health = 100, Gold = 100, Attack = 25, Defense = 25;
    public Text HealthText, GoldText, AttackupText, DefenseupText;
    public int HealthAmount, GoldAmount, AttackAmount, DefenseAmount;
    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "Health: " + Health;
        GoldText.text = "Gold: " + Gold;
        AttackupText.text = "Attack Pow: " + Attack;
        DefenseupText.text = "Defense Pow: " + Defense;
    }

    public void ClickedH() {
        Health += HealthAmount; }
    public void ClickedG() {
        Gold += GoldAmount; }
    public void ClickedD() { 
    Defense += DefenseAmount;}
    public void ClickedA() {

    Attack += AttackAmount; }
  }
