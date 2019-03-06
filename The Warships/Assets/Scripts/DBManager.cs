using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour {

    public static string username;
    public static int broj_brodova;
    public static int zlato;
    public static int rum;
    public static int drvo;
    public static int biseri;
    public static int score;
    public static int level;

    public static bool LoggedIn { get { return username != null; } }

    public static void LogOut()
    {
        username = null;
    }

    // Napisati staticne metode za spremanje raznih podataka.
}
