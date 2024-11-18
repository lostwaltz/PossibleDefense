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

        List<List<StageTileTag>> MapMatirx = new List<List<StageTileTag>>();

        for (int i = 0; i < lines.Length; i++)
        {
            List<StageTileTag> ColumnTileTag = new List<StageTileTag>();

            string line = lines[i].Trim(); // 공백 제거
            if (string.IsNullOrEmpty(line)) continue; // 빈 줄 무시

            string[] values = line.Split(',');
            
            for(int j = 0; j < values.Length; j++)
            {
                StageTileTag.TryParse(values[j], out StageTileTag result);
                ColumnTileTag.Add(result);
            }

            MapMatirx.Add(ColumnTileTag);
        }

        return MapMatirx;
    }
}
