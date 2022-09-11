using System.Linq;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private bool _showFoldOut;

    private Player _player;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _player = (Player)target;

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("State Machine");
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField("Press I Key for IDLE");
        EditorGUILayout.Space(2);
        EditorGUILayout.LabelField("Press M Key for MOVE");
        EditorGUILayout.Space(2);
        EditorGUILayout.LabelField("Press J Key for JUMP");

        EditorGUILayout.Space(10);

        if (_player.PlayerMachine == null)
            return;

        if (_player.PlayerMachine.CurrentStateType.ToString() != "null")
            EditorGUILayout.LabelField(string.Format("{0} : {1} ", "Current State ", _player.PlayerMachine.CurrentStateType.ToString()));

        _showFoldOut = EditorGUILayout.Foldout(_showFoldOut, "Available States");

        if (_showFoldOut)
        {
            EditorGUILayout.Space(3);

            var keys = _player.PlayerMachine.statesDictionary.Keys.ToArray();
            var values = _player.PlayerMachine.statesDictionary.Values.ToArray();

            if (keys != null && values != null)
            {
                for (int i = 0; i < _player.PlayerMachine.statesDictionary.Count; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} : {1} ", keys[i].ToString(), values[i].ToString()));
                    EditorGUILayout.Space(3);
                }
            }
        }
    }
}
