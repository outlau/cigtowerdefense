using UnityEngine;
using System.Collections;

public class Game
{
    // Public static variables
    public static int Lives
    {
        get; set;
    }
    public static int Money
    {
        get; private set;
    }
    public static bool GameOver
    {
        get; set;
    }
    public static int Wave
    {
        get; set;
    }
    public static int[] WaveLength
    {
        get; set;
    }

    public static bool GiveMoney(int amt)
    {

        if (amt < 0)
        {
            return false;
        }
        Money += amt;
        return true;
    }
    public static bool TakeMoney(int amt)
    {
        if (amt < 0 || amt > Money)
        {
            return false;
        }
        Money -= amt;
        return true;
    }

    static Game()
    {
        Lives = 1;
        Money = 1000;
        GameOver = false;
        Wave = 0;
     
        
        WaveLength = new int[]{
            30, //Wave 1
            30, //Wave 2
            25, //Wave 3
            15, //Wave 4
            1,  //Wave 5
            3, //Wave 6
            3, //Wave 7
            3, //Wave 8
        };

    }
}
