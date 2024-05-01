using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int souls;
    private bool hasRaviosHood; 
    private bool hasMorshusBombs;
    private bool hasPsiHeal; 

    void Start()
    {
        souls = 0;
        hasRaviosHood = false;
        hasMorshusBombs = false;
        hasPsiHeal = false;
    }

    public int GetSouls()
    {
        return souls;
    }

    public void AddSouls(int amount)
    {
        souls += amount;
    }

    public bool HasRaviosHood()
    {
        return hasRaviosHood;
    }

    public void EnableRaviosHood()
    {
        hasRaviosHood = true;
    }

    public bool HasMorshusBombs()
    {
        return hasMorshusBombs;
    }

    public void EnableMorshusBombs()
    {
        hasMorshusBombs = true;
    }

    public bool HasPsiHeal()
    {
        return hasPsiHeal;
    }

    public void EnablePsiHeal()
    {
        hasPsiHeal = true;
    }
}