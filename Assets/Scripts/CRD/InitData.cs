using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class InitData : MonoBehaviour
{
    private const string URL = "https://docs.google.com/spreadsheets/d/1YG_fi8dFtoPyjmRXZShf48eEhHDvR8dvHUxUl-H7ELE/export?format=tsv&range=A2:I";
    private const string UnitPath = "ScriptableObjects/Units";
    
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        
        string json = www.downloadHandler.text;
        print(json);
        SetSO(json);
        
      
    }
    
    void SetSO(string tsv){
        
        string[] row =tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize =row[0].Split('\t').Length;
        var assets = Resources.LoadAll<UnitData>(UnitPath);
        if (assets != null)
        {
            for (int i = 0; i < Mathf.Min(rowSize, assets.Length); i++)
            {
                string[] column = row[i].Split('\t');
                if (column.Length > 0)
                {
                    assets[i].ID = column[0];
                    assets[i].unitName = column[1];
                    Sprite sprite = Resources.Load<Sprite>(column[2]);
                    if (sprite != null)
                    {
                        assets[i].attackicon = sprite;
                    }

                    assets[i].health = float.Parse(column[3]);
                    assets[i].magicPoint = float.Parse(column[4]);
                    assets[i].moveSpeed = float.Parse(column[5]);
                    assets[i].attackRange = float.Parse(column[6]);
                    assets[i].attackDamage = float.Parse(column[7]);
                    assets[i].attackSpeed = float.Parse(column[8]);
                }
            }
        }
        else
        {
            
            Debug.LogError("Failed to load Unit Data.");
        }
    }

}
