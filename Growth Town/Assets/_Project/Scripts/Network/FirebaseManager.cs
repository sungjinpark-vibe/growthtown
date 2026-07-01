using UnityEngine;
// using Firebase;
// using Firebase.Extensions;
// using Firebase.Firestore;

namespace LifeTown.Network
{
    public class FirebaseManager : MonoBehaviour
    {
        public static FirebaseManager Instance { get; private set; }

        // public FirebaseApp App { get; private set; }
        // public FirebaseFirestore Firestore { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeFirebase();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeFirebase()
        {
            Debug.Log("Initializing Firebase...");
            // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            // {
            //     var dependencyStatus = task.Result;
            //     if (dependencyStatus == DependencyStatus.Available)
            //     {
            //         App = FirebaseApp.DefaultInstance;
            //         Firestore = FirebaseFirestore.DefaultInstance;
            //         Debug.Log("Firebase initialized successfully.");
            //     }
            //     else
            //     {
            //         Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            //     }
            // });
        }
    }
}
