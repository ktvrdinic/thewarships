using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{

    public static string username;
    public static List<Ship> shipyardShips;
    public static int no_ships;
    public static int gold;
    public static int rum;
    public static int wood;
    public static int pearl;
    public static int experience;
    public static int level;
    public static int no_victory;
    public static int no_lose;

    public static bool LoggedIn { get { return username != null; } }

    public static void LogOut()
    {
        username = null;
    }

    // Napisati staticne metode za spremanje raznih podataka.
}
