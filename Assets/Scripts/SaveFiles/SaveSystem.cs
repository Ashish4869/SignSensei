using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Saves and loads data saved in the system.
/// </summary>
public static class SaveSystem
{

    //------------------STATS RELATED--------------------------
    //Converts the dateTime of the file to proper readable format
    public static string GetStatID()
    {
        //split the data
        string date = System.DateTime.Now.ToString();
        string[] dayAndTime = date.Split(" ");
        string[] day = dayAndTime[0].Split("-");
        string[] times = dayAndTime[1].Split(":");

        string finalDate = "";

        //join the data
        for (int i = day.Length - 1; i >= 0; i--) finalDate += day[i];

        for (int i = 0; i < times.Length; i++) finalDate += times[i];

        return finalDate;
    }

   public static void SaveStat(Stats stat)
   {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Stats";
        
        //create new folder if doesnt exist
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        string statId = GetStatID();
        path += "/" + statId;

        //Save data
        FileStream stream = new FileStream(path, FileMode.Create);
        StatData data = new StatData(stat);
        data._statID = statId;

        //save and close file
        formatter.Serialize(stream, data);
        stream.Close();
   }

    public static List<StatData> LoadStatData(int index)
    {
        List<StatData> statData = new List<StatData>();
        string path = Application.persistentDataPath + "/Stats";


        //get the data
        DirectoryInfo d = new DirectoryInfo(path);
        if (!Directory.Exists(path))
        {
            Debug.Log("NO FILES");
            return null;
        }
        else
        {
            //Fetching files using LINQ (Language INtegrated Query)
            FileInfo[] Files = d.GetFiles() //get files in the directory.
                .OrderByDescending(p => p.CreationTime) //order the files by creation time
                .Skip(index * 4) //skip 4*index amount of files.
                .Take(4) //take only 4 of the data at a time
                .ToArray(); // convert to array.
            
            
            BinaryFormatter formatter = new BinaryFormatter();
           
            //add file into the statdata list
            foreach(FileInfo file in Files)
            {
                FileStream stream = new FileStream(path + "/" + file.Name, FileMode.Open);
                StatData stat = formatter.Deserialize(stream) as StatData;
                statData.Add(stat);
                stream.Close();
            }
        }

        return statData;
    }

    public static int GetNoOfStatFiles()
    {
        string path = Application.persistentDataPath + "/Stats";
        if (Directory.Exists(path))
        {
            DirectoryInfo d = new DirectoryInfo(path);
            int noOfFiles = d.GetFiles().Length;
            return noOfFiles;
        }

        return -1;
    }



    //------------------SETTINGS RELATED--------------------------
    public static void SaveSettings(Settings s)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.ini";

        FileStream stream = new FileStream(path, FileMode.Create);
        SettingsData data = new SettingsData(s);

        //save and close file
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.ini";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            stream.Close();
            return data;

        }
        else
        {
            Debug.Log("No settings file");
            return null;
        }
    }
}
