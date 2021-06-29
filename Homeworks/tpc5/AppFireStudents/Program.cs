using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

class Program
{
    static void Main(string[] args)
    {
        FirestoreDb db = FirestoreDb.Create("fire-students-4eb9a");
        QuerySnapshot query = db.Collection("students").GetSnapshotAsync().Result;
        foreach(DocumentSnapshot doc in query)
        {
            print('{');
            Dictionary<string, object> map = doc.ToDictionary();
            foreach(var pair in map)
            {
                print($"{pair.Key} : {pair.Value},");
            }
            print("\b"); //removes the extra ", "
            print('}');
        }
    }

    static void print(string s){ // to "force" write for debug window and cmd
        System.Diagnostics.Debug.Write(s);
        Console.Write(s);
    }

     static void print(char c){
        System.Diagnostics.Debug.Write(c);
        Console.Write(c);
    }

}