using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(FSMExemple))]
public class StateMachineEditor : Editor
{
    private bool _showFoldOut;

    private FSMExemple fsm;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        fsm = (FSMExemple)target;

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null)
            return;

        if (fsm.stateMachine.CurrentStateType.ToString() != "null")
            EditorGUILayout.LabelField(string.Format("{0} : {1} ","Current State ", fsm.stateMachine.CurrentStateType.ToString()));

        _showFoldOut = EditorGUILayout.Foldout(_showFoldOut, "Available States");

        if(_showFoldOut)
        {
            EditorGUILayout.Space(3);

            var keys = fsm.stateMachine.statesDictionary.Keys.ToArray();
            var values = fsm.stateMachine.statesDictionary.Values.ToArray();

            if (keys != null && values != null)
            {
                for (int i = 0; i < fsm.stateMachine.statesDictionary.Count; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} : {1} ", keys[i].ToString(), values[i].ToString()));
                    EditorGUILayout.Space(3);
                }
            }
        }              
    }
}
