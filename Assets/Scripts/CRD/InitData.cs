using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class InitData : MonoBehaviour
{
    private const string URL = "https://docs.google.com/spreadsheets/d/1YG_fi8dFtoPyjmRXZShf48eEhHDvR8dvHUxUl-H7ELE/export?format=tsv&range=A2:I";
    private const string UnitPath = "ScriptableObjects/Units";
    public UnitData[] Units;
    
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
        // int columnSize =row[0].Split('\t').Length;
        Units = Resources.LoadAll<UnitData>(UnitPath);
        if (Units != null)
        {
            string[] rows = tsv.Split('\n'); // 행 데이터를 분리
            if (Units != null && rows.Length > 0)
            {
                int index = 0; // 행 인덱스를 추적할 변수
                foreach (var unit in Units)
                {
                    if (index >= rows.Length) break; // 데이터 부족 시 루프 종료

                    string[] columns = rows[index].Split('\t'); // 열 데이터를 분리
                    if (columns.Length > 0)
                    {
                        unit.ID = columns[0];
                        unit.unitName = columns[1];

                        // 리소스에서 스프라이트 로드
                        Sprite sprite = Resources.Load<Sprite>(columns[2]);
                        if (sprite != null)
                        {
                            unit.attackicon = sprite;
                        }

                        // 값 설정
                        unit.health = float.Parse(columns[3]);
                        unit.magicPoint = float.Parse(columns[4]);
                        unit.moveSpeed = float.Parse(columns[5]);
                        unit.attackRange = float.Parse(columns[6]);
                        unit.attackDamage = float.Parse(columns[7]);
                        unit.attackSpeed = float.Parse(columns[8]);
                    }

                    index++; // 다음 행으로 이동
                }
            }
            else
            {
                // Debug.LogError("Failed to load Unit Data.");
            }
        }
        else
        {
            
            // Debug.LogError("Failed to load Unit Data.");
        }
    }

}
