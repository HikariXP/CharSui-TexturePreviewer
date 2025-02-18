/*
 * Author: CharSui
 * Created On: 2025.02.18
 * Description: 【代码借助Deepseek-r1生成】在Inspector中添加快速打开预览窗口的方式
 */

using System;
using UnityEditor;
using UnityEngine;

namespace CharSui.TexturePreviewer.EditorExtension
{
	[CustomEditor(typeof(TextureImporter))]
	public class TextureInspectorExtension : Editor
	{
		private Editor defaultEditor;

		private void OnEnable()
		{
			// 创建默认的TextureImporter编辑器
			defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.TextureImporterInspector, UnityEditor"));
		}

		private void OnDisable()
		{
			// 销毁默认编辑器
			DestroyImmediate(defaultEditor);
		}

		public override void OnInspectorGUI()
		{
			// 显示默认界面
			defaultEditor.OnInspectorGUI();

			// 只对Texture2D类型生效
			EditorGUILayout.Space(20);
			if (GUILayout.Button("预览", GUILayout.Height(40)))
			{
				var importer = target as TextureImporter;
				if (importer == null)
				{
					return;
				}

				var texture = AssetDatabase.LoadAssetAtPath<Texture>(importer.assetPath);
				HandleTexture(texture);
			}
		}

		private void HandleTexture(Texture texture)
		{
			// 示例：打开自定义窗口
			TexturePreviewer.PreviewTexture(texture);
		}
	}
}