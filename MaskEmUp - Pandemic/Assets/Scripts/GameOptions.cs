using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptions
{
    public static IDictionary<string, int> layerSort = new Dictionary<string, int>()
    {
        {"Hair", 3},
        {"Head", 2},
        {"Eyes", 3},
        {"Mask", 3},
        {"Body", 2},
        {"L_Arm", 4},
        {"R_Arm", 0},
        {"L_Leg", 1},
        {"R_Leg", 0}
    };

}
