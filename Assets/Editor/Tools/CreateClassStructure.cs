using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace Editor.Tools
{
    public class CreateClassStructure : EditorWindow
    {
        private const string ControllerPath = "/Controller/";
        private const string ControllerPostfix = "Controller";
        private const string ViewPath = "/View/";
        private const string ViewPostfix = "View";
        private const string DataPath = "/Data/";
        private const string DataPostfix = "Data";

        private const string ControllerClassString = "public class {0} : BaseController<{1}>\n{{\n}}";
        private const string DataClassString = "public class {0} : BaseData\n{{\n}}";

        private static string _monoBehaviourClassString = "";
        
        private static CreateClassStructure _window;

        [MenuItem("VLR/Create Class Structure")]
        public static void Init()
        {
           // Get existing open window or if none, make a new one:
            _window = GetWindow<CreateClassStructure>(false, "Class Editor");
            _window.minSize = new Vector2(420, 150);

            if (string.IsNullOrEmpty(_monoBehaviourClassString))
            {
                InitMonoBehaviourString();
            }
        }

        private static void InitMonoBehaviourString()
        {
            var builder = new StringBuilder("");
            builder.Append("using System.Collections;\n");
            builder.Append("using System.Collections.Generic;\n");
            builder.Append("using UnityEngine;\n");
            builder.Append("\n");
            builder.Append("public class {0} : MonoBehaviour\n{{\n");
            builder.Append("    void Start()\n    {{\n\n    }}\n");
            builder.Append("\n");
            builder.Append("    void Update()\n    {{\n\n    }}\n");
            builder.Append("}}");
            _monoBehaviourClassString = builder.ToString();
        }

        private string _path;
        private string _name = "Entity name...";

        public void OnGUI()
        {
            if (_window == null)
            {
                Init();
            }
            
            GUILayout.BeginVertical();
            {
                var folder = Selection.activeObject;
                _path = AssetDatabase.GetAssetPath(folder);
                GUILayout.Label(_path);
                _name = GUILayout.TextField(_name);
                if (GUILayout.Button("Create"))
                {
                    CreateDataClass();
                    CreateViewClass();
                    CreateControllerClass();
                    
                    AssetDatabase.Refresh();
                }
            }
            GUILayout.EndVertical();
        }

        private void CreateControllerClass()
        {
            var path = _path + ControllerPath + _name + ControllerPostfix + ".cs";
            if (File.Exists(path))
            {
                return;
            }
            var builder = new StringBuilder();
            var classString = ControllerClassString;
            classString = string.Format(classString, _name + ControllerPostfix, _name + DataPostfix);
            byte[] bytes = Encoding.UTF8.GetBytes(classString);
            File.WriteAllBytes(path, bytes);
        }

        private void CreateViewClass()
        {
            var path = _path + ViewPath + _name + ViewPostfix + ".cs";
            if (File.Exists(path))
            {
                return;
            }
            var builder = new StringBuilder();
            var classString = _monoBehaviourClassString;
            classString = string.Format(classString, _name + ViewPostfix);
            byte[] bytes = Encoding.UTF8.GetBytes(classString);
            File.WriteAllBytes(path, bytes);
        }

        private void CreateDataClass()
        {
            var path = _path + DataPath + _name + DataPostfix + ".cs";
            if (File.Exists(path))
            {
                return;
            }
            var builder = new StringBuilder();
            var classString = DataClassString;
            var className = _name + DataPostfix;
            classString = string.Format(classString, className);
            byte[] bytes = Encoding.UTF8.GetBytes(classString);
            File.WriteAllBytes(path, bytes);
        }
    }
}