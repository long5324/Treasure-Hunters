using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(PoolingControler),true)]
public class InspecterEditer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PoolingControler cullingController = (PoolingControler)target;
        if (GUILayout.Button("Run UnActive"))
        {
            Undo.RecordObject(cullingController, "Init Amo");
            cullingController.Onsetup();
            EditorUtility.SetDirty(cullingController);
        }
    }
}

