/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;

public class FireBaseDataBase : MonoBehaviour {

    // TEST varijable
    public Text username;
    public Text noShips;
    public Text prikazPloca;
    public User user;
    private DatabaseReference playerDbRef;


    ArrayList leaderBoard = new ArrayList();

    private const int MaxScores = 5;
    private string logText = "";
    private string email = "";
    private int score = 100;
    protected bool UIEnabled = true;

    const int kMaxLogSize = 16382;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool isFirebaseInitialized = false;
    
    void Start()
    {
        // Set these values before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://the-warships.firebaseio.com/");
        //FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("SERVICE-ACCOUNT-ID@the-warships.iam.gserviceaccount.com");
        playerDbRef = FirebaseDatabase.DefaultInstance.RootReference;

        GetUsers();
    }

    public void GetUsers()
    {
        Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
        Debug.Log("Start GetUsers");
        dbInstance.GetReference("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Nesto ne valja");
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;
                    Debug.Log("" + dictUser[0] + " - " + dictUser[1]);
                }
            }
        });
    }
    /*
    private void WriteNewScore(string userId, int score)
    {
        // Create new entry at /user-scores/$userid/$scoreid and at
        // /leaderboard/$scoreid simultaneously
        string key = reference.Child("users").Push().Key;

        Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
        childUpdates["the-warships/users/ktvrdinic/noOfShips"] = 5;
        //childUpdates["/user-scores/" + userId + "/" + key] = entryValues;

       reference.UpdateChildrenAsync(childUpdates);
    }*/









        // When the app starts, check to make sure that we have
        // the required dependencies to use Firebase, and if not,
        // add them if possible.
        /* protected virtual void Start()
         {
             leaderBoard.Clear();
             leaderBoard.Add("Firebase Top " + MaxScores.ToString() + " Scores");

             FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                 dependencyStatus = task.Result;
                 if (dependencyStatus == DependencyStatus.Available)
                 {
                     InitializeFirebase();
                 }
                 else
                 {
                     Debug.LogError(
                       "Could not resolve all Firebase dependencies: " + dependencyStatus);
                 }
             });
         }*/
         /*
        // Initialize the Firebase database:
    protected virtual void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        // NOTE: You'll need to replace this url with your Firebase App's database
        // path in order for the database connection to work correctly in editor.
        app.SetEditorDatabaseUrl("https://the-warships.firebaseio.com");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
        StartListener();
        isFirebaseInitialized = true;
    }

    protected void StartListener()
    {
        FirebaseDatabase.DefaultInstance
          .GetReference("users").OrderByChild("score")
          .ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
              if (e2.DatabaseError != null)
              {
                  Debug.LogError(e2.DatabaseError.Message);
                  return;
              }
              Debug.Log("Received values for Leaders.");
              string title = leaderBoard[0].ToString();
              leaderBoard.Clear();
              leaderBoard.Add(title);
              if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
              {
                  foreach (var childSnapshot in e2.Snapshot.Children)
                  {
                      if (childSnapshot.Child("score") == null
                    || childSnapshot.Child("score").Value == null)
                      {
                          Debug.LogError("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
                          break;
                      }
                      else
                      {
                          Debug.Log("Leaders entry : " +
                        childSnapshot.Child("email").Value.ToString() + " - " +
                        childSnapshot.Child("score").Value.ToString());
                          leaderBoard.Insert(1, childSnapshot.Child("score").Value.ToString()
                        + "  " + childSnapshot.Child("email").Value.ToString());

                          prikazPloca.text = "";
                          foreach (string item in leaderBoard)
                          {
                              prikazPloca.text += "\n" + item;
                          }
                      }
                  }
              }
          };
    }

    // Exit if escape (or back, on mobile) is pressed.
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Output text to the debug log text field, as well as the console.
    public void DebugLog(string s)
    {
        Debug.Log(s);
        logText += s + "\n";

        while (logText.Length > kMaxLogSize)
        {
            int index = logText.IndexOf("\n");
            logText = logText.Substring(index + 1);
        }
    }

    // A realtime database transaction receives MutableData which can be modified
    // and returns a TransactionResult which is either TransactionResult.Success(data) with
    // modified data or TransactionResult.Abort() which stops the transaction with no changes.
    TransactionResult AddScoreTransaction(MutableData mutableData)
    {
        List<object> leaders = mutableData.Value as List<object>;

        if (leaders == null)
        {
            leaders = new List<object>();
        }
        else if (mutableData.ChildrenCount >= MaxScores)
        {
            // If the current list of scores is greater or equal to our maximum allowed number,
            // we see if the new score should be added and remove the lowest existing score.
            long minScore = long.MaxValue;
            object minVal = null;
            foreach (var child in leaders)
            {
                if (!(child is Dictionary<string, object>))
                    continue;
                long childScore = (long)((Dictionary<string, object>)child)["score"];
                if (childScore < minScore)
                {
                    minScore = childScore;
                    minVal = child;
                }
            }
            // If the new score is lower than the current minimum, we abort.
            if (minScore > score)
            {
                return TransactionResult.Abort();
            }
            // Otherwise, we remove the current lowest to be replaced with the new score.
            leaders.Remove(minVal);
        }

        // Now we add the new score as a new entry that contains the email address and score.
        Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
        newScoreMap["score"] = score;
        newScoreMap["email"] = email;
        leaders.Add(newScoreMap);

        // You must set the Value to indicate data at that location has changed.
        mutableData.Value = leaders;
        return TransactionResult.Success(mutableData);
    }

    public void AddScore()
    {
        if (score == 0 || string.IsNullOrEmpty(email))
        {
            DebugLog("invalid score or email.");
            return;
        }
        DebugLog(String.Format("Attempting to add score {0} {1}",
          email, score.ToString()));

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Leaders");

        DebugLog("Running Transaction...");
        // Use a transaction to ensure that we do not encounter issues with
        // simultaneous updates that otherwise might create more than MaxScores top scores.
        reference.RunTransaction(AddScoreTransaction)
          .ContinueWith(task => {
              if (task.Exception != null)
              {
                  DebugLog(task.Exception.ToString());
              }
              else if (task.IsCompleted)
              {
                  DebugLog("Transaction complete.");
              }
          });
    }

}*/