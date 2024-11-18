using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CSVReader
{
    public static List<List<StageTileTag>> LoadMapMatrixFromCSV(string fileName)
    {
        // Resources 폴더에서 파일 로드
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);

        if (csvFile == null)
        {
            Debug.LogError($"File {fileName} not found in Resources folder.");
            return null;
        }

        // 데이터를 한 줄씩 분리
        string[] lines = csvFile.text.Split('\n');
        var vectorList = new List<Vector3>();

        List<List<StageTileTag>> mapMatirx = new List<List<StageTileTag>>();

        for (int i = 0; i < lines.Length; i++)
        {
            List<StageTileTag> columnTileTag = new List<StageTileTag>();

            string line = lines[i].Trim(); // 공백 제거
            if (string.IsNullOrEmpty(line)) continue; // 빈 줄 무시

            string[] values = line.Split(',');
            
            for(int j = 0; j < values.Length; j++)
            {
                StageTileTag.TryParse(values[j], out StageTileTag result);
                columnTileTag.Add(result);
            }

            mapMatirx.Add(columnTileTag);
        }

        return mapMatirx;
    }

    public static List<Vector3> LoadStageWayPointFromCSV(string fileName)
    {
        // Resources 폴더에서 파일 로드
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);

        if (csvFile == null)
        {
            Debug.LogError($"File {fileName} not found in Resources folder.");
            return null;
        }

        // 데이터를 한 줄씩 분리
        string[] lines = csvFile.text.Split('\n');
        var vectorList = new List<Vector3>();

        List<Vector3> wayPointList = new List<Vector3>();

        //첫번쨰 줄은 헤더이기에 제외 
        for (int i = 1; i < lines.Length; i++)
        {

            string line = lines[i].Trim(); // 공백 제거
            if (string.IsNullOrEmpty(line)) continue; // 빈 줄 무시

            string[] values = line.Split(',');

            if (int.TryParse(values[0], out int x) &&
                int.TryParse(values[1], out int y) &&
                int.TryParse(values[2], out int z))
            {
                vectorList.Add(new Vector3Int(x, y, z));
            }
        }

        return wayPointList;
    }
}
