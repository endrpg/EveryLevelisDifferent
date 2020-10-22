using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
    private Vector3 worldStart;

    private GameObject tileIslandStartPrefab;
    private GameObject tileIslandFillerPrefab;
    private GameObject tileIslandEndPrefab;
    private int segments;
    private Vector2 segmentMinMax;
    private int segmin;
    private int segmax;
    private bool useIslands;

    private GameObject tilePrefab;
    private float tileCount;
    private float tileSpacing;

    private int level;

    [MenuItem("Window/Level Editor")]
    static void OpenWindow()
    {
        LevelEditorWindow window = (LevelEditorWindow)GetWindow(typeof(LevelEditorWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("World Start Position");
        worldStart = EditorGUILayout.Vector3Field("", worldStart);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Tile");
        tilePrefab = (GameObject)EditorGUILayout.ObjectField(tilePrefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Tile Spacing");
        tileSpacing = EditorGUILayout.FloatField(tileSpacing);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Tile Count For Straight World");
        tileCount = EditorGUILayout.FloatField(tileCount);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Generate Straight World"))
        {
            SpawnStraightWorld();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Island Start Tile");
        tileIslandStartPrefab = (GameObject)EditorGUILayout.ObjectField(tileIslandStartPrefab, typeof(GameObject), false);

        GUILayout.Label("Island Filler Tile");
        tileIslandFillerPrefab = (GameObject)EditorGUILayout.ObjectField(tileIslandFillerPrefab, typeof(GameObject), false);

        GUILayout.Label("Island End Tile");
        tileIslandEndPrefab = (GameObject)EditorGUILayout.ObjectField(tileIslandEndPrefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Segment Min Max");
        segmentMinMax = EditorGUILayout.Vector2Field("", segmentMinMax);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Segments");
        segments = EditorGUILayout.IntField(segments);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Use Islands");
        useIslands = EditorGUILayout.Toggle(useIslands);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Generate Runner World"))
        {
            SpawnRunnerWorld(useIslands);
        }

        if(GUILayout.Button("Remove Exisiting Tiles"))
        {
            RemoveAllTiles();
        }
    }

    private void RemoveAllTiles()
    {
        foreach(var go in GameObject.FindGameObjectsWithTag("Tile"))
        {
            DestroyImmediate(go);
        }
    }

    private void SpawnStraightWorld()
    {
        RemoveAllTiles();

        var nextPos = worldStart;
        GameObject go = new GameObject();
        go.name = "Level Design";
        go.tag = "Tile";
        for (int i = 0; i < tileCount; i++)
        {
            var curTile = Instantiate(tilePrefab, nextPos, tilePrefab.transform.rotation);

            nextPos.x += tileSpacing;

            curTile.transform.parent = go.transform;
        }
    }

    private void SpawnRunnerWorld(bool useIslands)
    {
        RemoveAllTiles();

        GameObject go = new GameObject();
        go.name = "Level Design";
        go.tag = "Tile";

        var nextPos = worldStart;
        for (int i = 0; i < segments; i++)
        {
            nextPos.x += tileSpacing * 1.5f;

            var changeLevel = Random.Range(0, 100);
            if (changeLevel > 50)
            {
                var upDown = Random.Range(0, 100);
                if(upDown > 50)
                {
                    nextPos.y += tileSpacing;
                }
                else
                {
                    nextPos.y -= tileSpacing;
                }
            }

            var tileCount = Random.Range((int)segmentMinMax.x, (int)segmentMinMax.y);

            GameObject curTile;

            for (int j = 0; j < tileCount; j++)
            {
                if (useIslands)
                {
                    if(j == 0)
                    {
                        curTile = Instantiate(tileIslandStartPrefab, nextPos, new Quaternion());
                    }
                    else if(j == tileCount - 1)
                    {
                        curTile = Instantiate(tileIslandEndPrefab, nextPos, new Quaternion());
                    }
                    else
                    {
                        curTile = Instantiate(tileIslandFillerPrefab, nextPos, new Quaternion());
                    }
                }
                else
                {
                    curTile = Instantiate(tilePrefab, nextPos, new Quaternion());
                }

                nextPos.x += tileSpacing;
                curTile.transform.parent = go.transform;
            }
        }
    }
}
